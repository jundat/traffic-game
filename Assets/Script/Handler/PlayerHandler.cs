using UnityEngine;
using System.Collections;

public class PlayerHandler : SingletonMono <PlayerHandler> {

	private BikeHandler bikeHandler;

	public PlayerState currentState;
	public PlayerState lastState;

	void Awake () {
		bikeHandler = gameObject.GetComponent <BikeHandler> ();
	}

	void Start () {}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Collide: " + other.name);

		switch (other.name) {
		case OBJ.FINISH_POINT:
			Debug.LogError ("End Game!!!");
			break;
		}
	}

	public PlayerState GetCurrentState () {
		PlayerState p = new PlayerState ();

		p.isHelmetOn = bikeHandler.isHelmetOn;
		p.isLightOn = bikeHandler.isLightOn;
		p.isNearLight = bikeHandler.isNearLight;
		p.leftRightLight = bikeHandler.leftRightLight;

		//Raycats to get road

	}
}
