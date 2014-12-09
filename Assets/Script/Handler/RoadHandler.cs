using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadHandler : TileHandler {

	[System.Serializable]
	public class CollisionRoad {
		public RoadHandler road;
		public MoveDirection dir;
	}

	public List<CollisionRoad> listCollisionRoads = new List<CollisionRoad> ();

	private TrafficLightStatus lightStatus = TrafficLightStatus.none;

	public MeshRenderer roadMeshRender;
	public MeshRenderer roadMeshRenderRepeat;

	public Material prefabRoadMat;
	public Material prefabRoadMatRepeat;
	public Material prefabViaHeMat;
	public Material prefabDaiPCachMat; //Dai phan cach

	//Border Collider
	public string _________________1;
	public GameObject borderRight;
	public GameObject borderLeft;
	public GameObject borderUp;
	public GameObject borderDown;

	//Via he
	public string _________________2;
	public GameObject viaheRight;
	public GameObject viaheLeft;
	public GameObject viaheUp;
	public GameObject viaheDown;

	//Vach duong
	public string _________________3;
	public GameObject vachRight;
	public GameObject vachLeft;
	public GameObject vachUp;
	public GameObject vachDown;

	//Le Duong
	public string _________________4;
	public GameObject leRight;
	public GameObject leLeft;
	public GameObject leUp;
	public GameObject leDown;

	//Vach Phan Cach
	public string _________________5;
	public GameObject pcachRight;
	public GameObject pcachLeft;
	public GameObject pcachUp;
	public GameObject pcachDown;

	//Dai Phan Cach
	public string _________________6;
	public GameObject daiPCachRight;
	public GameObject daiPCachLeft;
	public GameObject daiPCachUp;
	public GameObject daiPCachDown;

	//Anchor Point
	public string _________________7;
	public GameObject anchorRight;
	public GameObject anchorLeft;
	public GameObject anchorUp;
	public GameObject anchorDown;



	public TrafficLightStatus LightStatus {
		get {
			return lightStatus;
		}
		set {
			lightStatus = value;

			//-------- debug --------
			if (Global.DEBUG_LIGHT) { 
				RoadHandler handler = gameObject.GetComponent <RoadHandler> ();
				switch (lightStatus) {
				case TrafficLightStatus.none:
					handler.roadMeshRender.material.color = Color.white;
					break;

				case TrafficLightStatus.green:
					handler.roadMeshRender.material.color = Color.green;
					break;

				case TrafficLightStatus.red:
					handler.roadMeshRender.material.color = Color.red;
					break;
					
				case TrafficLightStatus.yellow:
					handler.roadMeshRender.material.color = Color.yellow;
					break;
				}
			}
		}
	}

	void Start () {
	}
	
	void Update () {}

	public void Init () {
		//Dai Phan Cach
//		bool isDaiPCRight = bool.Parse ( Ultil.GetString (TileKey.PCACH_PHAI, "false", tile.properties));
//		bool isDaiPCLeft = bool.Parse ( Ultil.GetString (TileKey.PCACH_TRAI, "false", tile.properties));
//		bool isDaiPCUp = bool.Parse ( Ultil.GetString (TileKey.PCACH_TREN, "false", tile.properties));
//		bool isDaiPCDown = bool.Parse ( Ultil.GetString (TileKey.PCACH_DUOI, "false", tile.properties));
//
//		daiPCachRight.SetActive (isDaiPCRight);
//		daiPCachLeft.SetActive (isDaiPCLeft);
//		daiPCachUp.SetActive (isDaiPCUp);
//		daiPCachDown.SetActive (isDaiPCDown);

		daiPCachRight.SetActive (false);
		daiPCachLeft.SetActive (false);
		daiPCachUp.SetActive (false);
		daiPCachDown.SetActive (false);

		Material matDaiPCLeftRight = (Material) GameObject.Instantiate (prefabDaiPCachMat);
		Material matDaiPCUpDown = (Material) GameObject.Instantiate (prefabDaiPCachMat);
		
		MeshRenderer daiPCLeftRender = daiPCachLeft.GetComponent <MeshRenderer> ();
		MeshRenderer daiPCRightRender = daiPCachRight.GetComponent <MeshRenderer> ();
		MeshRenderer daiPCUpRender = daiPCachUp.GetComponent <MeshRenderer> ();
		MeshRenderer daiPCDownRender = daiPCachDown.GetComponent <MeshRenderer> ();
		
		daiPCLeftRender.material = matDaiPCLeftRight;
		daiPCRightRender.material = matDaiPCLeftRight;
		daiPCUpRender.material = matDaiPCUpDown;
		daiPCDownRender.material = matDaiPCUpDown;
		
		matDaiPCLeftRight.mainTextureScale = new Vector2 (tile.w / 16 / 5, tile.h / 12);
		matDaiPCUpDown.mainTextureScale = new Vector2 (tile.w / 12, tile.h / 16 / 5);

		//-----------------------

		bool isRight = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_PHAI, "false", tile.properties));
		bool isLeft = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_TRAI, "false", tile.properties));
		bool isUp = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_TREN, "false", tile.properties));
		bool isDown = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_DUOI, "false", tile.properties));
		
		borderRight.SetActive (isRight);
		borderLeft.SetActive (isLeft);
		borderUp.SetActive (isUp);
		borderDown.SetActive (isDown);

		leRight.SetActive (false);
		leLeft.SetActive (false);
		leUp.SetActive (false);
		leDown.SetActive (false);

		viaheRight.SetActive (isRight);
		viaheLeft.SetActive (isLeft);
		viaheUp.SetActive (isUp);
		viaheDown.SetActive (isDown);

		vachRight.SetActive (false);
		vachLeft.SetActive (false);
		vachUp.SetActive (false);
		vachDown.SetActive (false);

		if (tile.typeId == TileID.ROAD_NONE) { //Giao lo
			vachRight.SetActive (!isRight);
			vachLeft.SetActive (!isLeft);
			vachUp.SetActive (!isUp);
			vachDown.SetActive (!isDown);
		}

		
		pcachRight.SetActive (false);
		pcachLeft.SetActive (false);
		pcachUp.SetActive (false);
		pcachDown.SetActive (false);

		roadMeshRender.gameObject.SetActive (false);
		roadMeshRenderRepeat.gameObject.SetActive (false);

		//Material
		if (IsBus) {
			roadMeshRender.gameObject.SetActive (true);

			//Road
			Material matRoad = (Material) GameObject.Instantiate (prefabRoadMat);
			roadMeshRender.material = matRoad;

			if (Ultil.IsVerticalRoad (tile)) {
				if (tile.h > 512) {
					matRoad.mainTextureScale = new Vector2 (1, tile.h / 512);
				} else {
					matRoad.mainTextureScale = new Vector2 (1, 1);
				}
			} else if (Ultil.IsHorizontalRoad (tile)) {
				if (tile.w > 512) {
					matRoad.mainTextureScale = new Vector2 (tile.w / 512, 1);
				} else {
					matRoad.mainTextureScale = new Vector2 (1, 1);
				}
			}
		} else {
			roadMeshRenderRepeat.gameObject.SetActive (true);

			//Road Repeat
			Material matRoadRepeat = (Material) GameObject.Instantiate (prefabRoadMatRepeat);
			roadMeshRenderRepeat.material = matRoadRepeat;
			matRoadRepeat.mainTextureScale = new Vector2 (tile.w / 16, tile.h / 16);

			//Vach Phan Cach
			switch (tile.typeId) {
			case 1:
				pcachRight.SetActive (true);
				break;
				
			case 2:
				pcachDown.SetActive (true);
				break;
				
			case 3:
				pcachUp.SetActive (true);
				break;
				
			case 4:
				pcachLeft.SetActive (true);
				break;
			}
		}

		//Via he
		Material matViaheLeftRight = (Material) GameObject.Instantiate (prefabViaHeMat);
		Material matViaheUpDown = (Material) GameObject.Instantiate (prefabViaHeMat);

		MeshRenderer viaheLeftRender = viaheLeft.GetComponent <MeshRenderer> ();
		MeshRenderer viaheRightRender = viaheRight.GetComponent <MeshRenderer> ();
		MeshRenderer viaheUpRender = viaheUp.GetComponent <MeshRenderer> ();
		MeshRenderer viaheDownRender = viaheDown.GetComponent <MeshRenderer> ();

		viaheLeftRender.material = matViaheLeftRight;
		viaheRightRender.material = matViaheLeftRight;
		viaheUpRender.material = matViaheUpDown;
		viaheDownRender.material = matViaheUpDown;

		matViaheLeftRight.mainTextureScale = new Vector2 (tile.w / 16 / 5, tile.h / 12);
		matViaheUpDown.mainTextureScale = new Vector2 (tile.w / 12, tile.h / 16 / 5);

		//-------------- Tim cac duong giao --------------

	}

	#region Collision Road
	public void FetchCollisionRoad () {
		if (tile.typeId == TileID.ROAD_NONE) {
			Dictionary<int, TileHandler> dict = MapRenderer.Instance.layerRoad;

			BoxCollider box = this.GetComponent<BoxCollider>();
			
			foreach (KeyValuePair<int, TileHandler> p in dict) {
				RoadHandler road = (RoadHandler) p.Value;
				if (road.tile.typeId >= TileID.ROAD_MIN && road.tile.typeId <= TileID.ROAD_MAX) {
					BoxCollider box1 = road.GetComponent<BoxCollider>();

					//box vs box1
					if (box.bounds.Intersects (box1.bounds)) {

						CollisionRoad c = new CollisionRoad ();
						c.road = road;

						Vector3 v = road.transform.localPosition - this.transform.localPosition;
						MoveDirection dir = Ultil.GetMoveDirection (v);
						c.dir = dir;

						listCollisionRoads.Add (c);
					}
				}
			}
		}
	}
	#endregion

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

	public MoveDirection CheckInLeDuong (Vector3 pos) {
		pos.y = this.transform.position.y + 1;

		bool isRight = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_PHAI, "false", tile.properties));
		bool isLeft = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_TRAI, "false", tile.properties));
		bool isUp = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_TREN, "false", tile.properties));
		bool isDown = bool.Parse ( Ultil.GetString (TileKey.ROAD_LE_DUOI, "false", tile.properties));
		leRight.SetActive (isRight);
		leLeft.SetActive (isLeft);
		leUp.SetActive (isUp);
		leDown.SetActive (isDown);

		BoxCollider box = this.GetComponent<BoxCollider>();
		box.enabled = false;
		MoveDirection result = MoveDirection.NONE;

		RaycastHit hit;
		Ray rayDown = new Ray (pos, Vector3.down);
		//Debug.DrawRay (pos, Vector3.down);
		if (Physics.Raycast (rayDown, out hit)) {
			string name = hit.transform.gameObject.name;

			//Debug.Log (hit.transform.gameObject.name);

			switch (name) {
			case OBJ.LeLeft:
				result = MoveDirection.LEFT;
				break;
				
			case OBJ.LeRight:
				result = MoveDirection.RIGHT;
				break;
				
			case OBJ.LeUp:
				result = MoveDirection.UP;
				break;
				
			case OBJ.LeDown:
				result = MoveDirection.DOWN;
				break;
				
			default:
				result = MoveDirection.NONE;
				break;
			}
		}

		box.enabled = true;

		leRight.SetActive (false);
		leLeft.SetActive (false);
		leUp.SetActive (false);
		leDown.SetActive (false);

		return result;
	}

	public MoveDirection CheckInAllLeDuong (Vector3 pos) {
		pos.y = this.transform.position.y + 1;

		leRight.SetActive (true);
		leLeft.SetActive (true);
		leUp.SetActive (true);
		leDown.SetActive (true);
		
		BoxCollider box = this.GetComponent<BoxCollider>();
		box.enabled = false;
		MoveDirection result = MoveDirection.NONE;
		
		RaycastHit hit;
		Ray rayDown = new Ray (pos, Vector3.down);
		//Debug.DrawRay (pos, Vector3.down);
		if (Physics.Raycast (rayDown, out hit)) {
			string name = hit.transform.gameObject.name;
			
			Debug.Log (hit.transform.gameObject.name);
			
			switch (name) {
			case OBJ.LeLeft:
				result = MoveDirection.LEFT;
				break;
				
			case OBJ.LeRight:
				result = MoveDirection.RIGHT;
				break;
				
			case OBJ.LeUp:
				result = MoveDirection.UP;
				break;
				
			case OBJ.LeDown:
				result = MoveDirection.DOWN;
				break;
				
			default:
				result = MoveDirection.NONE;
				break;
			}
		}
		
		box.enabled = true;
		
		leRight.SetActive (false);
		leLeft.SetActive (false);
		leUp.SetActive (false);
		leDown.SetActive (false);
		
		return result;
	}
	
	public InRoadPosition CheckInOutLen (Vector3 pos) {
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


