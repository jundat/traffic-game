using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class AutoCarHandler : TileHandler {

	public const float UPDATE_DELAY = 2.0f;
	public const float UPDATE_INTERVAL = 0.2f;
	private const float STOP_SPEED = 0.001f;

	public List<AutocarCollider> listCollider = new List<AutocarCollider> ();
	public List<Collider> currentCollision = new List<Collider> ();
	public GameObject frontTire;
	public GameObject rearTire;
	public float SPEED = 40.0f;
	public float currentSpeed;
	public MoveDirection direction;

	private bool isInJunction = false; //giao lo
	private TweenRotation tweenFront;
	private TweenRotation tweenRear;

	void Awake () {
		tweenFront = frontTire.GetComponent<TweenRotation>();
		tweenRear = rearTire.GetComponent<TweenRotation>();
	}

	void Start () {
		currentSpeed = SPEED;

		for (int i = 0; i < listCollider.Count; ++i) {
			listCollider[i].onCollideEnter = this.CallbackCollideEnter;
			listCollider[i].onCollideExit = this.CallbackCollideExit;
		}

		InvokeRepeating ("ScheduleUpdate", UPDATE_DELAY, UPDATE_INTERVAL);
	}

	void Update () {

		Vector3 pos = transform.localPosition;
		pos += currentSpeed * transform.forward * Time.deltaTime;
		transform.localPosition = pos;

		tweenFront.duration = 0.5f * SPEED / (currentSpeed + 0.001f);
		tweenRear.duration = 0.5f * SPEED / (currentSpeed + 0.001f);
	}

	public void Init () {
		
		direction = Ultil.ToMoveDirection ( tile.properties[TileKey.AUTOCAR_DIR]);
		RotateToDirection ();
	}

	public void ScheduleUpdate () {
		//Change direction if need

		RoadHandler road = Ultil.RayCastRoad (this.transform.position + new Vector3 (0,1,0));
		if (road != null) {
			if (road.tile.typeId == TileID.ROAD_NONE) {

				if (isInJunction == false) {
					isInJunction = true;

					List<RoadHandler> listAvai = new List<RoadHandler> ();
					for (int i = 0; i < road.listCollisionRoads.Count; ++i) {
						RoadHandler.CollisionRoad c = road.listCollisionRoads[i];
						RoadHandler r = c.road;
						if (r.Direction == c.dir) {
							listAvai.Add (r);
						}
					}

					int count = listAvai.Count;
					if (count > 0) {
						int randomIndex = Ultil.random.Next (0, count-1);
						RoadHandler nextRoad = listAvai[randomIndex];

						this.direction = nextRoad.Direction;
						RotateToDirection ();

						switch (nextRoad.Direction) {
						case MoveDirection.UP:
							this.transform.position = nextRoad.anchorDown.transform.position;
							break;

						case MoveDirection.DOWN:
							this.transform.position = nextRoad.anchorUp.transform.position;
							break;
							
						case MoveDirection.LEFT:
							this.transform.position = nextRoad.anchorRight.transform.position;
							break;
							
						case MoveDirection.RIGHT:
							this.transform.position = nextRoad.anchorLeft.transform.position;
							break;
						}

					} else {
						currentSpeed = STOP_SPEED;
					}
				}
			} else {
				isInJunction = false;
			}
		}
	}

	private void RotateToDirection () {
		
		//Rotation
		switch (direction) {
		case MoveDirection.UP:
			transform.eulerAngles = new Vector3 (0, 180, 0);
			break;
			
		case MoveDirection.DOWN:
			transform.eulerAngles = new Vector3 (0, 0, 0);
			break;
			
		case MoveDirection.LEFT:
			transform.eulerAngles = new Vector3 (0, 90, 0);
			break;
			
		case MoveDirection.RIGHT:
			transform.eulerAngles = new Vector3 (0, -90, 0);
			break;
		}
	}

	public void CallbackCollideEnter (Collider col, AutocarCollider side) {
		string sideName = side.gameObject.name;
		string colName = col.gameObject.name;


		if (colName != OBJ.START_POINT &&
		    colName != OBJ.FINISH_POINT &&
		    colName != OBJ.CHECK_POINT) 
		{
			//Debug.Log (col.gameObject.name + " >< " + side.gameObject.name);

			if (sideName == AutocarCollider.FAR_FRONT) {
				if (currentCollision.Count == 0) {
					currentSpeed = SPEED / 2;
				}
			}

			if (sideName == AutocarCollider.FRONT) {
				currentCollision.Add (col);
				currentSpeed = STOP_SPEED;
			}
		}
	}

	public void CallbackCollideExit (Collider col, AutocarCollider side) {

		string sideName = side.gameObject.name;
		string colName = col.gameObject.name;

		if (colName != OBJ.START_POINT &&
		    colName != OBJ.FINISH_POINT &&
		    colName != OBJ.CHECK_POINT) 
		{
			//Debug.LogError (col.gameObject.name + " >< " + side.gameObject.name);

			if (sideName == AutocarCollider.FRONT) {
				currentCollision.Remove (col);
				if (currentCollision.Count == 0) {
					currentSpeed = SPEED;
				}
			}

			if (sideName == AutocarCollider.FAR_FRONT) {
				if (currentCollision.Count == 0) {
					currentSpeed = SPEED;
				}
			}
		}
	}
}
