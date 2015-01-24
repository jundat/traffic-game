using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : SingletonMono<GameManager> {

	public TileHandler startPoint;
	public TileHandler endPoint;
	public Dictionary<int, bool> listCheckpoint = new Dictionary<int, bool> (); //objId, isCompleted
	public Dictionary<int, TileHandler> listTileHandler = new Dictionary<int, TileHandler> (); //objId, TileHandler

	void Start () {}

	void Update () {}

	public void AddCheckpoint (TileHandler t) {
		listCheckpoint[t.tile.objId] = false;
		listTileHandler[t.tile.objId] = t;
	}

	public void CompleteCheckpoint (int objId) {
		try {
			MiniMap.Instance.CompleteCheckpoint (objId);

			if (listCheckpoint[objId] == false) {
				SoundManager.Instance.PlayCheckpoint ();

				listCheckpoint[objId] = true;
				listTileHandler[objId].gameObject.SetActive (false);

				NotifierHandler.Instance.PushNotify ("[00ff00]Checkpoint completed![-]");
				int r = RemainCheckpoint;
				if (r > 0) {
					NotifierHandler.Instance.PushNotify ("[ffff00]Remain: " + r + "[-]");
				} else {
					NotifierHandler.Instance.PushNotify ("[ffff00]Remain: end point[-]");
				}
			}
		} catch (Exception) {
			Debug.LogError ("not a check point");
		}
	}

	public bool IsCompleted {
		get {
			foreach (KeyValuePair <int, bool> p in listCheckpoint) {
				if (p.Value == false) {
					return false;
				}
			}
			return true;
		}
	}

	public int RemainCheckpoint {
		get {
			int count = 0;
			foreach (KeyValuePair <int, bool> p in listCheckpoint) {
				if (p.Value == false) {
					count++;
				}
			}
			return count;
		}
	}
}
