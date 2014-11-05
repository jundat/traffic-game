using UnityEngine;
using System.Collections;

public class FollowerActive : MonoBehaviour {

	public GameObject target;

	void Start () {}

	void Update () {}

	void OnEnable() {
		target.SetActive (true);
	}

	void OnDisable() {
		target.SetActive (false);
	}
}
