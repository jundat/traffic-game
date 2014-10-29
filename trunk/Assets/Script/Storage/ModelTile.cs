using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModelTile {

	public int objId;
	public int typeId;
	public LayerType layerType;
	public float x;
	public float y;
	public float w;
	public float h;
	public Dictionary<string, string> properties = new Dictionary<string, string> ();
}
