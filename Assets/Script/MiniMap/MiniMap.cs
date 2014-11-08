using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class MiniMap : SingletonMono<MiniMap> {

	public Camera cameraMiniMap;
	public int MiniMapLayer;
	public GameObject layerRoad;
	public GameObject layerSign;
	public GameObject layerView;
	public GameObject layerOther;
	public UITexture ttMiniMap;

	string json;
	private bool isBigMap = false;
	private const int SMALL_WIDTH = 320;
	private const int SMALL_HEIGHT = 160;
	private const int BIG_WIDTH = 960;
	private const int BIG_HEIGHT = 480;
	private const float SMALL_CAMERA = 2.5f;
	private const float BIG_CAMERA = 4;

	void Start () {

	}

	void Update () {
		float x = PlayerHandler.Instance.transform.localPosition.x;
		float y = PlayerHandler.Instance.transform.localPosition.z;

		Vector2 v = Ultil.ParseReal2Map (x, y);
		float z = cameraMiniMap.transform.localPosition.z;
		cameraMiniMap.transform.localPosition = new Vector3 (-v.x, -v.y, z);
	}

	public void Import (string s) {
		Dictionary<string, object>  total = JsonReader.Deserialize <Dictionary<string, object>> (s);

		//Layers
		Dictionary<string, object> layers = total["layer"] as Dictionary<string, object>;
		if (layers != null) {

			foreach (KeyValuePair<string, object> p in layers) {
				//p = 1 layer
				Dictionary<string, object> layer = p.Value as Dictionary<string, object>;
				int layerId = int.Parse (layer["id"].ToString ());
				string name = layer["name"].ToString ();
				LayerType layerType = (LayerType) Enum.Parse (typeof (LayerType), layer["type"].ToString ());

				//Tiles
				Dictionary<string, object> tiles = layer["tile"] as Dictionary<string, object>;

				//Tile
				foreach (KeyValuePair<string, object> p2 in tiles) {
					ModelTile t = JsonReader.Deserialize<ModelTile> (JsonWriter.Serialize (p2.Value));

					if (t.layerType != LayerType.View) {
						GridTileHandler gt = AddNewObject (t, layerId);
					}

					//Increase Unique Tile Id
					Ultil.ResetObjId (t.objId);
				}
			}
		} else {
			Debug.Log ("Null layer dictionary");
		}
	}

	private GameObject GetLayer (int layerId) {
		switch (layerId) {
		case 1:
			return layerOther;
			break;
			
		case 2:
			return layerView;
			break;
			
		case 3:
			return layerSign;
			break;
			
		case 4:
			return layerRoad;
			break;
		}
		
		Debug.LogError ("Wrong layer");
		
		return null;
	}
	
	public GridTileHandler AddNewObject (ModelTile tile, int layerId) {
		if (layerId > 0) {
			GameObject layer = GetLayer (layerId);
			
			GameObject ins = new GameObject ();
			ins.layer = MiniMapLayer;
			
			GridTileHandler gt = ins.AddComponent <GridTileHandler>();
			gt.Init (tile);
			
			//--------------------------------------------------
			
			gt.transform.parent = layer.transform;
			gt.transform.localPosition = new Vector2 (tile.x, tile.y);
			gt.transform.localScale = Vector2.one;
			
			return gt;
		}
		
		return null;
	}

	public void OnClickMap () {
		isBigMap = ! isBigMap;

		if (isBigMap) {
			ttMiniMap.width = BIG_WIDTH;
			ttMiniMap.height = BIG_HEIGHT;
			cameraMiniMap.orthographicSize = BIG_CAMERA;
		} else {
			ttMiniMap.width = SMALL_WIDTH;
			ttMiniMap.height = SMALL_HEIGHT;
			cameraMiniMap.orthographicSize = SMALL_CAMERA;
		}
	}

}
