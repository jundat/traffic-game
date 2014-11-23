using UnityEngine;
using System.Collections;

public class LoadingHandler : MonoBehaviour {

	public UISlider slider;

	void Start () {}

	void Update () {}

	public void SetValue (float v) {
		slider.value = v;
	}
}
