using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MapManager : Singleton <MapManager> {

	public ModelMap mapFix;
	public ModelMap mapOgirgin;
	public Vector3 startPoint;
	public Vector3 finishPoint;

	public void LoadFile (string mapfile) {
		mapFix = MapLoader.Instance.LoadFile (mapfile);
		mapOgirgin = MapLoader.Instance.LoadFile (mapfile);
	}

	public void LoadJSON (string mapjson) {
		mapFix = MapLoader.Instance.LoadJSON (mapjson);
		mapOgirgin = MapLoader.Instance.LoadJSON (mapjson);
	}

	public void Init () {

		if (mapFix != null && mapOgirgin != null) {
			//Fix Data Map
			foreach (KeyValuePair<string, ModelLayer> p1 in mapFix.layer) {
				foreach (KeyValuePair<string, ModelTile> p2 in p1.Value.tile) {
					p2.Value.x *= -1;
					p2.Value.y *= -1;
				}
			}
			
			//Simulate Time
			DateTime d = DateTime.Now;
			int h = int.Parse (Ultil.GetString (MapKey.simulateTime, "8", mapFix.info));
			d = d.AddHours (-d.Hour);
			d = d.AddMinutes(-d.Minute);
			d = d.AddHours(h);
			Main.Instance.SetStartTime (d);
			MapRenderer.Instance.Init (mapFix);
			MiniMap.Instance.Import (mapOgirgin);

			//Get Collision Road
			foreach (KeyValuePair <int, TileHandler> p in MapRenderer.Instance.layerRoad) {
				RoadHandler r = (RoadHandler) p.Value;
				r.FetchCollisionRoad ();
			}

		} else {
			Debug.Log ("Load map file before start game");
		}
	}
}
