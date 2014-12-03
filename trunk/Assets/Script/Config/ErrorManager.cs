using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ErrorManager : SingletonMono<ErrorManager> {

	public List<ModelErrorItem> listError = new List<ModelErrorItem> ();

	void Start () {
	}

	void Update () {}

	public void PushError (int errorId, DateTime time) {
		ModelErrorItem item = new ModelErrorItem ();
		item.errorId = errorId;
		item.time = time;
		listError.Add (item);

		ConfigErrorItem configItem = ConfigError.Instance.GetError (item.errorId);

		//show
		string message = item.time.ToShortTimeString() 
			+ ": [ff0000]" + configItem.id
			+ ": " + configItem.name +"[-]";

		NotifierHandler.Instance.PushNotify (message);
	}
}
