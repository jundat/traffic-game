using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MapRenderer : SingletonMono <MapRenderer> {
	
	public ModelMap map;

	void Start () {}

	void Update () {}

	public void Init (ModelMap map) {
		this.map = map;

		foreach (KeyValuePair<string, ModelLayer> p in map.layer) {
			foreach (KeyValuePair<string, ModelTile> p2 in p.Value.tile) {
				ModelTile tile = p2.Value;
				GameObject go = ModelFactory.Instance.GetNewModel (tile);
				if (go != null) {
					go.transform.parent = this.transform;
				}
			}
		}
	}
}
