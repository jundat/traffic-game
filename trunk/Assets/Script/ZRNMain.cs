using UnityEngine;
using System.Collections;

public class ZRNMain : MonoBehaviour {

	public static bool IsCollider = true;

	// Use this for initialization
	void Start () {
		MeshRenderer[] meshes = FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
		Debug.Log ("TotalMesh: " + meshes.Length);

		foreach (MeshRenderer m in meshes) {
			if (m.gameObject.GetComponent<MeshCollider>() == null) {
				m.gameObject.AddComponent (typeof(MeshCollider));
			}
		}
	}
	
	void OnGUI ()
	{
		if (GUI.Button (new Rect(Screen.width-100,Screen.height-40,100,40),"Collider:"+IsCollider)) {
			IsCollider = ! IsCollider;
			RefreshCollider ();
		}
	}

	void RefreshCollider ()
	{
		MeshCollider[] collider = FindObjectsOfType(typeof(MeshCollider)) as MeshCollider[];
		Debug.Log ("TotalCollider: " + collider.Length);

		foreach (MeshCollider m in collider) {
			m.enabled = IsCollider;
		}
	}
}
