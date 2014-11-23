using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ErrorManager : SingletonMono<ErrorManager> {

	public List<ModelError> listError = new List<ModelError> ();

	void Start () {}
	void Update () {}

	public ModelError GetModelError (ErrorCode errorCode) {
		return new ModelError ();
	}

	public void PushError (ModelError error) {
		listError.Add (error);

		//show
		int priority = GetPriority (error);
		string message = "";
		switch (priority) {
		case 1:
			message = error.time.ToShortTimeString() + ": [ff0000]" + error.code.ToString () + ": " + error.description +"[-]";
			break;

		default:
			message = error.time.ToShortTimeString() + ": [ff0000]" + error.code.ToString () + ": " + error.description +"[-]";
			break;
		}

		NotifierHandler.Instance.PushNotify (message);
	}

	public int GetPriority (ModelError error) {
		return 1;
	}
}
