using UnityEngine;
using System.Collections;

public class BikeHandler : MonoBehaviour {

	public HelmetHandler helmet;
	public UIButton btnTakeOff;

	void Start () {
		btnTakeOff.gameObject.SetActive (false);
	}

	void Update () {
	}

	public void OnHelmetClick () {
		helmet.Wear ();
		btnTakeOff.gameObject.SetActive (true);
	}

	public void OnTakeOffHelmet () {
		helmet.UnWear ();
		btnTakeOff.gameObject.SetActive (false);
	}
}
