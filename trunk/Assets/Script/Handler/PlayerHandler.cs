using UnityEngine;
using System;
using System.Collections;

public class PlayerHandler : SingletonMono <PlayerHandler> {

	public const float DELAY_TIME = 1;
	public const float SCHEDULE_TIME = 0.25f;

	private BikeHandler bikeHandler;
	private BikeMovement2 bikeMovement;

	public const int QUEUE_SIZE = 50;
	private Queue queueState = new Queue (QUEUE_SIZE); 			//Danh sach sau moi SCHEDULE_TIME
	private Queue queueStateDiff = new Queue (QUEUE_SIZE);		//Danh sach state khi co chuyen lan duong

	//Bat xi nhanh
	private const float TURN_TIME_LIMIT = 3;
	private TurnLight turnLight;
	private bool isTurnLightOn;
	private float startTurnLightTime;
	//


	void Awake () {
		bikeHandler = gameObject.GetComponent <BikeHandler> ();
		bikeMovement = gameObject.GetComponent <BikeMovement2> ();
	}

	void Start () {
		InvokeRepeating ("UpdateState", DELAY_TIME, SCHEDULE_TIME);
	}

	void Update () {
		//Sound ----------------------------------
		if (Input.GetKeyUp (KeyCode.B)) {
			SoundManager.Instance.PlayHorn ();

			if (Main.Instance.time >= Global.TIME_STOP_HORN || Main.Instance.time <= Global.TIME_START_HORN) {
				NotifierHandler.Instance.AddNotify ("[ff0000]Bam coi tu 22h-5h[-]");
			}
		}
	}
	
	#region COLLISION, CHECK POINT
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Collide: " + other.name);

