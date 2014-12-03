using UnityEngine;
using System;
using System.Collections;

public class HistoryItemHandler : MonoBehaviour {
	
	public UILabel lbContent;
	public UILabel lbTitle;
	public UILabel lbScore;
	public UILabel lbTime;
	public UILabel lbError;

	void Start () {
	}

	void Update () {}

	public void Init (ModelHistoryItem p) {

		lbTitle.text = p.map;
		lbScore.text = ""+p.score;

		var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(p.time + 0d)).ToLocalTime();
		lbTime.text = dt.ToLongTimeString () + " " + dt.ToLongDateString();
	
		string content = "";
		string[] ss = p.detail.Split (',');
		for (int i = 0; i < ss.Length; ++i) {
			int errorId = -1;
			int.TryParse (ss[i], out errorId);

			if (errorId != -1) {
				ConfigErrorItem item = ConfigError.Instance.GetError (errorId);
				if (item != null) {
					content += errorId + ": " + item.name + "\n";
				} else {
					Debug.LogError ("Item==null: " + errorId);
				}
			} else {
				Debug.LogError ("ErrorId == -1: " + ss[i]);
			}
		}
		
		lbError.text = ""+ss.Length;
		lbContent.text = content;
	}
}
