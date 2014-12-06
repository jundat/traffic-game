using UnityEngine;
using System;
using System.Collections;

public class UI2DManager : SingletonMono<UI2DManager> {

	public UIButton btnTakeHelmetOff;
	public UILabel lbWorldTime;
	public UILabel lbFPS;
	public UILabel lbRunTime;

	public GameObject objTutorial;

	void Start () {}

	void Update () {}

	public void ShowHideTutorial (bool isShow) {
		objTutorial.gameObject.SetActive (isShow);
	}

	public void ShowHelmet () {
		btnTakeHelmetOff.gameObject.SetActive (true);
	}

	public void HideHelmet () {
		btnTakeHelmetOff.gameObject.SetActive (false);
	}

	public void SetWorldTime (DateTime d) {
		string s = d.ToLongTimeString ();
		lbWorldTime.text = s;
	}

	public void SetRunTime (TimeSpan d) {
		string s = string.Format("{0:00}:{1:00}", d.Minutes, d.Seconds);
		lbRunTime.text = s;
	}

	//Event

	public void OnClickCloseTutorial () {
		objTutorial.SetActive (false);
	}

	public void OnClickOpenTutorial () {
		objTutorial.SetActive (true);
	}
}
