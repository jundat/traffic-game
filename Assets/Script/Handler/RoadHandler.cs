using UnityEngine;
using System.Collections;

public class RoadHandler : TileHandler {

	private TrafficLightStatus lightStatus = TrafficLightStatus.none;

	public GameObject up;
	public GameObject down;
	public GameObject left;
	public GameObject right;

	public TrafficLightStatus LightStatus {
		get {
			return lightStatus;
		}
		set {
			lightStatus = value;

			//-------- debug --------
			if (Global.DEBUG_LIGHT) { 
				MeshRenderer render = gameObject.GetComponent <MeshRenderer> ();
				switch (lightStatus) {
				case TrafficLightStatus.none:
					render.material.color = Color.white;
					break;

				case TrafficLightStatus.green:
					render.material.color = Color.green;
					break;

				case TrafficLightStatus.red:
					render.material.color = Color.red;
					break;
					
				case TrafficLightStatus.yellow:
					render.material.color = Color.yellow;
					break;
				}
			}
		}
	}

	void Start () {

	}
	
	void Update () {}

	public void Init () {
		bool isUp = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_TREN, "false", tile.properties));
		bool isDown = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_DUOI, "false", tile.properties));
  		bool isRight = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_PHAI, "false", tile.properties));
   		bool isLeft = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_TRAI, "false", tile.properties));

		up.SetActive (isUp);
		down.SetActive (isDown);
		left.SetActive (isLeft);
		right.SetActive (isRight);
	}

	public InRoadPosition CheckPosition (Vector3 pos) {
		float dx = pos.x - transform.position.x;
		float dz = pos.z - transform.position.z;

		if (tile.typeId == TileID.ROAD_UP) {
			if (dx > 0) {
				return InRoadPosition.OutLen;
			} else {
				return InRoadPosition.InLen;
			}
		}

		if (tile.typeId == TileID.ROAD_DOWN) {
			if (dx > 0) {
				return InRoadPosition.InLen;
			} else {
				return InRoadPosition.OutLen;
			}
		}

		if (tile.typeId == TileID.ROAD_LEFT) {
			if (dz > 0) {
				return InRoadPosition.OutLen;
			} else {
				return InRoadPosition.InLen;
			}
		}
		
		if (tile.typeId == TileID.ROAD_RIGHT) {
			if (dz > 0) {
				return InRoadPosition.InLen;
			} else {
				return InRoadPosition.OutLen;
			}
		}

		return InRoadPosition.None; //out-of
	}

	#region Public Get Functions 
	public MoveDirection Direction {
		get {
			switch (tile.typeId) {
			case 1:
				return MoveDirection.DOWN;
				break;

			case 2:
				return MoveDirection.LEFT;
				break;

			case 3:
				return MoveDirection.RIGHT;
				break;

			case 4:
				return MoveDirection.UP;
				break;
				
			case 7:
				return MoveDirection.NONE;
				break;
			}

			return MoveDirection.NONE;
		}
	}

	public bool BikeAvailable {
		get {
			string s = Ultil.GetString (TileKey.ROAD_DI + VihicleType.MoToA1, "true", tile.properties);
			return bool.Parse (s);
		}
	}
	#endregion
}


public enum InRoadPosition {
	None, 	//ko nam tren duong
	InLen,	//len trong le
	OutLen	//len ngoai le
}


