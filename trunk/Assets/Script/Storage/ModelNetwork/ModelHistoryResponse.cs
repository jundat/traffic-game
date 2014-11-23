using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ModelHistoryResponse : ModelResponse {
	public List<ModelHistoryItem> highscore;
}


public class ModelHistoryItem {
	public long time;
	public int score;
	public ModelCampaignItem campaign;
	public List<string> detail;
}

public class ModelCampaignItem {
	public string name;
	public string map;
}