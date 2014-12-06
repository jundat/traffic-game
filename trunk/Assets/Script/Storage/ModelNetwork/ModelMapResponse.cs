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

	/// <summary>
	/// The time in minute
	/// </summary>
	public float time;
	public string thumnail;

	public ModelMapNetwork Copy () {
		ModelMapNetwork m = new ModelMapNetwork ();
		m.uid = this.uid;
		m.name = this.name;
		m.url = this.url;
		m.level = this.level;
		m.info = this.info;

		m.time = this.time;
		m.thumnail = this.thumnail;

		return m;
	}
}