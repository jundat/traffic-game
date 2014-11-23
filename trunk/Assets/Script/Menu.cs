using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class Menu : MonoBehaviour {

	public GameObject prefabHistoryItem;
	public UIPanel pnHistory;
	public UIPanel pnAbout;

	public UILabel lbUsername;
	public UILabel lbName;
	public UILabel lbError;

	public GameObject objWait;

	public HistoryHandler historyHandler;

	void Start () {
		pnHistory.gameObject.SetActive (false);
		pnAbout.gameObject.SetActive (false);

		lbUsername.text = UserManager.Instance.user.username;
		lbName.text = UserManager.Instance.user.name;
	}

	void Update () {}

	public void OnPlay () {
		Application.LoadLevel ("Main");
	}

	public void OnLogout () {
		Application.LoadLevel ("Login");
	}

	//---------------

	public void OnOpenHistory () {
		objWait.SetActive (true);
		StartCoroutine( History (UserManager.Instance.user.username));
	}

	public IEnumerator History (string username) {
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		
		WWW w = new WWW(Global.URL_HISTORY, form);
		
		yield return w;
		
		if (!string.IsNullOrEmpty(w.error))
		{
			objWait.SetActive (false);
			lbError.text = "Can not connect to server!";
			
		}
		else
		{
			objWait.SetActive (false);
			lbError.text = "";
			
			ModelHistoryResponse res = JsonReader.Deserialize <ModelHistoryResponse> (w.text) as ModelHistoryResponse;
			if (res != null) {
				if (res.is_success) {

					historyHandler.Init (res);
					pnHistory.gameObject.SetActive (true);
				} else {
					lbError.text = res.message;
				}
			} else {
				lbError.text = "Something wrong with API";
			}
		}
	}

	public void OnCloseHistory () {
		pnHistory.gameObject.SetActive (false);
	}
	
	//---------------

	public void OnOpenAbout () {
		pnAbout.gameObject.SetActive (true);
	}

	public void OnCloseAbout () {
		pnAbout.gameObject.SetActive (false);
	}
	
}
