using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : Singleton <MapManager> {

	public ModelMap map;
	public Vector3 startPoint;
	public Vector3 finishPoint;

	public void Init () {
		map = MapLoader.Instance.Load ("Map/map");
		MapRenderer.Instance.Init (map);
	}
}
