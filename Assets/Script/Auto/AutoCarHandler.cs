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

					//Cac huong co the di
					bool isRight = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_PHAI, "false", road.tile.properties));
					bool isLeft = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_TRAI, "false", road.tile.properties));
					bool isUp = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_TREN, "false", road.tile.properties));
					bool isDown = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_DUOI, "false", road.tile.properties));
					
					List<MoveDirection> listDirection = new List<MoveDirection> ();
					if (!isRight) {
						listDirection.Add (MoveDirection.RIGHT);
					}

					if (!isLeft) {
						listDirection.Add (MoveDirection.LEFT);
					}

					if (!isUp) {
						listDirection.Add (MoveDirection.UP);
					}

					if (!isDown) {
						listDirection.Add (MoveDirection.DOWN);
					}
					//Debug.Log ("Type: " + road.tile.typeId + ", Id: " + road.tile.objId);
					//Debug.Log (">----------------");
					//for (int i = 0; i < listDirection.Count; ++i) {
					//	Debug.Log (listDirection[i]);
					//}

					//current direction
					MoveDirection currentDirection = Ultil.GetMoveDirection (this.transform.forward);
					MoveDirection removedDirection = Ultil.OppositeOf (currentDirection);
					listDirection.Remove (removedDirection);
					//Debug.Log ("Currnet: " + currentDirection);
					//Debug.Log ("Opposite: " + removedDirection);
					//Debug.Log ("<<<<");
					//for (int i = 0; i < listDirection.Count; ++i) {
					//	Debug.Log (listDirection[i]);
					//}
					//Debug.Log (">>>>");

					//Random
					int count = listDirection.Count;
					int idx = Ultil.random.Next (0, count-1);
					MoveDirection nextDirection = listDirection[idx];
					//Debug.Log ("Next: " + nextDirection);
					direction = nextDirection;
					RotateToDirection ();

					//Debug.Log ("-----------------<");
				}
			} else {
				//Debug.Log ("ExitConjunction: " + road.tile.objId);
				isInJunction = false;
			}
		} else {
			//Debug.Log ("NULL");
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
