using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestDrawCall : MonoBehaviour {

	public GameObject prefab;
	public int number;

	List<GameObject> list = new List<GameObject> ();


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Clear () {
		for (int i = 0; i < list.Count; ++i) {
			DestroyImmediate (list[i]);
		}
	}

	public void Generate () {
		for (int i = 0; i < number; ++i) {
			GameObject go = GameObject.Instantiate (prefab) as GameObject;
			go.transform.position = new Vector3 (Ultil.random.Next (0, 100), 0, -Ultil.random.Next (0, 100));

			list.Add (go);
		}
	}
}
