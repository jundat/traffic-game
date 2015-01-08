using UnityEngine;
using System.Collections;

public class MoveInCircle : MonoBehaviour {

	public GameObject center;
	public GameObject dest;

	private bool isRun = false;
	private float angle = 0;
	private float r = 5;
	private float cx = 0;
	private float cz = 0;
	public float v = 1;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			isRun = ! isRun;

			if (isRun) {
				r = Vector3.Distance (transform.position, dest.transform.position) / 2.0f;
				Vector3 pCenter = (transform.position + dest.transform.position) / 2;
				center.transform.position = pCenter;
				cx = pCenter.x;
				cz = pCenter.z;

				float x = transform.position.x;
				float z = transform.position.z;
				float sin = (x - cx) / r;
				float cos = (z - cz) / r;

				float aSin = Mathf.Asin (sin);
				float aCos = Mathf.Acos (cos);

				//Debug.Log ("Asin: " + aSin);
				//Debug.Log ("Acos: " + aCos);

				float nxx = cx + Mathf.Sin (aSin) * r;
				float nxz = cz + Mathf.Cos (aSin) * r;

				if (sin == Mathf.Sin (aCos)) {
					angle = aCos / v;
				} else {
					angle = aCos / v;
				}

				if (sin < 0) {
					angle = - angle;
				}
			}
		}

		if (isRun) {
			angle += v * Time.deltaTime;

			float x = cx + Mathf.Sin (angle) * r;
			float z = cz + Mathf.Cos (angle) * r;

			Vector3 pos = new Vector3 (0, 0, 0);
			pos.x = x;
			pos.z = z;

			transform.position = pos;
			Quaternion q = new Quaternion ();
			if (v > 0) {
				q.eulerAngles = new Vector3 (0, 90 + angle * 180 / Mathf.PI, 0);
			} else {
				q.eulerAngles = new Vector3 (0, -90 + angle * 180 / Mathf.PI, 0);
			}

			transform.rotation = q;

//			if (Vector3.Distance (transform.position, dest.transform.position) < 0.5f) {
//				Debug.LogError ("STOP");
//				transform.position = dest.transform.position;
//				isRun = false;
//				v = 0;
//			}
		}
	}

	float EPSILON = 0.1f;
	public bool CheckZero (float x) {
		return Mathf.Abs (v) < EPSILON;
	}
}
