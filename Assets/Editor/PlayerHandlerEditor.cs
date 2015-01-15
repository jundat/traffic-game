using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(PlayerHandler))]
public class PlayerHandlerEditor : Editor {

	public RoadHandler testroad = null;

	public override void OnInspectorGUI () {
		DrawDefaultInspector();
		
		PlayerHandler player = (PlayerHandler)target;
		
		GUILayoutOption[] gg = new GUILayoutOption[] {};
		
		if (GUILayout.Button ("TestRoad", gg)) {
			Vector2 p = new Vector2 ();
			p.x = player.transform.position.x;
			p.y = player.transform.position.z;
			testroad = MapRenderer.Instance.GetRoadNotBusAt (p);
		}
	}
}