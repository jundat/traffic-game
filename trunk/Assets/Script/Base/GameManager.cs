using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : SingletonMono<GameManager> {

	public ModelTile startPoint;
	public ModelTile endPoint;
	public Dictionary<int, bool> listCheckpoint = new Dictionary<int, bool> (); //objId, isCompleted

	void Start () {}

	void Update () {}

	public void AddCheckpoint (ModelTile t) {
		listCheckpoint[t.objId] = false;
	}

	public void CompleteCheckpoint (int objId) {
		try {
		if (listCheckpoint[objId] == false) {
				listCheckpoint[objId] = true;
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
