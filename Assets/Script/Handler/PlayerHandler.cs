﻿using UnityEngine;
using System.Collections;

public class PlayerHandler : SingletonMono <PlayerHandler> {

	public const float DELAY_TIME = 1;
	public const float SCHEDULE_TIME = 0.25f;

	private BikeHandler bikeHandler;
	private BikeMovement2 bikeMovement;

	public const int QUEUE_SIZE = 50;
	private Queue queueState = new Queue (QUEUE_SIZE); 			//Danh sach sau moi SCHEDULE_TIME
	private Queue queueStateDiff = new Queue (QUEUE_SIZE);		//Danh sach state khi co chuyen lan duong

	void Awake () {
		bikeHandler = gameObject.GetComponent <BikeHandler> ();
		bikeMovement = gameObject.GetComponent <BikeMovement2> ();
	}

	void Start () {
		InvokeRepeating ("UpdateState", DELAY_TIME, SCHEDULE_TIME);
	}

	void Update () {
	}

	#region COLLISION, CHECK POINT
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Collide: " + other.name);

		switch (other.name) {
		case OBJ.FINISH_POINT:
			Debug.LogError ("End Game!!!");
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
		if (newState.road.IsBus == false && oldState.road.IsBus == false) {//Debug.Log ("1");
			if (newState.road.Direction != MoveDirection.NONE) {//Debug.Log ("2");
				if (oldState.road.Direction == MoveDirection.NONE) {//Debug.Log ("3"); 							//Co Giao Lo
					PlayerState prev = Ultil.GetPreviousDiffState (oldState, queueStateDiff);
					if (prev != null) {//Debug.Log ("4");
						if (newState.road.Direction == Ultil.LeftOf (prev.road.Direction)) {//Debug.Log ("5");
							if (prev.turnLight != TurnLight.LEFT) {//Debug.Log ("6");
								NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Rẽ trái không xi nhanh[-]");
							}
						}
					}
				} else if (newState.road.Direction == Ultil.LeftOf (oldState.road.Direction)) { //Ko Co Giao Lo
					PlayerState prev = oldState;
					if (prev.turnLight != TurnLight.LEFT) {
						NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Rẽ trái không xi nhanh[-]");
					}
				}
			}
		}

		//2: re phai tai nga 3, 4
		if (newState.road.IsBus == false && oldState.road.IsBus == false) {Debug.Log ("1");
			if (newState.road.Direction != MoveDirection.NONE) {Debug.Log ("2");
				if (oldState.road.Direction == MoveDirection.NONE) {Debug.Log ("3");					//Co Giao Lo
					PlayerState prev = Ultil.GetPreviousDiffState (oldState, queueStateDiff);
					if (prev != null) {Debug.Log ("4");
						Debug.Log (newState.road.Direction);
						Debug.Log (newState.time);
						Debug.Log (newState.turnLight);
						Debug.Log ("--");
						Debug.Log (prev.road.Direction);
						Debug.Log (prev.time);
						Debug.Log (prev.turnLight);
						Debug.Log ("---------");
						if (newState.road.Direction == Ultil.RightOf (prev.road.Direction)) {Debug.Log ("5");
							if (prev.turnLight != TurnLight.RIGHT) {Debug.Log ("6");
								NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Rẽ phai không xi nhanh[-]");
							}
						}
					}
				} else if (newState.road.Direction == Ultil.RightOf (oldState.road.Direction)) { Debug.Log ("a");//Ko Co Giao Lo
					PlayerState prev = oldState;
					if (prev.turnLight != TurnLight.RIGHT) {Debug.Log ("b");
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
							if (prev.turnLight != TurnLight.LEFT) {
								NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Quay đầu xe không xi nhanh[-]");
							}
						}
					}
				}
			}
		}
		#endregion
	}

	private void OnInRoadChange (PlayerState newState) {
		PlayerState oldState = null;
		object[] arr = queueState.ToArray ();

		int index1 = -1;
		//int index2 = -1;
		for (int i = arr.Length-1; i >= 0; --i) {
			PlayerState pl = arr[i] as PlayerState;
			if (pl.road.tile.objId == newState.road.tile.objId) {
				if (pl.inRoadPos != newState.inRoadPos) {
					index1 = i;
				} else {
					if (index1 > -1) {
						//index2 = i;
						oldState = pl;
						if (newState.time - oldState.time < Global.TIME_TO_LANGLACH) {
							NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Lạng lách[-]");
						}
						break;
					}
				}
			}
		}
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
			NotifierHandler.Instance.AddNotify ((int)Time.realtimeSinceStartup + "s: [ff0000]Chạy quá tốc độ[-]");
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

		//Dung xe ko co tin hieu, hoac tin hieu sai

	}

	private PlayerState _currentState;
	private PlayerState _lastState;

	private void UpdateState () {
		_lastState = _currentState;
		_currentState = GetCurrentState ();

		Ultil.AddToQueue (_currentState, queueState, QUEUE_SIZE);
		
		CheckState (_currentState);

		if (_lastState != null) {
			//Change Road
			if (_lastState.road.tile.objId != _currentState.road.tile.objId) {
				Ultil.AddToQueue (_lastState, queueStateDiff, QUEUE_SIZE);
				Ultil.AddToQueue (_currentState, queueStateDiff, QUEUE_SIZE);

				OnRoadChange (_lastState, _currentState);
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

	public PlayerState GetCurrentState () {
		PlayerState p = new PlayerState ();

		p.time = Time.realtimeSinceStartup;
		p.isHelmetOn = bikeHandler.isHelmetOn;
		p.isLightOn = bikeHandler.isLightOn;
		p.isNearLight = bikeHandler.isNearLight;
		p.turnLight = bikeHandler.turnLight;
		p.speed = bikeMovement.Speed;
		p.road = null;

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
				p.inRoadPos = p.road.CheckPosition (transform.position);
			}
		}

		return p;
	}
	#endregion
}
