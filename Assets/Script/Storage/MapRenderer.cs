using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MapRenderer : SingletonMono <MapRenderer> {
	
	public ModelMap map;
	
	public Dictionary<int, TileHandler> layerOther = new Dictionary<int, TileHandler> ();
	public Dictionary<int, TileHandler> layerView = new Dictionary<int, TileHandler> ();
	public Dictionary<int, TileHandler> layerSign = new Dictionary<int, TileHandler> ();
	public Dictionary<int, TileHandler> layerRoad = new Dictionary<int, TileHandler> ();

	void Start () {}

	void Update () {}

	public void Init (ModelMap map) {
		this.map = map;

		foreach (KeyValuePair<string, ModelLayer> p in map.layer) {
			LayerType layerType = p.Value.type;

			foreach (KeyValuePair<string, ModelTile> p2 in p.Value.tile) {
				ModelTile tile = p2.Value;
				GameObject go = ModelFactory.Instance.GetNewModel (tile);
				TileHandler handler = go.GetComponent <TileHandler> ();


				if (go != null) {
					go.transform.parent = this.transform;

					switch (layerType) {
					case LayerType.Other:
						layerOther[tile.objId] = handler;
						break;
					case LayerType.View:
						layerView[tile.objId] = handler;
						break;
					case LayerType.Sign:
						layerSign[tile.objId] = handler;
						break;
					case LayerType.Road:
						layerRoad[tile.objId] = handler;
						break;
					}
				} else {
					Debug.Log ("Null prefab: " + tile.typeId + "," + tile.objId);
				}
			}
		}

		//Light
		foreach (KeyValuePair<int, TileHandler> p in layerOther) {
			if (p.Value.tile.typeId == 301) { //light
				TrafficLightHandler light = (TrafficLightHandler) p.Value;

				string idRoad = p.Value.tile.properties[TileKey.LIGHT_LAN_DUONG];
				RoadHandler road = layerRoad[int.Parse (idRoad)] as RoadHandler;
				light.road = road;

				light.Init (p.Value.tile);
				TrafficLightManager.Instance.AddLight (light);
			}
		}
	}
}
