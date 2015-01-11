using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(AutoVehicleHandler))]
public class AutoCarHandlerEditor : Editor {

	public override void OnInspectorGUI () {
		DrawDefaultInspector();
		
		AutoVehicleHandler car = (AutoVehicleHandler)target;

		GUILayoutOption[] gg = new GUILayoutOption[] {};
		
		if (GUILayout.Button ("CheckRoad", gg)) {
			car.CheckRoad ();
		}
	}
}