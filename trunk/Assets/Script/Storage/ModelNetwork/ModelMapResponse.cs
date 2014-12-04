using System;
using System.Collections;
using System.Collections.Generic;

public class ModelMapResponse : ModelResponse {

	public List<ModelMapNetwork> maps;

}

[System.Serializable]
public class ModelMapNetwork {

	public string uid;
	public string name;
	public string url;
	public int level;
	public string info;
	public int time; //in minute
	public string thumnail;

}