using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class ModelMap {
	
	public string name = "";
	public int width = 0;
	public int height = 0;

	public Dictionary<string, string> info;
	public Dictionary<string, string> state;
	public Dictionary<string, ModelLayer> layer;
}
