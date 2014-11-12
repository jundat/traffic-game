using UnityEngine;
using System.Collections;

public class NotifierHandler : SingletonMono<NotifierHandler> {

	public UITextList uiTextList;

	void Start () {
		InvokeRepeating ("ScheduleTime", 0, 2);
	}
	
	void Update () {}

	public void ScheduleTime () {
		AddNotify ("Time: " + (int)Time.realtimeSinceStartup + "s: [00ff00]OK[-]");
	}

	public void AddNotify (string s) {
		uiTextList.Add (s);
	}
}
