using UnityEngine;
using System.Collections;

public class MapManager : Singleton <MapManager> {

	public ModelMap map;

	public void Init () {
		map = MapLoader.Instance.Load ("Map/map");
		MapRenderer.Instance.Init (map);
	}
}
