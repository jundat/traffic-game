using UnityEngine;
using System.Collections;

public class FinishTrigger : MonoBehaviour {

	void Start () {}

	void Update () {}

	void OnTriggerEnter(Collider other) {
		Debug.LogError ("Collide: " + other.name);
	}
}
