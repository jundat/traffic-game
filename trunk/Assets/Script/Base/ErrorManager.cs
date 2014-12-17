using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ErrorManager : SingletonMono<ErrorManager> {

	public const int MAX_SCORE = 100;
	public List<ModelErrorItem> listError = new List<ModelErrorItem> ();

	void Start () {}

	void Update () {}

	/// <summary>
	/// Pushs the error.
	/// </summary>
	/// <returns><c>true</c>If can continue run<c>false</c>If must stop run immediately</returns>
	public bool PushError (int errorId, DateTime time) {
		if (Main.Instance.isStarted == false || Main.Instance.isEndGame == true) {return false;}
		
		ConfigErrorItem configItem = ConfigError.Instance.GetError (errorId);

		ModelErrorItem item = new ModelErrorItem ();
		item.errorId = errorId;
		item.time = time;
		item.configItem = configItem;
		AddNewError (item);

		//show
		string message = item.time.ToShortTimeString() 
			+ ": [ff0000]" + configItem.id
			+ ": " + configItem.name +"[-]";

		NotifierHandler.Instance.PushNotify (message);

		if (configItem.sub >= MAX_SCORE) {
			return false;
		} else {
			return true;
		}
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

	public int Score {
		get {
			int score = MAX_SCORE;
			for (int i = 0; i < listError.Count; ++i) {
				score -= listError[i].configItem.sub;
			}

			return score;
		}
	}
}
