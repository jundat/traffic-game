using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class MapLoader : Singleton <MapLoader> {

	public string json;
	public ModelMap map;

	public ModelMap Load (string path) {
		Object obj = Resources.Load (path);
		json = obj.ToString ();

		map = JsonReader.Deserialize <ModelMap> (json);
		if (map == null) {
			Debug.LogError ("Can not deserilize map!");
		} else {
			//Fix Data
			foreach (KeyValuePair<string, ModelLayer> p1 in map.layer) {
				foreach (KeyValuePair<string, ModelTile> p2 in p1.Value.tile) {
					p2.Value.x *= -1;
					p2.Value.y *= -1;
				}
			}
		}

		return map;
	}
}
