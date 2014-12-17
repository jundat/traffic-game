using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class Menu : MonoBehaviour {

	public GameObject prefabHistoryItem;
	public UIPanel pnHistory;
	public UIPanel pnAbout;

	public UILabel lbUsername;
	public UILabel lbName;
	public UILabel lbLevel;
	public UILabel lbError;

	public GameObject objWait;
	public LoadingHandler loadingHandler;

	public HistoryHandler historyHandler;

	void Start () {
		pnHistory.gameObject.SetActive (false);
		pnAbout.gameObject.SetActive (false);

		lbUsername.text = UserManager.Instance.user.username;
		lbName.text = UserManager.Instance.user.name;
		lbLevel.text = "Level " + UserManager.Instance.user.level;
	}

	void Update () {}

	public void OnPlay () {
		Application.LoadLevel ("SelectMap");
	}

	
	public IEnumerator StartGame () {
		WWWForm form = new WWWForm();
		WWW w = new WWW(Global.URL_GETMAP, form);
		
		yield return w;
		
		if (!string.IsNullOrEmpty(w.error))
		{
			loadingHandler.gameObject.SetActive (false);
			lbError.text = w.error;
		}
		else
		{
			lbError.text = "";

			if (! string.IsNullOrEmpty (w.text)) {

				MapManager.Instance.LoadJSON (w.text);

				StartCoroutine (LoadMainGame ());
			} else {
				loadingHandler.gameObject.SetActive (false);
				lbError.text = "Something wrong!";
			}
		}
	}

	public IEnumerator LoadMainGame () {
		AsyncOperation asyn = Application.LoadLevelAsync ("Main");
		while (asyn.isDone == false) {
			loadingHandler.SetValue (asyn.progress);
			yield return null;
		}
		
		loadingHandler.gameObject.SetActive (false);
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
		
		WWW w = new WWW(Global.URL_SERVER + Global.URL_HISTORY, form);
		
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
