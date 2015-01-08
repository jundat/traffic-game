using UnityEngine;
using System.Collections;

public class MoveInLine : MonoBehaviour {

	public Transform from;
	public Transform to;
	public float v = 0.05f;

	private bool isRun = false;
	private Vector3 step;

	void Start () {}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			isRun = true;
			transform.LookAt (to);
			step = (to.position - from.position) / Vector3.Distance (from.position, to.position);
		}

		if (isRun) {
			Vector3 move = step * v;
			transform.position = transform.position + move;

			if (Vector3.Distance (transform.position, to.position) < 1) {
				Debug.Log ("OK");
				transform.position = to.position;
				isRun = false;
			}
		}
	}
}
