using UnityEngine;
using System.Collections;

public class BikeMovement : MonoBehaviour {

	public const float TO_REAL_SPEED = 27.0f / 40;

	public float Gravity;
	public float MaxMoveSpeed;
	public float RotateSpeed;
	public float accelMoveFoward;
	public float accelMoveBackward;
	public float accelAutoStop;

	private float moveSpeed = 0.0f;
	private CharacterController controller;

	public float Speed {
		get {
			return moveSpeed * TO_REAL_SPEED;
		}
	}

	void Start () {
		controller = GetComponent<CharacterController> ();
	}

	void Update () {

		if (Input.GetKey (KeyCode.A) == true 
		    || Input.GetKey (KeyCode.LeftArrow) == true) { //left

			transform.localEulerAngles = 
				new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - RotateSpeed * Time.deltaTime, 0);
		} else if (Input.GetKey (KeyCode.D) == true 
		           || Input.GetKey (KeyCode.RightArrow) == true) { //right

			transform.localEulerAngles = 
				new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + RotateSpeed * Time.deltaTime, 0);
		}
		
		//Foward
		if (Input.GetKey (KeyCode.W) 
		    || Input.GetKey (KeyCode.UpArrow)) { 	//Forward
			
			moveSpeed += accelMoveFoward * Time.deltaTime;
			if (moveSpeed > MaxMoveSpeed) {
				moveSpeed = MaxMoveSpeed;
			}
		} else if (Input.GetKey (KeyCode.Space) 	//Backward
		           || Input.GetKey (KeyCode.DownArrow) 
		           || Input.GetKey (KeyCode.S)) {
			
			moveSpeed -= accelMoveBackward * Time.deltaTime;
			if (moveSpeed < 0.2) {
				moveSpeed = 0;
			}
		}

		Vector3 v = transform.forward * moveSpeed * Time.deltaTime;
		controller.Move (v);

		controller.Move (transform.up * Gravity);
	}

}
