using UnityEngine;
using System.Collections;

public class BikeHandler : MonoBehaviour {

	public HelmetHandler helmet;
	public UIButton btnTakeOff;

	public GameObject objLight;
	public Light nearLight;
	public Light farLight;

	public AudioClip sndHorn;

	void Start () {
		btnTakeOff.gameObject.SetActive (false);

		nearLight.gameObject.SetActive (true);
		farLight.gameObject.SetActive (false);
		objLight.SetActive (false);
	}

	void Update () {

		//Light --------------------------

		if (Input.GetKeyUp (KeyCode.L)) {
			LightOn ();
		}

		if (Input.GetKeyUp (KeyCode.F)) {
			LightFar ();
		}

		if (Input.GetKeyUp (KeyCode.N)) {
			LightNear ();
		}

		//Sound --------------------------

		if (Input.GetKeyUp (KeyCode.B)) {
			Beep ();
		}
	}

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

	public void LightOn () {
		objLight.SetActive (true);
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
		btnTakeOff.gameObject.SetActive (true);
	}

	public void OnTakeOffHelmet () {
		helmet.UnWear ();
		btnTakeOff.gameObject.SetActive (false);
	}
	#endregion
}
