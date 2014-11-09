using UnityEngine;
using System;
using System.Collections;

public class UI2DManager : SingletonMono<UI2DManager> {

	public UIButton btnTakeHelmetOff;
	public Action onHideHelmet;

	void Start () {}

	void Update () {}

	public void ShowHelmet () {
		btnTakeHelmetOff.gameObject.SetActive (true);
	}

	public void HideHelmet () {
		btnTakeHelmetOff.gameObject.SetActive (false);
		if (onHideHelmet != null) {
			onHideHelmet ();
		}
	}
}
