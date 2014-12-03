using UnityEngine;
using System;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class Login : MonoBehaviour {

	public static bool IsRunned = false;

	public UIInput inMssv;
	public UIInput inPassword;
	public GameObject objWait;
	public UILabel lbError;
	
	void Start () {
		objWait.SetActive (false);
		IsRunned = true;
	}
	
	void Update () {}
	
	public void OnSubmit () {
		objWait.SetActive (true);

		UserManager.Instance.password = inPassword.value;

		StartCoroutine( Submit (inMssv.value, inPassword.value));
	}

	public IEnumerator Submit (string username, string password) {
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		form.AddField("password", password);
		
		WWW w = new WWW(Global.URL_SERVER + Global.URL_LOGIN, form);
		
		yield return w;

		if (!string.IsNullOrEmpty(w.error))
		{
			objWait.SetActive (false);
			lbError.text = w.error;
		}
		else
		{
			objWait.SetActive (false);
			lbError.text = "";

			ModelLoginResponse res = JsonReader.Deserialize <ModelLoginResponse> (w.text) as ModelLoginResponse;
			if (res != null) {
				if (res.is_success) {

					UserManager.Instance.user = res.userinfo.ToModelUser ();
					Application.LoadLevel ("Menu");
				} else {
					lbError.text = res.message;
				}
			} else {
				lbError.text = "Something wrong with API";
			}
		}
	}
}
