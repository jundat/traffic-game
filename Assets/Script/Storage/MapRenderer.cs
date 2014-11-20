using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MapRenderer : SingletonMono <MapRenderer> {
	
	public Terrain terrain;
	public float terrainY = -0.2f;

	public ModelMap map;
	
	public Dictionary<int, TileHandler> layerOther = new Dictionary<int, TileHandler> ();
	public Dictionary<int, TileHandler> layerView = new Dictionary<int, TileHandler> ();
	public Dictionary<int, TileHandler> layerSign = new Dictionary<int, TileHandler> ();
	public Dictionary<int, TileHandler> layerRoad = new Dictionary<int, TileHandler> ();

	void Start () {}

	void Update () {}

	public void Init (ModelMap map) {
		this.map = map;

		int width = int.Parse (Ultil.GetString ("width", "1", map.info));
		int height = int.Parse (Ultil.GetString ("height", "1", map.info));
		terrain.terrainData.size = new Vector3 (width * 16, 1, height * 16);
		terrain.transform.localPosition = new Vector3(-width * 8, terrainY, -height * 8);

		foreach (KeyValuePair<string, ModelLayer> p in map.layer) {
			LayerType layerType = p.Value.type;

			foreach (KeyValuePair<string, ModelTile> p2 in p.Value.tile) {
				ModelTile tile = p2.Value;

				if (Global.BUILD_BUIDING == false) {
					if (tile.layerType == LayerType.View && tile.typeId != 203) {
						continue;
					}
				}

				GameObject go = ModelFactory.Instance.GetNewModel (tile);
				if (go == null) {
					continue;
				}

				if (go != null) {
					TileHandler handler = go.GetComponent <TileHandler> ();
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
					default:
						Debug.LogError ("Wrong: " + tile.typeId);
						break;
					}
				} else {
					Debug.Log ("Null prefab: " + tile.typeId + "," + tile.objId);
				}
			}
		}

		//Light
		foreach (KeyValuePair<int, TileHandler> p in layerOther) {
			ModelTile tile = p.Value.tile;

			switch (tile.typeId) {
			case 301: //light
			{
				TrafficLightHandler light = (TrafficLightHandler) p.Value;
				
				string idRoad = p.Value.tile.properties[TileKey.LIGHT_LAN_DUONG];
				RoadHandler road = layerRoad[int.Parse (idRoad)] as RoadHandler;
				light.road = road;
				
				light.Init (tile);
				TrafficLightManager.Instance.AddLight (light);
				break;
			}

			case 302: //start point
			{
				Vector2 newpos2d = Ultil.ParseMap2Real (tile.x, tile.y);
				MapManager.Instance.startPoint = new Vector3 (newpos2d.x, 0, newpos2d.y);
				Vector3 oldpos = PlayerHandler.Instance.transform.localPosition;
				PlayerHandler.Instance.transform.localPosition = new Vector3 (newpos2d.x, oldpos.y, newpos2d.y);
				break;
			}
				
			case 303: //finish point
			{
				Vector2 newpos2d = Ultil.ParseMap2Real (tile.x, tile.y);
				MapManager.Instance.finishPoint = new Vector3 (newpos2d.x, 0, newpos2d.y);
				break;
			}
			}
		}
	}
}
