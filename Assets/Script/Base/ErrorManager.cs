using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ErrorManager : SingletonMono<ErrorManager> {

	public List<ModelErrorItem> listError = new List<ModelErrorItem> ();

	void Start () {}

	void Update () {}

	public void PushError (int errorId, DateTime time) {
		if (Main.Instance.isStarted == false || Main.Instance.isEndGame == true) {return;}

		ModelErrorItem item = new ModelErrorItem ();
		item.errorId = errorId;
		item.time = time;
		AddNewError (item);

		ConfigErrorItem configItem = ConfigError.Instance.GetError (item.errorId);

		//show
		string message = item.time.ToShortTimeString() 
			+ ": [ff0000]" + configItem.id
			+ ": " + configItem.name +"[-]";

		NotifierHandler.Instance.PushNotify (message);
	}

	private void AddNewError (ModelErrorItem item) {
		listError.Add (item);


	}

	public string StringError {
		get {
			string s = "";
			for (int i = 0; i < listError.Count; ++i) {
				s += listError[i].errorId + " ";
			}

			s = s.Trim ();
			s = s.Replace (" ", ",");
			return s;
		}
	}
}
