using UnityEngine;
using System;
using System.Collections;

public class UI2DManager : SingletonMono<UI2DManager> {

	public static Color COLOR_RED = new Color (1, 0, 0);
	public static Color COLOR_ORANGE = new Color (1, 0.5f, 0);
	public static Color COLOR_YELLOW = new Color (1, 1, 0);
	public static Color COLOR_GREEN = new Color (0, 1, 0);

	public UIButton btnTakeHelmetOff;
	public UILabel lbWorldTime;
	public UILabel lbFPS;
	public UILabel lbRunTime;
	public ScorePanelHandler scoreHandler;

	public GameObject objTutorial;

	void Start () {
	}

	void Update () {}

	public void ShowScore () {
		scoreHandler.Show ();
	}

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

	public void SetRemainTime (TimeSpan d) {
		string s = string.Format("{0:00}:{1:00}", d.Minutes, d.Seconds);
		lbRunTime.text = s;

		if (d.TotalSeconds < 0) {
			lbRunTime.GetComponent<TweenScale>().enabled = false;
		} else if (d.TotalSeconds < 60) {
			lbRunTime.color = COLOR_RED;
			lbRunTime.GetComponent<TweenScale>().enabled = true;
		} else if (d.TotalSeconds < 120) {
			lbRunTime.color = COLOR_ORANGE;
		} else if (d.TotalSeconds < 180) {
			lbRunTime.color = COLOR_YELLOW;
		} else {
			lbRunTime.color = COLOR_GREEN;
		}
	}

	//Event

	public void OnClickCloseTutorial () {
		objTutorial.SetActive (false);
	}

	public void OnClickOpenTutorial () {
		if (Main.Instance.isEndGame == true) {return;}

		objTutorial.SetActive (true);
	}

	public void OnExitGame () {
		Application.LoadLevel ("SelectMap");
	}
}
