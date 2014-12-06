using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class ScorePanelHandler : MonoBehaviour {

	public UILabel lbContent;
	public UIButton btnClose;
	public UIButton btnResend;
	public UISprite sprWaiting;
	public UILabel lbError;

	void Start () {}

	void Update () {}

	public void Show () {
		this.transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);
		this.GetComponent<UIWidget>().alpha = 0.01f;

		sprWaiting.gameObject.SetActive (true);
		this.gameObject.SetActive (true);

		this.GetComponent<TweenScale>().enabled = true;
		this.GetComponent<TweenAlpha>().enabled = true;

		StartCoroutine (PostScore ());
	}

	public IEnumerator PostScore () {
		WWWForm form = new WWWForm();

		string map = MapManager.Instance.mapNetwork.uid;
		Debug.Log ("Missing score!");
		int score = 0;
		string detail = ErrorManager.Instance.StringError;

		form.AddField("username", UserManager.Instance.user.username);
		form.AddField("password", UserManager.Instance.password);
		form.AddField("map", map);
		form.AddField("score", score);
		form.AddField("detail", detail);

		WWW w = new WWW(Global.URL_SERVER + Global.URL_POSTSCORE, form);
		yield return w;

		sprWaiting.gameObject.SetActive (false);

		if (!string.IsNullOrEmpty(w.error))
		{
			//resend
			lbError.text = "Upload score failed, please resend!";
		}
		else
		{
			ModelResponse res = JsonReader.Deserialize <ModelResponse> (w.text) as ModelResponse;
			if (res != null) {
				if (res.is_success) {
					btnResend.gameObject.SetActive (false);
					lbError.text = "";
				} else {
					//resend
					lbError.text = "Upload score failed, please resend!";
				}
			} else {
				//resend
				lbError.text = "Upload score failed, please resend!";
			}
		}
	}

	public void OnResend () {
		sprWaiting.gameObject.SetActive (true);
		lbError.text = "";
		StartCoroutine (PostScore ());
	}

	public void OnClose () {
		Application.LoadLevel ("Menu");
	}
}
