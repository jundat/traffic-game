using UnityEngine;
using System.Collections;

public enum MyDirection {
	left,
	right,
	ahead
}

public class ScooterHandler : MonoBehaviour {

	private Animator anim;

	MyDirection dir = MyDirection.ahead;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A) == true) { //left
			if (dir != MyDirection.left) {
				dir = MyDirection.left;
				anim.SetInteger ("dir",2);
			}
		} else if (Input.GetKey (KeyCode.D) == true) { //left
			if (dir != MyDirection.right) {
				dir = MyDirection.right;
				anim.SetInteger ("dir",1);
			}
		} else {
			dir = MyDirection.ahead;
			anim.SetInteger ("dir",0);
		}
	}
}
