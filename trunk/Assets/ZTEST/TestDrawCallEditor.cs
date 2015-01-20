//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//
//[CustomEditor(typeof(TestDrawCall))]
//public class TestDrawCallEditor : Editor {
//
//	public override void OnInspectorGUI () {
//		DrawDefaultInspector();
//		
//		TestDrawCall player = (TestDrawCall)target;
//		
//		GUILayoutOption[] gg = new GUILayoutOption[] {};
//		
//		if (GUILayout.Button ("Generate", gg)) {
//			player.Generate ();
//		}
//
//		if (GUILayout.Button ("Clear", gg)) {
//			player.Clear ();
//		}
//	}
//}