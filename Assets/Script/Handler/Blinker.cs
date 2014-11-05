using UnityEngine;
using System.Collections;

public class Blinker : MonoBehaviour {

	public Light targetLight;
	public bool StartShow = true;
	public float TimeShow = 1;
	public float TimeHide = 1;

	private float time = 0;

	void Start () {
		if (StartShow) {
			time = 0;
		} else {
			time = - TimeHide;
		}
	}

	void Update () {
		time += Time.deltaTime;

		if (time > TimeShow) {
			time = - TimeHide;
		}

		if (time > 0) {
			targetLight.enabled = true;
		} else if (time < 0) {
			targetLight.enabled = false;
		}
	}
}
