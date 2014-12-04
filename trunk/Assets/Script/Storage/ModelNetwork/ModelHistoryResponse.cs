using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ModelHistoryResponse : ModelResponse {
	public List<ModelHistoryItem> highscore;
}

[System.Serializable]
public class ModelHistoryItem {
	public long time;
	public int score;
	public string map;
	public string detail;
}
