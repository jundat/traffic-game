using UnityEngine;
using System.Collections;

public class AutoLogin : MonoBehaviour {

	void Awake () {
		if (Login.IsRunned == false) {
			Application.LoadLevel ("Login");
		}
	}
}
