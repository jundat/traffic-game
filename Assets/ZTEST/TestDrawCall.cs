using UnityEngine;
using System.Collections;

public class TestDrawCall : MonoBehaviour {

	public GameObject prefab;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 100; ++i) {
			GameObject go = GameObject.Instantiate (prefab) as GameObject;
			go.transform.position = new Vector3 (Ultil.random.Next (0, 100), 0, -Ultil.random.Next (0, 100));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
