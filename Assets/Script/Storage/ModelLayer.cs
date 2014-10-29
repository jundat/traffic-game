using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LayerType {
	Road,
	Sign,
	View,
	Other
}

public class ModelLayer {

	public LayerType type;
	public string name;
	public int id;

	public Dictionary<string, ModelTile> tile = new Dictionary<string, ModelTile> ();
}
