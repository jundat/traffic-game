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

	public const float minAngle = 0;
	public const float maxAngle = 240.0f;
	public float angle = 0.0f;
	public GameObject kim;

	public void SetSpeed (float speed, float minSpeed, float maxSpeed) {
		float totalAngle = maxAngle - minAngle;
		float totalVelo = maxSpeed - minSpeed;
		float anglePerSpeed = totalAngle / totalVelo;
		angle = anglePerSpeed * speed;
	}

	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update () {
#if !UNITY_ANDROID
		if (Input.GetKey (KeyCode.A) == true || Input.GetKey (KeyCode.LeftArrow) == true) { //left
			AnimLeft ();
		} else if (Input.GetKey (KeyCode.D) == true || Input.GetKey (KeyCode.RightArrow) == true) { //right
			AnimRight ();
		} else {
			AnimAhead ();
		}
#endif

		kim.transform.localEulerAngles = new Vector3 (0, angle, 0);
	}

	public void AnimLeft () {
		if (dir != BikeDirection.left) {
			dir = BikeDirection.left;
			anim.SetInteger ("dir",2);
		}
	}

	public void AnimRight () {
		if (dir != BikeDirection.right) {
			dir = BikeDirection.right;
			anim.SetInteger ("dir",1);
		}
	}

	public void AnimAhead () {
		dir = BikeDirection.ahead;
		anim.SetInteger ("dir",0);
	}
}
