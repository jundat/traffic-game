using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoCarHandler : TileHandler {

	public const float UPDATE_DELAY = 2.0f;
	public const float UPDATE_INTERVAL = 0.2f;
	
	public float SPEED = 40.0f;
	public MoveDirection direction;

	private bool isInJunction = false; //giao lo

	void Start () {
		InvokeRepeating ("ScheduleUpdate", UPDATE_DELAY, UPDATE_INTERVAL);
	}

	void Update () {

		Vector3 pos = transform.localPosition;
		pos += SPEED * transform.forward * Time.deltaTime;
		transform.localPosition = pos;
	}

	public void Init () {
		
		direction = Ultil.ToMoveDirection ( tile.properties[TileKey.AUTOCAR_DIR]);
		RotateToDirection ();
	}

	public void ScheduleUpdate () {
		//Change direction if need

		RoadHandler road = Ultil.RayCastRoad (this.transform.position + new Vector3 (0,1,0));
		if (road != null) {
			if (road.tile.typeId == 7) {

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
						SPEED = 0;
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
}
