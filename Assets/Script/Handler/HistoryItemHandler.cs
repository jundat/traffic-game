using UnityEngine;
using System;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class HistoryItemHandler : MonoBehaviour {

	public ModelHistoryItem historyItem;

	public UILabel lbContent;
	public UILabel lbTitle;
	public UILabel lbScore;
	public UILabel lbTime;
	public UILabel lbError;

	void Start () {
	}

	void Update () {
		//box collider
		BoxCollider box = lbContent.GetComponent<BoxCollider>();
		box.size = new Vector3 (lbContent.width, lbContent.height, 0);
		box.center = new Vector3 (0, -lbContent.height/2, 0);
	}

	public void Init (ModelHistoryItem p) {

		lbTitle.text = p.map;
		lbScore.text = ""+p.score;

		var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(p.time + 0d)).ToLocalTime();
		lbTime.text = dt.ToLongTimeString () + " " + dt.ToLongDateString();
	
		string content = "";
		int errorNumber = 0;

		if (p.detail.Length > 0) {
			string[] ss = p.detail.Split (',');
			errorNumber = ss.Length;

			for (int i = 0; i < ss.Length; ++i) {
				int errorId = -1;
				int.TryParse (ss[i], out errorId);

				if (errorId != -1) {
					ConfigErrorItem item = ConfigError.Instance.GetError (errorId);
					if (item != null) {
						content += "[b][ff0000]" + errorId + " (-" + item.sub + ")[-][/b]. [000000]" + item.name + "[-]\n";
					} else {
						Debug.LogError ("Item==null: " + errorId);
						Debug.Log (JsonWriter.Serialize (p));
					}
				} else {
					Debug.LogError ("ErrorId == -1: " + ss[i]);
				}
			}
		}

		if (errorNumber > 0) {
			lbError.text = ""+errorNumber;
		} else {
			lbError.text = "";
		}

		lbContent.text = content;
	}
}
