using UnityEngine;
using System.Collections;

public class HistoryItemHandler : MonoBehaviour {

	public UILabel lbTitle;
	public UILabel lbContent;

	void Start () {}

	void Update () {}

	public void Init (ModelHistoryItem p) {
		string title = "";
		string content = "";

		title = "[0000ff]" + p.campaign.name + "[-] : [00ff00]" + p.score + "[-]";
		content = "Time: " + p.time + "\n"
			+ "Map: " + p.campaign.map + "\n";
		for (int i = 0; i < p.detail.Count; ++i) {
			content += p.detail[i] + "\n";
		}

		lbTitle.text = title;
		lbContent.text = content;
	}
}
