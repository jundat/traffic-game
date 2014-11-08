using UnityEngine;
using System.Collections;

public class Login : MonoBehaviour {
	
	public UIInput inMssv;
	public UIInput inPassword;
	public GameObject objWait;
	public UILabel lbError;
	
	void Start () {
		objWait.SetActive (false);
	}
	
	void Update () {}
	
	public void OnSubmit () {
		Debug.Log (inMssv.value + ", " + inPassword.value);
		objWait.SetActive (true);
		Callback ();
	}

	public void Callback () {
		objWait.SetActive (true);
		Application.LoadLevel ("Menu");
	}
}
