using UnityEngine;
using System.Collections;

public class PlayerHandler : SingletonMono <PlayerHandler> {

	private const float DELAY_TIME = 1;
	private const float SCHEDULE_TIME = 0.2f;

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
		NotifierHandler.Instance.AddNotify (oldState.road.Direction + "=>" + newState.road.Direction);

		//Vuot Den Do
		if (Ultil.IsRoad (newState.road.tile.typeId) 
		    && newState.road.Direction == MoveDirection.NONE 
		    && oldState.road.LightStatus == TrafficLightStatus.red) {

			NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Vượt đèn đỏ[-]");
		}

		//Vuot Den Vang
		if (Ultil.IsRoad (newState.road.tile.typeId) 
		    && newState.road.Direction == MoveDirection.NONE 
		    && oldState.road.LightStatus == TrafficLightStatus.yellow) {
			
			NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ffff00]Vượt đèn vàng[-]");
		}
	}

	private void CheckState (PlayerState state) {
		//Nguoc chieu
		if (Ultil.IsOpposite (state.direction, state.road.Direction)) {
			NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Ngược chiều[-]");
		}

		//Lan tuyen
		if (state.road.BikeAvailable == false) {
			NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Lấn tuyến[-]");
		}

	}
	
	private PlayerState _currentState;
	private PlayerState _lastState;

	private void UpdateState () {
		_lastState = _currentState;
		_currentState = GetCurrentState ();

		CheckState (_currentState);

		if (_lastState != null && _currentState != null) {
			if (_lastState.road.tile.objId != _currentState.road.tile.objId) {
				OnRoadChange (_lastState, _currentState);
			}
		}
	}

	public PlayerState GetCurrentState () {
		PlayerState p = new PlayerState ();

		p.time = Time.realtimeSinceStartup;
		p.isHelmetOn = bikeHandler.isHelmetOn;
		p.isLightOn = bikeHandler.isLightOn;
		p.isNearLight = bikeHandler.isNearLight;
		p.leftRightLight = bikeHandler.leftRightLight;
		p.speed = bikeMovement.Speed;
		p.road = null;

		//direction
		float x = transform.forward.x;
		float z = transform.forward.z;
		if (Mathf.Abs (x) > Mathf.Abs (z)) {
			//left-right
			if (x > 0) {
				p.direction = MoveDirection.LEFT;
			} else {
				p.direction = MoveDirection.RIGHT;
			}
		} else {
			//up-down
			if (z > 0) {
				p.direction = MoveDirection.DOWN;
			} else {
				p.direction = MoveDirection.UP;
			}
		}

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
