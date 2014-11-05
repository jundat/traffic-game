using UnityEngine;
using System.Collections;

public class BikeMovement : MonoBehaviour {

	public const float TO_REAL_SPEED = 0.5f;

	//Moto
	public float MaxMoveSpeed = 280;
	public float RotateSpeed = 70;
	public float accelMoveFoward = 50;
	public float accelMoveBackward = 100;
	public float accelAutoStop = 100;
	private float moveSpeed = 0.0f;

	public float Speed {
		get {
			return moveSpeed * TO_REAL_SPEED;
		}
	}

	void Start () {}

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
		if (Input.GetKey (KeyCode.W) == true 
		    || Input.GetKey (KeyCode.UpArrow) == true) { //ahead

			moveSpeed += accelMoveFoward * Time.deltaTime;
			if (moveSpeed > MaxMoveSpeed) {
				moveSpeed = MaxMoveSpeed;
			}
		}

		if (Input.GetKey (KeyCode.Space) == true 
		    || Input.GetKey (KeyCode.DownArrow) == true 
		    || Input.GetKey (KeyCode.S) == true) {

			moveSpeed -= accelMoveBackward * Time.deltaTime;
			if (moveSpeed < 0.2) {
				moveSpeed = 0;
			}
		}

		transform.localPosition += transform.forward * moveSpeed * Time.deltaTime;
	}
}
