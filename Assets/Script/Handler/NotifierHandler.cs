using UnityEngine;
using System.Collections;

public class NotifierHandler : SingletonMono<NotifierHandler> {

	public UITextList uiTextList;

	void Start () {
		InvokeRepeating ("Sche", 2, 2);
	}
	
	void Update () {}

	public void Sche () {
		AddNotify ("Time: " + Time.realtimeSinceStartup);
	}

	public void AddNotify (string s) {
		uiTextList.Add (s);
	}
}
