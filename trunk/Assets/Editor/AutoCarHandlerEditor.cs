using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(AutoCarHandler))]
public class AutoCarHandlerEditor : Editor {

	public override void OnInspectorGUI () {
		DrawDefaultInspector();
		
		AutoCarHandler car = (AutoCarHandler)target;

		GUILayoutOption[] gg = new GUILayoutOption[] {};
		
		if (GUILayout.Button ("CheckRoad", gg)) {
			car.ScheduleUpdate ();
		}
	}
}