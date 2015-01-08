using UnityEngine;
using System;
using System.Collections;

public class AutocarCollider : MonoBehaviour {

	public const string LEFT 		= "ColLeft";
	public const string RIGHT 		= "ColRight";
	public const string FRONT 		= "ColFront";
	public const string BACK 		= "ColBack";
	public const string FAR_FRONT 	= "ColFarFront";

	public Action<Collider, AutocarCollider> onCollideEnter;
	public Action<Collider, AutocarCollider> onCollideExit;

	void Start () {}

	void Update () {}

	void OnTriggerEnter (Collider other) {
		if (onCollideEnter != null) {
			onCollideEnter (other, this);
		}
	}
	
	void OnTriggerExit (Collider other) {
		if (onCollideExit != null) {
			onCollideExit (other, this);
		}
	}
}
