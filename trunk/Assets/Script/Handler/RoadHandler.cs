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
	//
	public GameObject topRight;
	public GameObject topLeft;
	public GameObject bottomLeft;
	public GameObject bottomRight;

	public GameObject redStop = null;
	private const float RED_STOP_Y_OUT = -20;
	private const float RED_STOP_Y = 8;
	private const float RED_STOP_DELTA = 15;

	public TrafficLightStatus LightStatus {
		get {
			return lightStatus;
		}
		set {
			lightStatus = value;
			redStop.SetActive (true);

			if (lightStatus == TrafficLightStatus.red) {
				switch (tile.typeId) {
				case 1://down
					redStop.transform.position = this.anchorDown.transform.position + new Vector3 (0, RED_STOP_Y, - RED_STOP_DELTA);
					break;
					
				case 2: //left
					redStop.transform.position = this.anchorLeft.transform.position + new Vector3 (- RED_STOP_DELTA, RED_STOP_Y, 0);;
					break;
					
				case 3: //right
					redStop.transform.position = this.anchorRight.transform.position + new Vector3 (+ RED_STOP_DELTA, RED_STOP_Y, 0);;
					break;
					
				case 4: //up
					redStop.transform.position = this.anchorUp.transform.position + new Vector3 (0, RED_STOP_Y, + RED_STOP_DELTA);;
					break;
				}
			} else {
				redStop.transform.position = redStop.transform.position + new Vector3 (0, RED_STOP_Y_OUT, 0);
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

		//Create Red Stop
		float hRoad = Vector3.Distance (anchorUp.transform.position, anchorDown.transform.position);
		float wRoad = Vector3.Distance (anchorLeft.transform.position, anchorRight.transform.position);

		redStop = GameObject.CreatePrimitive (PrimitiveType.Cube);
		TweenRotation t = redStop.AddComponent <TweenRotation>();
		t.duration = 8;
		t.style = UITweener.Style.Loop;
		t.from = Vector3.zero;
		
		switch (tile.typeId) {
		case 1://down
			redStop.transform.localScale = new Vector3 (4*wRoad, 0.125f, 0.125f);
			t.to = new Vector3 (360, 0, 0);
			break;
			
		case 2: //left
			redStop.transform.localScale = new Vector3 (0.125f, 0.125f, 4*hRoad);
			t.to = new Vector3 (0, 0, 360);
			break;
			
		case 3: //right
			redStop.transform.localScale = new Vector3 (0.125f, 0.125f, 4*hRoad);
			t.to = new Vector3 (0, 0, 360);
			break;
			
		case 4: //up
			redStop.transform.localScale = new Vector3 (4*wRoad, 0.125f, 0.125f);
			t.to = new Vector3 (360, 0, 0);
			break;
		}

		redStop.name = OBJ.RED_STOP;
		redStop.transform.position = new Vector3 (0, RED_STOP_Y_OUT, 0);

		Rigidbody rig = redStop.AddComponent <Rigidbody>();
		rig.mass = 1;
		rig.drag = 0;
		rig.angularDrag = 0;
		rig.isKinematic = true;
		rig.useGravity = false;
		rig.constraints = RigidbodyConstraints.FreezeAll;
		rig.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

		redStop.GetComponent<MeshRenderer>().enabled = false;
		redStop.SetActive (false);
	}

	private Rect _rect;
	private bool _isGetRect = false;
	public Rect Rect {
		get {
			if (_isGetRect == false) {
				Bounds b = roadMeshRenderRepeat.bounds;
				_rect = new Rect ();
				_rect.x = b.center.x - b.size.x/2;
				_rect.y = b.center.z - b.size.z/2;
				_rect.width = b.size.x;
				_rect.height = b.size.z;

				_isGetRect = true;
				//Debug.Log ("Rect: " + rect.x + ", " + rect.y + ", " + rect.width + ", " + rect.height);
			}
			return _rect;
		}
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
	bool _directionChecked = false;
	MoveDirection _direction = MoveDirection.NONE;
	public MoveDirection Direction {
		get {
			if (_directionChecked == false) {
				_directionChecked = true;
				switch (tile.typeId) {
				case 1:
					_direction = MoveDirection.DOWN;
					break;

				case 2:
					_direction = MoveDirection.LEFT;
					break;

				case 3:
					_direction = MoveDirection.RIGHT;
					break;

				case 4:
					_direction = MoveDirection.UP;
					break;
					
				case 7:
					_direction = MoveDirection.NONE;
					break;
				}
			}

			return _direction;
		}
	}

	bool _bikeAvailableChecked = false;
	bool _bikeAvailable = false;
	public bool BikeAvailable {
		get {
			if (_bikeAvailableChecked == false) {
				_bikeAvailableChecked = true;
				string s = Ultil.GetString (TileKey.ROAD_DI + VihicleType.MoToA1, "true", tile.properties);
				_bikeAvailable = bool.Parse (s);
			}
			return _bikeAvailable;
		}
	}

	bool _bikeStopAvailableChecked = false;
	bool _bikeStopAvailable = false;
	public bool BikeStopAvailable {
		get {
			if (_bikeStopAvailableChecked == false) {
				_bikeStopAvailableChecked = true;
				string s = Ultil.GetString (TileKey.ROAD_DUNG + VihicleType.MoToA1, "true", tile.properties);
				_bikeStopAvailable = bool.Parse (s);
			}
			return _bikeStopAvailable;
		}
	}

	int _minSpeed = -1;
	public int MinSpeed {
		get {
			if (_minSpeed == -1) {
				string s = Ultil.GetString (TileKey.ROAD_MIN_VEL, "0", tile.properties);
				_minSpeed = int.Parse (s);
			}
			return _minSpeed;
		}
	}

	int _maxSpeed = -1;
	public int MaxSpeed {
		get {
			if (_maxSpeed == -1) {
				string s = Ultil.GetString (TileKey.ROAD_MAX_VEL, "40", tile.properties);
				_maxSpeed = int.Parse (s);
			}
			return _maxSpeed;
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

	bool _canTurnLeftChecked = false;
	bool _canTurnLeft = false;
	public bool CanTurnLeft {
		get {
			if (_canTurnLeftChecked == false) {
				_canTurnLeftChecked = true;
				string s = Ultil.GetString (TileKey.ROAD_RE_TRAI, "true", tile.properties);
				_canTurnLeft = bool.Parse (s);
			}

			return _canTurnLeft;
		}
	}

	bool _canTurnRightChecked = false;
	bool _canTurnRight = false;
	public bool CanTurnRight {
		get {
			if (_canTurnRightChecked == false) {
				_canTurnRightChecked = true;
				string s = Ultil.GetString (TileKey.ROAD_RE_PHAI, "true", tile.properties);
				_canTurnRight = bool.Parse (s);
			}
			return _canTurnRight;
		}
	}

	bool _canGoAheadChecked = false;
	bool _canGoAhead = false;
	public bool CanGoAhead {
		get {
			if (_canGoAheadChecked == false) {
				_canGoAheadChecked = true;
				string s = Ultil.GetString (TileKey.ROAD_RE_THANG, "true", tile.properties);
				_canGoAhead = bool.Parse (s);
			}
			return _canGoAhead;
		}
	}

	bool _canTurnBackChecked = false;
	bool _canTurnBack = false;
	public bool CanTurnBack {
		get {
			if (_canTurnBackChecked == false) {
				_canTurnBackChecked = true;
				string s = Ultil.GetString (TileKey.ROAD_QUAY_DAU, "true", tile.properties);
				_canTurnBack = bool.Parse (s);
			}
			return _canTurnBack;
		}
	}

	#endregion
}


public enum InRoadPosition {
	None, 	//ko nam tren duong
	InLen,	//len trong le
	OutLen	//len ngoai le
}


