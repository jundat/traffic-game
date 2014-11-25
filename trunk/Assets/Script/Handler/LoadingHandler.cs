using UnityEngine;
using System.Collections;

public class LoadingHandler : MonoBehaviour {

	public UISlider slider;

	void Start () {
		slider.gameObject.SetActive (false);
	}

	void Update () {}

	public void SetValue (float v) {
		slider.gameObject.SetActive (true);
		slider.value = v;
	}
}
