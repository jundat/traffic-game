using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
		string s = "";
		List<ModelErrorItem> l = ErrorManager.Instance.listError;

		int totalFrom = 0;
		int totalTo = 0;
		int count = 0;

		for (int i = 0; i < l.Count; ++i) {
			ConfigErrorItem it = ConfigError.Instance.GetError (l[i].errorId);

			if (it != null) {
				count++;
				totalFrom += it.moneyFrom;
				totalTo += it.moneyTo;

				s += 	"[ff0000]" + (i+1) + ". [-]"
						+ "[0000ff]" + l[i].time.ToLongTimeString() + "[-]"
						+ "[ff0000] (-" + it.sub + ")[-]"
						+ "[000000]\n" + it.name + "[-]"
						+ "\n[000000]=>[-] [FF2F00]" + it.moneyFrom.ToString("n0") + "[-] - [FF2F00]" + it.moneyTo.ToString("n0") + "[-][000000] vnd[-]\n\n";
			}
		}
		s += "[-]";

		s = "[000000]TOTAL SCORE[-]: [ff0000]" + ErrorManager.Instance.Score + "[-]"
			+ "\n[000000]TOTAL ERROR[-]: [ff0000]" + count + "[-]"
				+ "\n[000000]TOTAL MONEY[-]: [FF2F00]" + totalFrom.ToString("n0") + "[-] - [FF2F00]" + totalTo.ToString("n0") + "[-][000000] vnd[-]\n\n" + s;

		lbContent.text = s;

		//
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
		int score = ErrorManager.Instance.Score;
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
