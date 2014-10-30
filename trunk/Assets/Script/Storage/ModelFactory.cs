using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ModelFactory : Singleton <ModelFactory> {

	public ModelFactory () {
		Load ();
	}

	Dictionary <string, GameObject> dictModels = new Dictionary<string, GameObject> ();
	Dictionary<string, Texture> dictTextures = new Dictionary<string, Texture> ();

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
			break;

		case LayerType.Other:
			break;
		}

		return ins;
	}

	private GameObject InitRoad (ModelTile tile) {

		GameObject ins = null;

		GameObject prefab = null;
		dictModels.TryGetValue ("Road", out prefab);
		if (prefab != null) {
			ins = GameObject.Instantiate (prefab) as GameObject;
			
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
			ins.transform.localPosition = new Vector3 (tile.x * Global.SCALE_TILE * Global.SCALE_SIZE, 0, tile.y * Global.SCALE_TILE * Global.SCALE_SIZE);

		} else {
			return null;
		}

		return ins;
	}

	private GameObject InitSign (ModelTile tile) {

		GameObject ins = null;

		GameObject prefab = null;
		dictModels.TryGetValue ("Sign", out prefab);
		if (prefab != null) {
			ins = GameObject.Instantiate (prefab) as GameObject;
			SignHandler handler = ins.GetComponent <SignHandler> ();
			
			//Texture
			Texture tt = null;
			dictTextures.TryGetValue (tile.typeId+"", out tt);
			if (tt != null) {
				handler.SetSign (tt);
			} else {
				Debug.LogError ("Null texture at tile: " + tile.objId);
			}

			
			//Size + Position
			//ins.transform.localScale = new Vector3 (tile.w * Global.SCALE_TILE, 1, tile.h * Global.SCALE_TILE);
			ins.transform.localPosition = new Vector3 (tile.x * Global.SCALE_TILE * Global.SCALE_SIZE, 0, tile.y * Global.SCALE_TILE * Global.SCALE_SIZE);

			//Rotation
			int rot = 0;
			switch (tile.properties[TileKey.SIGN_DIR]) {
			case MyDirection.UP:
				rot = 0;
				break;
			case MyDirection.RIGHT:
				rot = 90;
				break;
			case MyDirection.DOWN:
				rot = 180;
				break;
			case MyDirection.LEFT:
				rot = 270;
				break;
			}
			ins.transform.localRotation = Quaternion.Euler(0, rot, 0);

		} else {
			return null;
		}
		
		return ins;
	}
}
