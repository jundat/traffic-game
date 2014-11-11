using UnityEngine;
using System.Collections;

public class PlayerHandler : SingletonMono <PlayerHandler> {

	private const float DELAY_TIME = 1;
	private const float SCHEDULE_TIME = 0.1f;

	private BikeHandler bikeHandler;
	private BikeMovement2 bikeMovement;


	void Awake () {
		bikeHandler = gameObject.GetComponent <BikeHandler> ();
		bikeMovement = gameObject.GetComponent <BikeMovement2> ();
	}

	void Start () {
		InvokeRepeating ("UpdateState", DELAY_TIME, SCHEDULE_TIME);
	}

	void Update () {}

	#region COLLISION, CHECK POINT
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Collide: " + other.name);

		switch (other.name) {
		case OBJ.FINISH_POINT:
			Debug.LogError ("End Game!!!");
			break;
		}
	}
	#endregion

	#region PLAYER STATE
	private void OnRoadChange (PlayerState oldState, PlayerState newState) {
		Debug.Log ("Change: " + oldState.road.tile.objId + " -> " + newState.road.tile.objId);

	}
	
	private PlayerState _currentState;
	private PlayerState _lastState;

	private void UpdateState () {
		_lastState = _currentState;
		_currentState = GetCurrentState ();

		if (_lastState != null && _currentState != null) {
			if (_lastState.road.tile.objId != _currentState.road.tile.objId) {
				OnRoadChange (_lastState, _currentState);
			}
		}
	}

	public PlayerState GetCurrentState () {
		PlayerState p = new PlayerState ();

		p.isHelmetOn = bikeHandler.isHelmetOn;
		p.isLightOn = bikeHandler.isLightOn;
		p.isNearLight = bikeHandler.isNearLight;
		p.leftRightLight = bikeHandler.leftRightLight;
		p.speed = bikeMovement.Speed;
		p.road = null;

		//Raycats to get road
		RaycastHit hit;
		Ray rayDown = new Ray (transform.position + new Vector3 (0, 1, 0), Vector3.down);
		//Debug.DrawRay (transform.position + new Vector3 (0, 1, 0), Vector3.down);

		if (Physics.Raycast (rayDown, out hit)) {

			if (hit.transform.gameObject.name.Equals (OBJ.ROAD)) {
				p.road = hit.transform.gameObject.GetComponent <RoadHandler>();
				//Debug.Log (hit.transform.gameObject.name + ": " + p.road.tile.typeId);
			}
		}

		return p;
	}
	#endregion
}
