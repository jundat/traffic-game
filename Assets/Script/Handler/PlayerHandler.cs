using UnityEngine;
using System.Collections;

public class PlayerHandler : SingletonMono <PlayerHandler> {

	private BikeHandler bikeHandler;

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
}
