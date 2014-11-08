using UnityEngine;
using System;
using System.Collections;

public class GridTileHandler : MonoBehaviour {

	public static int ROAD_DEPTH = -1000;
	public static int SIGN_DEPTH = -500;
	public static int VIEW_DEPTH = -0;

	public ModelTile tile;

	void Start () {}
	void Update () {}

	bool isSelected = false;

	public void Select (bool select) {
		isSelected = select;
		if (select) {
			GetComponent<UITexture>().color = new Color (1.0f,0.5f,0.5f,1);
		} else {
			GetComponent<UITexture>().color = Color.white;
		}
	}

	public void Init (ModelTile tile) {
		this.tile = tile;
		gameObject.name = ""+tile.objId;
		
		UITexture tt = gameObject.AddComponent <UITexture>();
		tt.mainTexture = TileManager.Instance.GetTexture (tile.typeId+"");
		tt.width = (int)tile.w;
		tt.height = (int)tile.h;
		tt.color = Color.white;

		int depth = ROAD_DEPTH;
		switch (tile.layerType) {
		case LayerType.Road:
			depth = ROAD_DEPTH;
			break;
		case LayerType.Sign:
			depth = SIGN_DEPTH;
			break;
		case LayerType.View:
			depth = VIEW_DEPTH;
			break;
		case LayerType.Other:
			depth = SIGN_DEPTH;
			break;
		}
		depth += tile.objId;
		tt.depth = depth;
	}
}
