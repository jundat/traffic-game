using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(BikeMovement))]
public class BikeMovementEditor : Editor {

	string s = "";
	public override void OnInspectorGUI () {
		DrawDefaultInspector();
		
		BikeMovement bike = (BikeMovement)target;
		
		GUILayoutOption[] gg = new GUILayoutOption[] {};
		
		if (GUILayout.Button ("Speed", gg)) {
			s = "" + bike.Speed;
		}

		GUILayout.Label (s, gg);
	}
}