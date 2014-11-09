using UnityEngine;
using System.Collections;

public class BikeHandler : MonoBehaviour {

	private BikeMovement2 bikeMovement;
	public ScooterHandler scooterHandler;

	//helmet
	public HelmetHandler helmet;
	public UIButton btnTakeHelmetOff;

	//light-near-far
	private bool isLightOn = false;
	public GameObject objLight;
	public Light nearLight;
	public Light farLight;

	//horn
	public AudioClip sndHorn;

	//light-left-right
	private int leftRightLight = 0; //default = None
	public Light leftLight;
	public Light rightLight;


	void Start () {
		btnTakeHelmetOff.gameObject.SetActive (false);

		nearLight.gameObject.SetActive (true);
		farLight.gameObject.SetActive (false);
		objLight.SetActive (false);

		bikeMovement = this.gameObject.GetComponent <BikeMovement2> ();
		
		leftLight.gameObject.SetActive (false);
		rightLight.gameObject.SetActive (false);

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
		if (leftRightLight < -1) {
			leftRightLight = -1;
		}
		
		RefreshTurnLight ();
	}

	private void RightLight () {
		leftRightLight++;
		if (leftRightLight > 1) {
			leftRightLight = 1;
		}

		RefreshTurnLight ();
	}

	private void RefreshTurnLight () {
		if (leftRightLight == -1) {
			leftLight.gameObject.SetActive (true);
			rightLight.gameObject.SetActive (false);
		} else if (leftRightLight == 1) {
			leftLight.gameObject.SetActive (false);
			rightLight.gameObject.SetActive (true);
		} else {
			leftLight.gameObject.SetActive (false);
			rightLight.gameObject.SetActive (false);
		}
	}
	#endregion

	#region HORN
	public void Beep () {
		audio.PlayOneShot (sndHorn);
	}
	#endregion

	#region LIGHT
	public bool IsLightOn {
		get {
			return objLight.activeInHierarchy;
		}
	}

	public bool IsLightNear {
		get {
			return nearLight.gameObject.activeInHierarchy;
		}
	}

	public void LightOnOff () {
		isLightOn = ! isLightOn;
		if (isLightOn) {
			objLight.SetActive (true);
		} else {
			objLight.SetActive (false);
		}
	}

	public void LightOff () {
		objLight.SetActive (false);
	}

	public void LightFar () {
		nearLight.gameObject.SetActive (false);
		farLight.gameObject.SetActive (true);
	}

	public void LightNear () {
		nearLight.gameObject.SetActive (true);
		farLight.gameObject.SetActive (false);
	}
	#endregion

	#region HELMET
	public bool IsHelmetOn {
		get {
			return helmet.gameObject.activeInHierarchy;
		}
	}

	public void OnHelmetClick () {
		helmet.Wear ();
		btnTakeHelmetOff.gameObject.SetActive (true);
	}

	public void OnTakeOffHelmet () {
		helmet.UnWear ();
		btnTakeHelmetOff.gameObject.SetActive (false);
	}
	#endregion
}
