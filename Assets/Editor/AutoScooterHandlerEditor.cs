using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(AutoScooterHandler))]
public class AutoScooterHandlerEditor : Editor {

	public override void OnInspectorGUI () {
		DrawDefaultInspector();
		
		AutoScooterHandler car = (AutoScooterHandler)target;

		GUILayoutOption[] gg = new GUILayoutOption[] {};
		
		if (GUILayout.Button ("CheckRoad", gg)) {
			car.ScheduleUpdate ();
		}
	}
}