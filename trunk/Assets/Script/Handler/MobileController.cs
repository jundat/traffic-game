using UnityEngine;
using System.Collections;

public class MobileController : MonoBehaviour {

	public float accelForward = 0.8f;
	public float accelBackward = -1.0f;
	public float accelRotate = 0.8f;

	private BikeMovement bike;
	private ScooterHandler scooter;
	private PlayerHandler player;

	void Awake () {
		if (Application.platform == RuntimePlatform.Android) {
			this.gameObject.SetActive (true);
		} else {
			this.gameObject.SetActive (false);
		}
	}

	// Use this for initialization
	void Start () {
		bike = Main.Instance.player.GetComponent<BikeMovement>();
		scooter = Main.Instance.player.GetComponent<BikeHandler>().scooterHandler;
		player = Main.Instance.player;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isMoveForward) {
			bike.Move (accelForward);
		}

		if (isMoveBackward) {
			bike.Move (accelBackward);
		}

		if (isMoveLeft) {
			bike.Rotate (-accelRotate);
			scooter.AnimLeft ();
		}
		
		if (isMoveRight) {
			bike.Rotate (accelRotate);
			scooter.AnimRight ();
		}

		if (isMoveLeft == false && isMoveRight == false) {
			scooter.AnimAhead ();
		}
	}

	//Move forward---------------------

	private bool isMoveForward = false;
	public void OnMoveForward () {
		isMoveForward = true;
	}

	public void OnMoveForwardRelease () {
		isMoveForward = false;
	}

	//Move forward---------------------
	
	private bool isMoveBackward = false;
	public void OnMoveBackward () {
		isMoveBackward = true;
	}
	
	public void OnMoveBackwardRelease () {
		isMoveBackward = false;
	}

	//Move Left------------------------
	
	private bool isMoveLeft = false;
	public void OnMoveLeft () {
		isMoveLeft = true;
	}
	
	public void OnMoveLeftRelease () {
		isMoveLeft = false;
	}
	
	//Move Right------------------------
	
	private bool isMoveRight = false;
	public void OnMoveRight () {
		isMoveRight = true;
	}
	
	public void OnMoveRightRelease () {
		isMoveRight = false;
	}

	// Light
	public void OnLight () {
		player.OnLight ();
	}

	public void OnLightFar () {
		player.OnLightFar ();
	}

	public void OnLightNear () {
		player.OnLightNear ();
	}

	public void OnLightTurnLeft () {
		player.OnLightTurnLeft ();
	}

	public void OnLightTurnRight () {
		player.OnLightTurnRight ();
	}

	public void OnHorn () {
		player.OnHorn ();
	}


}
