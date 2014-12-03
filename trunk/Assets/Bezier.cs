using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bezier : MonoBehaviour {

	public List<Transform> points;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.Space) == true) {

			List<Vector3> l = new List<Vector3> ();
			for (int i = 0; i < points.Count; ++i) {
				l.Add (points[i].transform.position);
			}
			
			Vector3[] vs = l.ToArray ();

			iTween.MoveTo(this.gameObject, iTween.Hash(
				"easetype", iTween.EaseType.linear,
				"time", 5,
				"movetopath", true,
				"path", vs
			));
		}
	}
}
