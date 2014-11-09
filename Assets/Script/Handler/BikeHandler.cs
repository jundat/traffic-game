using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BikeHandler : MonoBehaviour {

	public const int LEFT_LIGHT = -1;
	public const int NONE_TURN_LIGHT = 0;
	public const int RIGHT_LIGHT = 1;

	private BikeMovement2 bikeMovement;
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
	public int leftRightLight = NONE_TURN_LIGHT; //default = None
	public Light leftLight;
	public Light rightLight;


	void Start () {
		bikeMovement = this.gameObject.GetComponent <BikeMovement2> ();
		UI2DManager.Instance.onHideHelmet = this.OnTakeOffHelmet;

		RefreshHelmetState ();
		RefreshLightState ();
		RefreshNearFarState ();
		RefreshTurnLight ();
	}

	void Update () {

		//Update Speed
		scooterHandler.SetSpeed (bikeMovement.Speed, 0, 160);


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

		//Sound ----------------------------------

		if (Input.GetKeyUp (KeyCode.B)) {
			Beep ();
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
		leftRightLight--;
		if (leftRightLight < LEFT_LIGHT) {
			leftRightLight = LEFT_LIGHT;
		}
		
		RefreshTurnLight ();
	}

	private void RightLight () {
		leftRightLight++;
		if (leftRightLight > RIGHT_LIGHT) {
			leftRightLight = RIGHT_LIGHT;
		}

		RefreshTurnLight ();
	}

	private void RefreshTurnLight () {
		if (leftRightLight == LEFT_LIGHT) { //left
			leftLight.gameObject.SetActive (true);
			rightLight.gameObject.SetActive (false);
		} else if (leftRightLight == RIGHT_LIGHT) { //right
			leftLight.gameObject.SetActive (false);
			rightLight.gameObject.SetActive (true);
		} else { //none
			leftLight.gameObject.SetActive (false);
			rightLight.gameObject.SetActive (false);
		}
	}
	#endregion

	#region HORN
	public void Beep () {
		SoundManager.Instance.PlayHorn ();
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
