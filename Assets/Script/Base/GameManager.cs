using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : SingletonMono<GameManager> {

	public ModelTile startPoint;
	public ModelTile endPoint;
	public Dictionary<ModelTile, bool> listCheckpoint = new Dictionary<ModelTile, bool> ();

	void Start () {}

	void Update () {}

	public void AddCheckpoint (ModelTile t) {
		listCheckpoint[t] = false;
	}
}
