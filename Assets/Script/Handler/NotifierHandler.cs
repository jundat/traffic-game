using UnityEngine;
using System.Collections;

public class NotifierHandler : SingletonMono<NotifierHandler> {

	public UITextList uiTextList;

	void Start () {
		//InvokeRepeating ("ScheduleTime", 0, 2);
	}
	
	void Update () {}

	public void ScheduleTime () {
		PushNotify ("Time: " + (int)Time.realtimeSinceStartup + "s: [00ff00]OK[-]");
	}

	public void PushNotify (string s) {
		uiTextList.Add (s);
	}
}
