using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BikeHandler : MonoBehaviour {

//	public const int LEFT_LIGHT = -1;
//	public const int NONE_TURN_LIGHT = 0;
//	public const int RIGHT_LIGHT = 1;

	private BikeMovement bikeMovement;
	public ScooterHandler scooterHandler;

	//helmet
	public bool isHelmetOn;
	public HelmetHandler helmet;

	//light
	public bool isLightOn;
	public GameObject objLight;

	//near-far
	public bool isNearLight;
	public Light nearLight;
	public Light farLight;

	//light-left-right
	public TurnLight turnLight = TurnLight.NONE; //default = None
	public Light leftLight;
	public Light rightLight;


	void Start () {
		bikeMovement = this.gameObject.GetComponent <BikeMovement> ();
		UI2DManager.Instance.onHideHelmet = this.OnTakeOffHelmet;

		RefreshHelmetState ();
		RefreshLightState ();
		RefreshNearFarState ();
		RefreshTurnLight ();
	}

	void Update () {

		//Update Speed
		scooterHandler.SetSpeed (bikeMovement.Speed, 0, 160);
		audio.pitch = 1 + 3.0f * bikeMovement.Speed / 160;

		//Light Near/Far--------------------------

		if (Input.GetKeyUp (KeyCode.L)) {
			LightOnOff ();
		}

		if (Input.GetKeyUp (KeyCode.F)) {
			LightFar ();
		}

		if (Input.GetKeyUp (KeyCode.N)) {
			LightNear ();
		}

		//Light Left/Right -----------------------

		if (Input.GetKeyUp (KeyCode.Q) || Input.GetKeyUp (KeyCode.Less)) {
			LeftLight ();
		}

		if (Input.GetKeyUp (KeyCode.E) || Input.GetKeyUp (KeyCode.Greater)) {
			RightLight ();
		}
	}

	#region LIGHT Left-Right
	private void LeftLight () {
		if (turnLight == TurnLight.RIGHT) {
			turnLight = TurnLight.NONE;
		} else if (turnLight == TurnLight.NONE) {
			turnLight = TurnLight.LEFT;
		}
		
		RefreshTurnLight ();
	}

	private void RightLight () {
		if (turnLight == TurnLight.LEFT) {
			turnLight = TurnLight.NONE;
		} else if (turnLight == TurnLight.NONE) {
			turnLight = TurnLight.RIGHT;
		}

		RefreshTurnLight ();
	}

	private void RefreshTurnLight () {
		if (turnLight == TurnLight.LEFT) { //left
			leftLight.gameObject.SetActive (true);
			rightLight.gameObject.SetActive (false);
		} else if (turnLight == TurnLight.RIGHT) { //right
			leftLight.gameObject.SetActive (false);
			rightLight.gameObject.SetActive (true);
		} else { //none
			leftLight.gameObject.SetActive (false);
			rightLight.gameObject.SetActive (false);
		}
	}
	#endregion
	
	#region LIGHT
	public void RefreshLightState () {
		if (isLightOn) {
			objLight.SetActive (true);
		} else {
			objLight.SetActive (false);
		}
	}

	public void LightOnOff () {
		isLightOn = ! isLightOn;
		RefreshLightState ();
	}

	public void RefreshNearFarState () {
		if (isNearLight) {
			nearLight.gameObject.SetActive (true);
			farLight.gameObject.SetActive (false);
		} else {
			nearLight.gameObject.SetActive (false);
			farLight.gameObject.SetActive (true);
		}
	}

	public void LightFar () {
		isNearLight = false;
		RefreshNearFarState ();
	}

	public void LightNear () {
		isNearLight = true;
		RefreshNearFarState ();
	}
	#endregion

	#region HELMET
	public void RefreshHelmetState () {
		if (isHelmetOn) {
			helmet.Wear ();
			UI2DManager.Instance.ShowHelmet ();
		} else {
			helmet.UnWear ();
		}
	}

	public void OnHelmetClick () {
		isHelmetOn = true;
		RefreshHelmetState ();
	}

	public void OnTakeOffHelmet () {
		isHelmetOn = false;
		RefreshHelmetState ();
	}
	#endregion
}
