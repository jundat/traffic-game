using UnityEngine;
using System.Collections;

public class BikeHandler : MonoBehaviour {

	public HelmetHandler helmetOff;

	void Start () {
	}

	void Update () {
	}

	public void OnHelmetClick () {
		helmetOff.Wear ();
		Debug.Log ("Click");
	}
}
