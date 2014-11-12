﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ModelFactory : Singleton <ModelFactory> {


	public ModelFactory () {
		Load ();
	}

	Dictionary <string, GameObject> dictModels = new Dictionary<string, GameObject> ();
	Dictionary<string, Texture> dictTextures = new Dictionary<string, Texture> ();

	#region LOAD
	private void Load () {
		GameObject[] gos = Resources.LoadAll<GameObject> ("Prefabs");
		for (int i = 0; i < gos.Length; ++i) {
			dictModels[gos[i].name] = gos[i];
			//Debug.Log (gos[i].name);
		}

		//Road
		Texture[] tts = Resources.LoadAll <Texture> (Global.ROAD_RES);
		for (int i = 0; i < tts.Length; ++i) {
			dictTextures[tts[i].name] = tts[i];
			//Debug.Log (tts[i].name);
		}

		//Sign
		Texture[] tts2 = Resources.LoadAll <Texture> (Global.SIGN_RES);
		for (int i = 0; i < tts2.Length; ++i) {
			dictTextures[tts2[i].name] = tts2[i];
			//Debug.Log (tts[i].name);
		}
	}
	#endregion

	public GameObject GetNewModel (ModelTile tile) {
		GameObject ins = null;
		switch (tile.layerType) {
		case LayerType.Road:
			ins = InitRoad (tile);
			break;

		case LayerType.Sign:
			ins = InitSign (tile);
			break;

		case LayerType.View:
			ins = InitView (tile);
			break;

		case LayerType.Other:
			ins = InitOther (tile);
			break;
		}

		TileHandler handler = ins.GetComponent<TileHandler>();
		if (handler == null) {
			handler = ins.AddComponent <TileHandler> ();
		}
		handler.tile = tile;

		return ins;
	}

	#region ROAD
	private GameObject InitRoad (ModelTile tile) {
		GameObject ins = null;
		GameObject prefab = null;
		dictModels.TryGetValue ("Road", out prefab);
		if (prefab != null) {
			ins = GameObject.Instantiate (prefab) as GameObject;
			RoadHandler handler = ins.GetComponent<RoadHandler>();
			handler.tile = tile;
			handler.Init ();

			//Texture
			Texture tt = null;
			dictTextures.TryGetValue (tile.typeId+"", out tt);
			if (tt != null) {
				MeshRenderer render = ins.GetComponent<MeshRenderer> ();
				render.material.mainTexture = tt;
			} else {
				Debug.LogError ("Null texture at tile: " + tile.objId);
			}

			//Size + Position
			ins.transform.localScale = new Vector3 (tile.w * Global.SCALE_TILE, 1, tile.h * Global.SCALE_TILE);

			float y1 = ins.transform.localPosition.y;
			float y2 = ins.transform.localPosition.y + Global.DELTA_HEIGH;

			if (tile.typeId >= 8 && tile.typeId <= 11) { //bus
				ins.transform.localPosition = new Vector3 (tile.x * Global.SCALE_TILE * Global.SCALE_SIZE, 
				                                           y2, 
				                                           tile.y * Global.SCALE_TILE * Global.SCALE_SIZE);
			} else { //road
				ins.transform.localPosition = new Vector3 (tile.x * Global.SCALE_TILE * Global.SCALE_SIZE, 
				                                           y1, 
				                                           tile.y * Global.SCALE_TILE * Global.SCALE_SIZE);
			}

		} else {
			return null;
		}

		return ins;
	}
	#endregion

	#region SIGN
	private GameObject InitSign (ModelTile tile) {
		GameObject ins = null;
		GameObject prefab = null;
		dictModels.TryGetValue ("Sign", out prefab);
		if (prefab != null) {
			ins = GameObject.Instantiate (prefab) as GameObject;
			SignHandler handler = ins.GetComponent <SignHandler> ();
			handler.tile = tile;
			
			//Texture
			Texture tt = null;
			dictTextures.TryGetValue (tile.typeId+"", out tt);
			if (tt != null) {
				handler.SetSign (tt);
			} else {
				Debug.LogError ("Null texture at tile: " + tile.typeId + "," + tile.objId);
			}

			//Position
			ins.transform.localPosition = new Vector3 (tile.x * Global.SCALE_TILE * Global.SCALE_SIZE, ins.transform.localPosition.y, tile.y * Global.SCALE_TILE * Global.SCALE_SIZE);

			//Rotation
			int rot = 0;
			MoveDirection dir = Ultil.ToMoveDirection (tile.properties[TileKey.SIGN_DIR]);

			switch (dir) {
			case MoveDirection.UP:
				rot = 0;
				break;
			case MoveDirection.RIGHT:
				rot = 90;
				break;
			case MoveDirection.DOWN:
				rot = 180;
				break;
			case MoveDirection.LEFT:
				rot = 270;
				break;
			}
			ins.transform.localRotation = Quaternion.Euler(0, rot, 0);

			//TExt Sign
			string text = "";
			Color textColor = Color.black;

			switch (tile.typeId) {
			case 103: //Toc Do Toi Da
				textColor = Color.black;
				text = Ultil.GetString (TileKey.SIGN_MAX_TOCDO, ""+Global.DEF_MAX_TOCDO, tile.properties); 
				break;

			case 128: //Het Toc Do Toi Da
				textColor = Color.black;
				text = Ultil.GetString (TileKey.SIGN_MAX_TOCDO, ""+Global.DEF_MAX_TOCDO, tile.properties); 
				break;

			case 138: //Toc Do Toi Thieu
				textColor = Color.white;
				text = Ultil.GetString (TileKey.SIGN_MIN_TOCDO, ""+Global.DEF_MIN_TOCDO, tile.properties); 
				break;

			case 139: //Het Toc Do Toi Da
				textColor = Color.white;
				text = Ultil.GetString (TileKey.SIGN_MIN_TOCDO, ""+Global.DEF_MIN_TOCDO, tile.properties); 
				break;
			}

			if (text.Length > 0) {
				handler.SetText (text, textColor);
			}

		} else {
			return null;
		}
		
		return ins;
	}
	#endregion

	#region VIEW
	private GameObject InitView (ModelTile tile) {
		GameObject ins = null;
		GameObject prefab = null;
		dictModels.TryGetValue (""+tile.typeId, out prefab);
		if (prefab != null) {
			ins = GameObject.Instantiate (prefab) as GameObject;
			ins.transform.localPosition = new Vector3 (tile.x * Global.SCALE_TILE * Global.SCALE_SIZE, ins.transform.localPosition.y, tile.y * Global.SCALE_TILE * Global.SCALE_SIZE);			
		} else {
			return null;
		}
		
		return ins;
	}
	#endregion
	
	#region OTHER
	private GameObject InitOther (ModelTile tile) {
		GameObject ins = null;
		GameObject prefab = null;

		dictModels.TryGetValue (""+tile.typeId, out prefab);
		if (prefab != null) {
			ins = GameObject.Instantiate (prefab) as GameObject;
			ins.transform.localPosition = new Vector3 (tile.x * Global.SCALE_TILE * Global.SCALE_SIZE, ins.transform.localPosition.y, tile.y * Global.SCALE_TILE * Global.SCALE_SIZE);

			if (tile.typeId == 301) { //Light
				TrafficLightHandler handler = ins.GetComponent<TrafficLightHandler>();
				handler.tile = tile;

				//Rotation
				int rot = 0;
				MoveDirection huong = Ultil.ToMoveDirection ( tile.properties[TileKey.LIGHT_HUONG]);
				
				switch (huong) {
				case MoveDirection.DOWN:
					rot = 0;
					break;
				case MoveDirection.LEFT:
					rot = 90;
					break;
				case MoveDirection.UP:
					rot = 180;
					break;
				case MoveDirection.RIGHT:
					rot = 270;
					break;
				}
				ins.transform.localRotation = Quaternion.Euler(0, rot, 0);
			}
		} else {
			return null;
		}
		
		return ins;
	}
	#endregion
}
