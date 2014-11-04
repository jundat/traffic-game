using UnityEngine;
using System.Collections;

public enum BikeDirection {
	left,
	right,
	ahead
}

public class ScooterHandler : MonoBehaviour {

	private Animator anim;

	BikeDirection dir = BikeDirection.ahead;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A) == true || Input.GetKey (KeyCode.LeftArrow) == true) { //left
			if (dir != BikeDirection.left) {
				dir = BikeDirection.left;
				anim.SetInteger ("dir",2);
			}
		} else if (Input.GetKey (KeyCode.D) == true || Input.GetKey (KeyCode.RightArrow) == true) { //right
			if (dir != BikeDirection.right) {
				dir = BikeDirection.right;
				anim.SetInteger ("dir",1);
			}
		} else {
			dir = BikeDirection.ahead;
			anim.SetInteger ("dir",0);
		}

//		if (Input.GetKey (KeyCode.W) == true || Input.GetKey (KeyCode.UpArrow) == true) {
//			dir = BikeDirection.ahead;
//			anim.SetInteger ("dir",0);
//		}
	}
}
