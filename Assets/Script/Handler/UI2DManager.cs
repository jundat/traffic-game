using UnityEngine;
using System;
using System.Collections;

public class UI2DManager : SingletonMono<UI2DManager> {

	public UIButton btnTakeHelmetOff;
	public Action onHideHelmet;
	public UILabel lbWorldTime;
	public UILabel lbFPS;
	public UILabel lbRunTime;

	void Start () {}

	void Update () {
		//lbFPS.text = ""+2.0f / Time.deltaTime;
	}

	public void ShowHelmet () {
		btnTakeHelmetOff.gameObject.SetActive (true);
	}

	public void HideHelmet () {
		btnTakeHelmetOff.gameObject.SetActive (false);
		if (onHideHelmet != null) {
			onHideHelmet ();
		}
	}

	public void SetWorldTime (DateTime d) {
		string s = d.ToLongTimeString ();
		lbWorldTime.text = s;
	}

	public void SetRunTime (TimeSpan d) {
		string s = string.Format("{0:00}:{1:00}", d.Minutes, d.Seconds);
		lbRunTime.text = s;
	}
}
