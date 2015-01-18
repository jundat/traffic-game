using UnityEngine;
using System.Collections;

public class HelmetHandler : MonoBehaviour {

	public BikeHandler bikeHandler;
	private Animator anim;

	void Awake () {
		anim = this.GetComponent<Animator>();
	}

	void Update () {}

	void OnMouseDown() {
		bikeHandler.OnHelmetClick ();
	}

	public void Wear () {
		anim.SetInteger ("wear", 1);
	}

	public void UnWear () {
		anim.SetInteger ("wear", 2);
	}
}
