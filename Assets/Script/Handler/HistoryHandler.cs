using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HistoryHandler : MonoBehaviour {

	public GameObject prefabHistoryItem;
	public GameObject table;
	public float tileHeight = 64;

	public UIScrollView scrollView;
	public UIScrollBar scrollBar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init (ModelHistoryResponse response) {
		UITable uitable = table.AddComponent <UITable> ();
		uitable.columns = 1;
		uitable.direction = UITable.Direction.Down;
		uitable.sorting = UITable.Sorting.None;
		uitable.hideInactive = true;
		uitable.keepWithinPanel = true;
		uitable.padding = new Vector2 (0, 4);


		List<ModelHistoryItem> scores = response.highscore;

		for (int i = scores.Count-1; i >= 0; --i) {
			ModelHistoryItem item = scores[i];

			GameObject ins = GameObject.Instantiate (prefabHistoryItem) as GameObject;
			HistoryItemHandler handler = ins.GetComponent<HistoryItemHandler>();
			handler.Init (item);

			ins.transform.parent = table.transform;
			ins.transform.localScale = Vector3.one;
		}
	}

	void OnDisable () {
		//clear all
		foreach (Transform t in table.transform) {
			Destroy (t.gameObject);
		}

		UITable uitable = table.GetComponent<UITable>();
		if (uitable != null) {
			Destroy (uitable);
		}
	}
}
