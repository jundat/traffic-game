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

	public float moveSpeed = 0.01f;
	private CharacterController controller;

	public float Speed {
		get {
			return moveSpeed * TO_REAL_SPEED;
		}
	}

	void Start () {
		controller = GetComponent<CharacterController> ();
	}

	void FixedUpdate () {
		if (Main.Instance.isEndGame == true) {return;}

		//Rotate
		float hor = Input.GetAxis("Horizontal");
		Rotate (hor);

		//Forward
		float ver = Input.GetAxis("Vertical");
		Move (ver);
	}

	public void Rotate (float hor) {
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + RotateSpeed * hor, 0);
	}

	public void Move (float ver) {
		if (ver >= 0) {
			moveSpeed += accelMoveFoward * ver;
			if (moveSpeed > MaxMoveSpeed) {
				moveSpeed = MaxMoveSpeed;
			}
		} else {
			moveSpeed += accelMoveBackward * ver;
			if (moveSpeed < 0.2f) {
				moveSpeed = 0.01f;
			}
		}
		
		Vector3 v = transform.forward * moveSpeed * Time.fixedDeltaTime;
		controller.Move (v);
	}

	public void StopRunning () {
		StartCoroutine (Stopping ());
	}

	private IEnumerator Stopping () {
		while (moveSpeed > 0) {
			moveSpeed -= accelMoveBackward/2.0f * Time.fixedDeltaTime;
			if (moveSpeed < 0.1f) {
				moveSpeed = 0.01f;
			}

			Vector3 v = transform.forward * moveSpeed * Time.fixedDeltaTime;
			controller.Move (v);

			yield return new WaitForSeconds (0.01f);
		}
	}
}
