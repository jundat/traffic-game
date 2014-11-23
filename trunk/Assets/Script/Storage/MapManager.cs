using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MapManager : Singleton <MapManager> {

	public ModelMap map;
	public Vector3 startPoint;
	public Vector3 finishPoint;

	public void LoadFile (string mapfile) {
		map = MapLoader.Instance.LoadFile (mapfile);
	}

	public void LoadJSON (string mapjson) {
		map = MapLoader.Instance.LoadJSON (mapjson);
	}

	public void Init () {

		if (map != null) {
			//Fix Data
			foreach (KeyValuePair<string, ModelLayer> p1 in map.layer) {
				foreach (KeyValuePair<string, ModelTile> p2 in p1.Value.tile) {
					p2.Value.x *= -1;
					p2.Value.y *= -1;
				}
			}
			
			//Simulate Time
			DateTime d = DateTime.Now;
			int h = int.Parse (Ultil.GetString (MapKey.simulateTime, "8", map.info));
			d = d.AddHours (-d.Hour);
			d = d.AddMinutes(-d.Minute);
			d = d.AddHours(h);
			Main.Instance.SetStartTime (d);

			MapRenderer.Instance.Init (map);
			MiniMap.Instance.Import (MapLoader.Instance.json);
		} else {
			Debug.Log ("Load map file before start game");
		}
	}
}
