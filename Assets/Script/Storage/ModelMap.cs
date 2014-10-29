using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class ModelMap {
	
	public string name = "";
	public int width = 0;
	public int height = 0;

	public Dictionary<string, string> info = new Dictionary<string, string> ();
	public Dictionary<string, string> state = new Dictionary<string, string> ();
	public Dictionary<string, ModelLayer> layer = new Dictionary<string, ModelLayer> ();
}
