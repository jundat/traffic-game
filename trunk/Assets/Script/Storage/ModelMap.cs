using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class ModelMap {
	public Dictionary<string, string> info;
	public Dictionary<string, string> state;
	public Dictionary<string, ModelLayer> layer;
}

public class MapKey {
	public const string simulateTime = "simulateTime";
}