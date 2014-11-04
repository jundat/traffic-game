using UnityEngine;
using System.Collections;

public class BikeMovement : MonoBehaviour {

	//Moto
	public float MaxMoveSpeed = 500;
	public float RotateSpeed = 300;
	public float accelMoveFoward = 100;
	public float accelMoveBackward = 400;
	public float accelAutoStop = 100;
	private float moveSpeed = 0.0f;


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
