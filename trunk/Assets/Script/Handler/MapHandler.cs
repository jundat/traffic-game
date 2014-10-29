//using UnityEngine;
//using System;
//using System.IO;
//using System.Collections;
//using System.Collections.Generic;
//using Pathfinding.Serialization.JsonFx;
//
//public class MapHandler : MonoBehaviour {
//
//	void Start () {}
//	void Update () {}
//
//	public void Init (string json) {
//		Import (json);
//	}
//
//	public void Import (string s) {
//
//		Dictionary<string, object>  total = JsonReader.Deserialize <Dictionary<string, object>> (s);
//		
//		//Info
//		Dictionary<string, object> info = total["info"] as Dictionary<string, object>;
//		if (info != null) {
//			Global.currentMap.name = (string) info["name"];
//			Global.currentMap.width = (int) info["width"];
//			Global.currentMap.height = (int) info["height"];
//		}
//		
//		//Layers
//		Dictionary<string, object> layers = total["layer"] as Dictionary<string, object>;
//		if (layers != null) {
//			
//			foreach (KeyValuePair<string, object> p in layers) {
//				//p = 1 layer
//				Dictionary<string, object> layer = p.Value as Dictionary<string, object>;
//				int layerId = int.Parse (layer["id"].ToString ());
//				string name = layer["name"].ToString ();
//				LayerType layerType = (LayerType) Enum.Parse (typeof (LayerType), layer["type"].ToString ());
//				
//				//New Layer
//				Debug.Log ("--------------------");
//				Debug.Log ("Layer: " + name + "," + layerId + "," + layerType);
//				
//				//Tiles
//				Dictionary<string, object> tiles = layer["tile"] as Dictionary<string, object>;
//				
//				//Tile
//				foreach (KeyValuePair<string, object> p2 in tiles) {
//					ModelTile t = JsonReader.Deserialize<ModelTile> (JsonWriter.Serialize (p2.Value));
//					Debug.Log ("Tile: " + JsonWriter.Serialize (t));
//				}
//			}
//		}
//	}
//}
