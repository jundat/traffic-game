using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public UIInput inMssv;
	public UIInput inPassword;

	void Start () {}

	void Update () {}

	public void OnSubmit () {
		Debug.Log (inMssv.value + ", " + inPassword.value);
	}
}
