using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveInBezier : MonoBehaviour {

	public List<Transform> listPos = new List<Transform> ();
	public List<GameObject> listpoint = new List<GameObject> ();

	// Use this for initialization
	void Start () {

	}

	void Draw () {
		for (int i = 0; i < listpoint.Count; ++i) {
			Destroy (listpoint[i]);
		}

		BezierCurve bc = new BezierCurve ();
		
		List<double> ptList = new List<double>();
		for (int i = 0; i < listPos.Count; ++i) {
			ptList.Add (listPos[i].position.x);
			ptList.Add (listPos[i].position.z);
		}
		
		// how many points do you need on the curve?
		const int POINTS_ON_CURVE = 40;
		
		double[] ptind = new double[ptList.Count];
		double[] p = new double[POINTS_ON_CURVE];
		ptList.CopyTo (ptind, 0);
		
		bc.Bezier2D(ptind, (POINTS_ON_CURVE) / 2, p);
		
		// draw points
		for (int i = 1; i != POINTS_ON_CURVE-1; i += 2)
		{
			//p[i+1]
			//p[i]
			GameObject ins = GameObject.CreatePrimitive(PrimitiveType.Cube);
			ins.transform.position = new Vector3 ((float)p[i+1], 0, (float)p[i]);

			listpoint.Add (ins);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Draw ();
		}
	}
}
