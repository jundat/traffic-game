using UnityEngine;
using System.Collections;

public class RoadHandler : TileHandler {

	private TrafficLightStatus lightStatus = TrafficLightStatus.none;

	//Border Collider
	public GameObject borderRight;
	public GameObject borderLeft;
	public GameObject borderUp;
	public GameObject borderDown;

	//Viahe
	public GameObject viaheRight;
	public GameObject viaheLeft;
	public GameObject viaheUp;
	public GameObject viaheDown;

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
		bool isRight = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_PHAI, "false", tile.properties));
		bool isLeft = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_TRAI, "false", tile.properties));
		bool isUp = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_TREN, "false", tile.properties));
		bool isDown = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_DUOI, "false", tile.properties));
		
		borderRight.SetActive (isRight);
		borderLeft.SetActive (isLeft);
		borderUp.SetActive (isUp);
		borderDown.SetActive (isDown);

		viaheRight.SetActive (isRight);
		viaheLeft.SetActive (isLeft);
		viaheUp.SetActive (isUp);
		viaheDown.SetActive (isDown);
	}

	#region Public Get Functions 
	public MoveDirection Direction {
		get {
			switch (tile.typeId) {
			case 1:
				return MoveDirection.DOWN;

			case 2:
				return MoveDirection.LEFT;

			case 3:
				return MoveDirection.RIGHT;

			case 4:
				return MoveDirection.UP;
				
			case 7:
				return MoveDirection.NONE;
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

	public int MinSpeed {
		get {
			string s = Ultil.GetString (TileKey.ROAD_MIN_VEL, "0", tile.properties);
			return int.Parse (s);
		}
	}
	
	public int MaxSpeed {
		get {
			string s = Ultil.GetString (TileKey.ROAD_MAX_VEL, "40", tile.properties);
			return int.Parse (s);
		}
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

	/// <summary>
	/// Kiem tra pos da nam o trong le chua
	/// </summary>
	public bool IsInBorder (Vector3 pos) {
		//float dx = pos.x - transform.position.x;
		//float dz = pos.z - transform.position.z;
		float w = transform.localScale.x * Global.SCALE_SIZE;
		float h = transform.localScale.z * Global.SCALE_SIZE;

		float dl = pos.x - (transform.position.x + w/2);
		float dr = pos.x - (transform.position.x - w/2);
		float dd = pos.z - (transform.position.z + h/2);
		float du = pos.z - (transform.position.z - h/2);

		if (dl >= 0 
		    && dr <= 0 
		    && du <= 0
		    && dd >= 0) //Contain
		{
			return true;
		}

		return false;
	}

	public bool IsBus {
		get {
			return (tile.typeId == TileID.ROAD_BUS_UP 
			        || tile.typeId == TileID.ROAD_BUS_DOWN 
			        || tile.typeId == TileID.ROAD_BUS_LEFT
			        || tile.typeId == TileID.ROAD_BUS_RIGHT);
		}
	}

	public bool CanTurnLeft {
		get {
			string s = Ultil.GetString (TileKey.ROAD_RE_TRAI, "true", tile.properties);
			return bool.Parse (s);
		}
	}

	public bool CanTurnRight {
		get {
			string s = Ultil.GetString (TileKey.ROAD_RE_PHAI, "true", tile.properties);
			return bool.Parse (s);
		}
	}

	public bool CanGoAhead {
		get {
			string s = Ultil.GetString (TileKey.ROAD_RE_THANG, "true", tile.properties);
			return bool.Parse (s);
		}
	}

	public bool CanTurnBack {
		get {
			string s = Ultil.GetString (TileKey.ROAD_QUAY_DAU, "true", tile.properties);
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