		switch (other.name) {
		case OBJ.FINISH_POINT:
			Debug.LogError ("End Game!!!");
			break;

		case OBJ.RoadBorderRight:
		case OBJ.RoadBorderLeft:
		case OBJ.RoadBorderUp:
		case OBJ.RoadBorderDown:
			NotifierHandler.Instance.AddNotify ("[ff00ff]Va cham giao thong@@[-]");
			SoundManager.Instance.PlayCrash ();
			break;
		}
	}
	#endregion

	#region PLAYER STATE
	private void OnRoadChange (PlayerState oldState, PlayerState newState) {
		NotifierHandler.Instance.AddNotify (oldState.road.Direction + "=>" + newState.road.Direction);

		#region Vuot Den Do
		if (Ultil.IsRoad (newState.road.tile.typeId) 
		    && newState.road.Direction != oldState.road.Direction //Ko di cung chieu
		    && newState.road.Direction != Ultil.OppositeOf (oldState.road.Direction) //Ko di nguoc chieu
		    && oldState.road.LightStatus == TrafficLightStatus.red //Den dang do
		    && oldState.direction == oldState.road.Direction) { //Di dung chieu

			NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Vượt đèn đỏ[-]");
		}
		#endregion

		#region Vuot Den Vang
		if (Ultil.IsRoad (newState.road.tile.typeId) 
		    && newState.road.Direction != oldState.road.Direction
		    && newState.road.Direction != Ultil.OppositeOf (oldState.road.Direction)
		    && oldState.road.LightStatus == TrafficLightStatus.yellow
		    && oldState.direction == oldState.road.Direction) {

			NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ffff00]Vượt đèn vàng[-]");
		}
		#endregion

		#region Re tai noi ko cho re
		//1: re trai tai nga 3, 4
		if (newState.road.IsBus == false && oldState.road.IsBus == false) {
			if (newState.road.Direction != MoveDirection.NONE) {
				if (oldState.road.Direction == MoveDirection.NONE) { 							//Co Giao Lo
					PlayerState prev = Ultil.GetPreviousDiffState (oldState, queueStateDiff);
					if (prev != null) {
						if (newState.road.Direction == Ultil.LeftOf (prev.road.Direction)) {
							if (prev.road.CanTurnLeft == false) {
								NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Không được rẽ trái[-]");
							}
						}
					}
				} else if (newState.road.Direction == Ultil.LeftOf (oldState.road.Direction)) { //Ko Co Giao Lo
					PlayerState prev = oldState;
					if (prev.road.CanTurnLeft == false) {
						NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Không được rẽ trái[-]");
					}
				}
			}
		}
		
		//2: re phai tai nga 3, 4
		if (newState.road.IsBus == false && oldState.road.IsBus == false) {
			if (newState.road.Direction != MoveDirection.NONE) {
				if (oldState.road.Direction == MoveDirection.NONE) { 							//Co Giao Lo
					PlayerState prev = Ultil.GetPreviousDiffState (oldState, queueStateDiff);
					if (prev != null) {
						if (newState.road.Direction == Ultil.RightOf (prev.road.Direction)) {
							if (prev.road.CanTurnRight == false) {
								NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Không được rẽ phai[-]");
							}
						}
					}
				} else if (newState.road.Direction == Ultil.RightOf (oldState.road.Direction)) { //Ko Co Giao Lo
					PlayerState prev = oldState;
					if (prev.road.CanTurnRight == false) {
						NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Không được rẽ phai[-]");
					}
				}
			}
		}
		
		//3: quay dau xe
		if (newState.road.IsBus == false && oldState.road.IsBus == false) {
			if (newState.road.Direction != MoveDirection.NONE) {
				if (oldState.road.Direction == MoveDirection.NONE) { 							//Co Giao Lo
					PlayerState prev = Ultil.GetPreviousDiffState (oldState, queueStateDiff);
					if (prev != null) {
						if (newState.road.Direction == Ultil.OppositeOf (prev.road.Direction)) {//Quay nguoc lai
							if (prev.road.CanTurnBack == false || prev.road.CanTurnLeft == false) {
								NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Không được quay đầu xe[-]");
							}
						}
					}
				} else if (newState.road.Direction == Ultil.OppositeOf (oldState.road.Direction)) { //Ko Co Giao Lo
					NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Không được quay đầu xe[-]");
				}
			}
		}
		#endregion

		#region Re ko xi nhanh
		//1: re trai tai nga 3, 4
		if (newState.road.IsBus == false && oldState.road.IsBus == false) {
			if (newState.road.Direction != MoveDirection.NONE) {
				if (oldState.road.Direction == MoveDirection.NONE) {					//Co Giao Lo
					PlayerState prev = Ultil.GetPreviousDiffState (oldState, queueStateDiff);
					if (prev != null) {
						if (newState.road.Direction == Ultil.LeftOf (prev.road.Direction)) {
							if (prev.turnLight != TurnLight.LEFT && oldState.turnLight != TurnLight.LEFT && newState.turnLight != TurnLight.LEFT) {
								NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Rẽ trái không xi nhanh[-]");
							}
						}
					}
				} else if (newState.road.Direction == Ultil.LeftOf (oldState.road.Direction)) { //Ko Co Giao Lo
					PlayerState prev = oldState;
					if (prev.turnLight != TurnLight.LEFT  && newState.turnLight != TurnLight.LEFT) {
						NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Rẽ trái không xi nhanh[-]");
					}
				}
			}
		}

		//2: re phai tai nga 3, 4
		if (newState.road.IsBus == false && oldState.road.IsBus == false) {
			if (newState.road.Direction != MoveDirection.NONE) {
				if (oldState.road.Direction == MoveDirection.NONE) {					//Co Giao Lo
					PlayerState prev = Ultil.GetPreviousDiffState (oldState, queueStateDiff);
					if (prev != null) {
						if (newState.road.Direction == Ultil.RightOf (prev.road.Direction)) {
							if (prev.turnLight != TurnLight.RIGHT  && oldState.turnLight != TurnLight.RIGHT && newState.turnLight != TurnLight.RIGHT) {
								NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Rẽ phai không xi nhanh[-]");
							}
						}
					}
				} else if (newState.road.Direction == Ultil.RightOf (oldState.road.Direction)) {//Ko Co Giao Lo
					PlayerState prev = oldState;
					if (prev.turnLight != TurnLight.RIGHT && newState.turnLight != TurnLight.RIGHT) {
						NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Rẽ phai không xi nhanh[-]");
					}
				}
			}
		}

		//3: quay dau xe
		if (newState.road.IsBus == false && oldState.road.IsBus == false) {
			if (newState.road.Direction != MoveDirection.NONE) {
				if (oldState.road.Direction == MoveDirection.NONE) { 							//Co Giao Lo
					PlayerState prev = Ultil.GetPreviousDiffState (oldState, queueStateDiff);
					if (prev != null) {
						if (newState.road.Direction == Ultil.OppositeOf (prev.road.Direction)) {//Quay nguoc lai
							if (prev.turnLight != TurnLight.LEFT && oldState.turnLight != TurnLight.LEFT && newState.turnLight != TurnLight.LEFT) {
								NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Quay đầu xe không xi nhanh[-]");
							}
						}
					}
				}
			}
		}
		#endregion

		#region Xi nhanh nhung ko re

		#endregion
	}

	public void OnTurnLightChange (PlayerState oldState, PlayerState newState) {

		if (newState.turnLight != TurnLight.NONE) { 		//Turning On
			turnLight = newState.turnLight;
			startTurnLightTime = Time.realtimeSinceStartup;
			isTurnLightOn = true;
		} else {
			isTurnLightOn = false;
			turnLight = TurnLight.NONE;
			startTurnLightTime = -1;
		}
	}

	private void OnInRoadChange (PlayerState newState) {
		#region LANG LACH
//		PlayerState oldState = null;
//		object[] arr = queueState.ToArray ();
//
//		int index1 = -1;
//		//int index2 = -1;
//		for (int i = arr.Length-1; i >= 0; --i) {
//			PlayerState pl = arr[i] as PlayerState;
//			if (pl.road.tile.objId == newState.road.tile.objId) {
//				if (pl.inRoadPos != newState.inRoadPos) {
//					index1 = i;
//				} else {
//					if (index1 > -1) {
//						//index2 = i;
//						oldState = pl;
//						if (newState.time - oldState.time < Global.TIME_TO_LANGLACH) {
//							NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Lạng lách[-]");
//						}
//						break;
//					}
//				}
//			}
//		}
		#endregion
	}

	private void CheckState (PlayerState state) {
		//Khong doi mu bao hiem
		if (state.isHelmetOn == false && state.speed > Global.RUN_SPEED_POINT) {
			NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Không đội mũ bảo hiểm[-]");
		}

		//Nguoc chieu
		if (Ultil.IsOpposite (state.direction, state.road.Direction)) {
			NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Ngược chiều[-]");
		} else {

		}

		//Lan tuyen
		if (state.road.BikeAvailable == false) {
			NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Lấn tuyến[-]");
		}

		//Chay qua cham
		if (state.speed < state.road.MinSpeed) {
			NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Chạy dưới tốc độ tối thiểu[-]");
		}

		//Chay qua toc do
		if (state.speed > state.road.MaxSpeed) {
			//0-5
			//5-10
			//10-20
			//20-35
			//35
			float deltaSpeed = state.speed - state.road.MaxSpeed;
			if (deltaSpeed < 5) {
				NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Chạy quá tốc độ: 0-5 km/h[-]");
			} else if (deltaSpeed < 10) {
				NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Chạy quá tốc độ: 5-10 km/h[-]");
			} else if (deltaSpeed < 20) {
				NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Chạy quá tốc độ: 10-20 km/h[-]");
			} else if (deltaSpeed < 35) {
				NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Chạy quá tốc độ: 20-35 km/h[-]");
			} else {
				NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Chạy quá tốc độ: >35 km/h[-]");
			}
		}

		//Bat den chieu xa trong do thi
		if (state.isLightOn == true && state.isNearLight == false) {
			NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Đèn chiếu xa trong đô thị[-]");
		}

		//Khong bat den khi troi toi
		if (state.isLightOn == false && Main.Instance.needTheLight == true) {
			NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Không bật đèn khi trời tối[-]");
		}

	}

	public void OnStop (PlayerState oldState, PlayerState newState) {
		//Dung Tram Xe Bus
		if (newState.road.tile.typeId == TileID.ROAD_BUS_UP 
		    || newState.road.tile.typeId == TileID.ROAD_BUS_DOWN 
		    || newState.road.tile.typeId == TileID.ROAD_BUS_LEFT
		    || newState.road.tile.typeId == TileID.ROAD_BUS_RIGHT) 
		{
			NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Dừng xe tại trạm xe bus[-]");
		}

		//Dung xe giua nga 3,4
		//Dam vach
		if (newState.road.tile.typeId == 7) {
			switch (newState.vachKeDuong) {
			case MoveDirection.LEFT:
			case MoveDirection.RIGHT:
			case MoveDirection.UP:
			case MoveDirection.DOWN:
				NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Dậm vạch[-]");
				break;

			default:
				NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Dừng xe tại giao lộ[-]");
				break;
			}
		}
	}

	private PlayerState _currentState;
	private PlayerState _lastState;

	private void UpdateState () {
		_lastState = _currentState;
		_currentState = GetCurrentState ();

		if (_currentState.road == null) {
			Debug.Log ("null");
		} else {
			Ultil.AddToQueue (_currentState, queueState, QUEUE_SIZE);
			CheckState (_currentState);

			if (_lastState != null) {
				//Change Road
				if (_lastState.road.tile.objId != _currentState.road.tile.objId) {
					Ultil.AddToQueue (_lastState, queueStateDiff, QUEUE_SIZE);
					Ultil.AddToQueue (_currentState, queueStateDiff, QUEUE_SIZE);
					OnRoadChange (_lastState, _currentState);
				}

				//Turn Light Change
				if (_lastState.turnLight != _currentState.turnLight) {
					OnTurnLightChange (_lastState, _currentState);
				}

				//Change In Road
				if (_lastState.inRoadPos != _currentState.inRoadPos
				    && _lastState.road.tile.objId == _currentState.road.tile.objId) 
				{
					OnInRoadChange (_currentState);
				}
			} else {
				Ultil.AddToQueue (_currentState, queueStateDiff, QUEUE_SIZE);
			}

			//Check Stop
			if (_currentState.speed < Global.RUN_SPEED_POINT) {
				OnStop (_lastState, _currentState);
			}
		}
	}

	public PlayerState GetCurrentState () {
		PlayerState p = new PlayerState ();

		p.time = Time.realtimeSinceStartup;
		p.isHelmetOn = bikeHandler.isHelmetOn;
		p.isLightOn = bikeHandler.isLightOn;
		p.isNearLight = bikeHandler.isNearLight;
		p.turnLight = bikeHandler.turnLight;
		p.speed = bikeMovement.Speed;
		p.road = null;
		p.vachKeDuong = MoveDirection.NONE;

		//direction
		float x = transform.forward.x;
		float z = transform.forward.z;
		if (Mathf.Abs (x) > Mathf.Abs (z)) {
			//left-right
			if (x > 0) {
				p.direction = MoveDirection.LEFT;
			} else {
				p.direction = MoveDirection.RIGHT;
			}
		} else {
			//up-down
			if (z > 0) {
				p.direction = MoveDirection.DOWN;
			} else {
				p.direction = MoveDirection.UP;
			}
		}

		//Road
		RaycastHit hit;
		Ray rayDown = new Ray (transform.position + new Vector3 (0, 1, 0), Vector3.down);
		if (Physics.Raycast (rayDown, out hit)) {
			if (hit.transform.gameObject.name.Equals (OBJ.ROAD)) {
				p.road = hit.transform.gameObject.GetComponent <RoadHandler>();

				//InRoadPosition
				p.inRoadPos = p.road.CheckInOutLen (transform.position);
			} else {
				string name = hit.transform.gameObject.name;
				bool damVach = false;

				switch (name) {
				case OBJ.VachLeft:
					damVach = true;
					p.vachKeDuong = MoveDirection.LEFT;
					break;

				case OBJ.VachRight:
					damVach = true;
					p.vachKeDuong = MoveDirection.RIGHT;
					break;

				case OBJ.VachUp:
					damVach = true;
					p.vachKeDuong = MoveDirection.UP;
					break;

				case OBJ.VachDown:
					damVach = true;
					p.vachKeDuong = MoveDirection.DOWN;
					break;
				}

				if (damVach == true) {
					if (hit.transform.parent != null) {
						Transform parent2 = hit.transform.parent.parent;
						if (parent2 != null) {

							if (parent2.gameObject.name.Equals (OBJ.ROAD)) {
								p.road = parent2.gameObject.GetComponent <RoadHandler>();

								//InRoadPosition
								p.inRoadPos = p.road.CheckInOutLen (transform.position);
							}
						}
					}
				}
			}
		}
		
		return p;
	}
	#endregion
}
