using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public GameObject prefabHistoryItem;
	public UIPanel pnHistory;
	public UIPanel pnAbout;

	void Start () {
		pnHistory.gameObject.SetActive (false);
		pnAbout.gameObject.SetActive (false);
	}

	void Update () {}

	public void OnPlay () {
		Application.LoadLevel ("Main");
	}

	public void OnLogout () {
		Application.LoadLevel ("Login");
	}
	
	public void OnOpenHistory () {
		pnHistory.gameObject.SetActive (true);
	}

	public void OnCloseHistory () {
		pnHistory.gameObject.SetActive (false);
	}

	public void OnOpenAbout () {
		pnAbout.gameObject.SetActive (true);
	}

	public void OnCloseAbout () {
		pnAbout.gameObject.SetActive (false);
	}
}
