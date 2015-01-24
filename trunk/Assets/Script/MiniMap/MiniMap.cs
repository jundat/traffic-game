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
	public float SMALL_CAMERA = 4.0f;
	public float BIG_CAMERA = 8.0f;

	private Dictionary<int, GridTileHandler> dictCheckpoint = new Dictionary<int, GridTileHandler> ();
	private bool isBigMap = false;
	private const int SMALL_WIDTH = 160;
	private const int SMALL_HEIGHT = 160;
	private const int BIG_WIDTH = 640;
	private const int BIG_HEIGHT = 640;

	void Start () {
		cameraMiniMap.orthographicSize = SMALL_CAMERA;
		dictCheckpoint.Clear ();
	}

	void Update () {
		float x = PlayerHandler.Instance.transform.localPosition.x;
		float y = PlayerHandler.Instance.transform.localPosition.z;

		Vector2 v = Ultil.ParseReal2Map (x, y);
		float z = cameraMiniMap.transform.localPosition.z;
		cameraMiniMap.transform.localPosition = new Vector3 (-v.x, -v.y, z);

		//Rotation ----------------

		float roty = PlayerHandler.Instance.transform.localEulerAngles.y;
		Vector3 oldrot = cameraMiniMap.transform.localEulerAngles;
		Vector3 newrot = new Vector3 (oldrot.x, oldrot.y, 180-roty);
		cameraMiniMap.transform.localEulerAngles = newrot;
	}

	public void Import (ModelMap map) {
		foreach (KeyValuePair<string, ModelLayer> p in map.layer) {
			ModelLayer layer = p.Value;
			foreach (KeyValuePair<string, ModelTile> p2 in layer.tile) {
				ModelTile tile = p2.Value;
				if (tile.layerType != LayerType.View) {
					GridTileHandler gt = AddNewObject (tile, layer.id);

					if (tile.typeId == TileID.CHECK_POINT) {
						dictCheckpoint[tile.objId] = gt;
					}
				}
				Ultil.ResetObjId (tile.objId);
			}
		}
	}

	private GameObject GetLayer (int layerId) {
		switch (layerId) {
		case 1:
			return layerOther;
			
		case 2:
			return layerView;
			
		case 3:
			return layerSign;
			
		case 4:
			return layerRoad;
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

	public void CompleteCheckpoint (int objId) {
		GridTileHandler gt = null;
		dictCheckpoint.TryGetValue (objId, out gt);
		if (gt != null) {
			gt.gameObject.SetActive (false);
		}
	}

	public void OnClickMap () {
		isBigMap = ! isBigMap;

		if (isBigMap) {
			ttMiniMap.pivot = UIWidget.Pivot.TopLeft;

			ttMiniMap.width = BIG_WIDTH;
			ttMiniMap.height = BIG_HEIGHT;
			cameraMiniMap.orthographicSize = BIG_CAMERA;

			ttMiniMap.pivot = UIWidget.Pivot.Center;
			BoxCollider box = ttMiniMap.GetComponent<BoxCollider> ();
			box.center = Vector3.zero;
		} else {
			ttMiniMap.pivot = UIWidget.Pivot.TopLeft;

			ttMiniMap.width = SMALL_WIDTH;
			ttMiniMap.height = SMALL_HEIGHT;
			cameraMiniMap.orthographicSize = SMALL_CAMERA;

			ttMiniMap.pivot = UIWidget.Pivot.Center;
			BoxCollider box = ttMiniMap.GetComponent<BoxCollider> ();
			box.center = Vector3.zero;
		}
	}

}
