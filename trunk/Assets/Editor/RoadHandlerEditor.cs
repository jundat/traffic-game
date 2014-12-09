using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(RoadHandler))]
public class RoadHandlerEditor : Editor {
	string s = "";

	public override void OnInspectorGUI () {
		DrawDefaultInspector();
		
		RoadHandler road = (RoadHandler)target;

		if (s.Length == 0) {
			if(GUILayout.Button ("Properties")) {
				foreach (KeyValuePair<string,string> p in road.tile.properties) {
					s += p.Key + " : " + p.Value + "\n";
				}
			}
		} else {
			if(GUILayout.Button ("Properties")) {
				s = "";
			}
		}

		GUILayoutOption[] gg = new GUILayoutOption[] {};

		GUILayout.Label (s, gg);
	}
}