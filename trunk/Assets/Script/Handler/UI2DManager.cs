using UnityEngine;
using System;
using System.Collections;

public class UI2DManager : SingletonMono<UI2DManager> {

	public UIButton btnTakeHelmetOff;
	public Action onHideHelmet;
	public UILabel lbTime;

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

	public void SetTime (DateTime d) {
		string s = d.ToLongTimeString ();
		lbTime.text = s;
	}
}
