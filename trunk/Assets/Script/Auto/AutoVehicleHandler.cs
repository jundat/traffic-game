using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AutoVehicleHandler : TileHandler {
	
	const float START_DELAY = 4.0f;
	const float UPDATE_INTERVAL = 0.2f;
	const float STOP_SPEED = 0.001f;
	const float DELTA_TO_ROAD = 8;
	const float RANDOM_INROAD = 4.0f / 5.0f;
	const int MIN_SPEED = 20;
	const int MAX_SPEED = 40;
	const int TIME_REMOVE_SELF = 2;
	const int DEADLOCK_LEVEL = 3;

	public List<GameObject> listModel = new List<GameObject> ();
	public List<AutoCollider> listCollider = new List<AutoCollider> ();
	public List<Collider> listCollision = new List<Collider> ();

	public float SPEED = 30;
	public float currentSpeed;

	public bool isInJunction = false; //giao lo
	public TweenRotation tweenFront;
	public TweenRotation tweenRear;
	public Collider autoCollider;

	public GameObject light;
	
	public bool isRun = false;
	public bool isPrioritized = false; //duoc quyen uu tien chay truoc
	public int currentPos = -1;
	public int currentDest = 0;
	public List<Vector3> listDest = new List<Vector3> ();

	const float ACCEL_UP = 2.0f;
	const float ACCEL_DOWN = -2.0f;
	const float ACCEL_NORMAL = 0;
	public float accelerate = ACCEL_UP;
	
	void Start () {
		SPEED = Ultil.random.Next (MIN_SPEED, MAX_SPEED);
		currentSpeed = 0;
		accelerate = ACCEL_UP;
		for (int i = 0; i < listCollider.Count; ++i) {
			listCollider[i].onCollideEnter = this.CallbackCollideEnter;
			listCollider[i].onCollideExit = this.CallbackCollideExit;
		}

		//Random model
		int count = listModel.Count;
		if (count > 0) {
			int rd = Ultil.random.Next (0, count);
			for (int i = 0; i < count; ++i) {
				listModel[i].SetActive (false);
			}
			listModel[rd].SetActive (true);
		}

		//lights
		light.SetActive (Main.Instance.needTheLight);

		Invoke ("StartRun", START_DELAY);
	}
	
	void StartRun () {
		InitDestination ();
		CheckRoad ();
	}
	
	void InitDestination () {
		
		listDest.Add (transform.position);
		currentPos = -1;
		currentDest = 0;
		
		RoadHandler road = Ultil.RayCastRoad (this.transform.position + new Vector3 (0,1,0), Vector3.down);
		if (road != null) {
			Vector3 p = transform.position;
			Vector3 v;

			switch (road.Direction) {
			case MoveDirection.UP:
				v = new Vector3 (p.x, p.y, p.z);
				v.z = road.anchorUp.transform.position.z - DELTA_TO_ROAD;
				listDest.Add (v); 
				break;
				
			case MoveDirection.DOWN:
				v = new Vector3 (p.x, p.y, p.z);
				v.z = road.anchorDown.transform.position.z + DELTA_TO_ROAD;
				listDest.Add (v); 
				break;
				
			case MoveDirection.LEFT:
				v = new Vector3 (p.x, p.y, p.z);
				v.x = road.anchorLeft.transform.position.x + DELTA_TO_ROAD;
				listDest.Add (v); 
				break;
				
			case MoveDirection.RIGHT:
				v = new Vector3 (p.x, p.y, p.z);
				v.x = road.anchorRight.transform.position.x - DELTA_TO_ROAD;
				listDest.Add (v); 
				break;
				
			default: //O giua giao lo
				CheckRoad ();
				break;
			}
			
			NextStep ();
		} else {
			Debug.LogError ("Car not in road! => Destroy");
			//Destroy (gameObject);
			Debug.Break ();
		}
	}
	
	void Update () {
		if (isRun) {

			currentSpeed = currentSpeed + accelerate;

			if (currentSpeed > SPEED) {
				currentSpeed = SPEED;
				accelerate = ACCEL_NORMAL;
			} else if (currentSpeed < 0) {
				currentSpeed = 0;
				if (listCollision.Count == 0) {
					accelerate = ACCEL_UP;
				} else {
					accelerate = ACCEL_NORMAL;
				}
			}

			Vector3 step = (listDest[currentDest] - transform.position) / Vector3.Distance (transform.position, listDest[currentDest]);
			Vector3 move = step * currentSpeed * Time.deltaTime;
			transform.position = transform.position + move;
			transform.LookAt (listDest[currentDest]);
			
			if (Vector3.Distance (transform.position, listDest[currentDest]) < move.magnitude) {
				transform.position = listDest[currentDest];
				//CheckRoad ();
				NextStep ();
			}
			
			if (currentSpeed != 0) {
				tweenFront.duration = tweenRear.duration = 20.0f / currentSpeed;
			}
		} else {
			tweenFront.duration = tweenRear.duration = 1000;
		}
	}
	
	public void Init () {}
	
	private void NextStep () {
		if (listDest.Count > currentDest + 1) {
			currentDest++;
			currentPos++;
			isRun = true;

			transform.LookAt (listDest[currentDest]);
			
			if (Vector3.Distance (listDest[currentDest], transform.position) < 0.001f) {
				NextStep ();
			}
			
		} else {
			//Debug.Log ("No next destination");
			isRun = false;
			isInJunction = false;
			CheckRoad ();
		}
	}
	
	public void CheckRoad () {
		RoadHandler road = Ultil.RayCastRoad (this.transform.position + new Vector3 (0,1,0), Vector3.down);
		if (road != null) {

			if (road.tile.typeId == TileID.ROAD_NONE) {

				if (isInJunction == false) {
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
						isInJunction = true;
						
						int randomIndex = Ultil.random.Next (0, count);
						RoadHandler nextRoad = listAvai[randomIndex];
						
						//Move in bezier
						CalculateNextDest (nextRoad, road);
					} else {
						currentSpeed = STOP_SPEED;
					}
				}
			} else {
				isInJunction = false;
			}
		} else {
			isInJunction = false;
		}
	}
	
	private void CalculateNextDest (RoadHandler nextRoad, RoadHandler currentRoad) {

		bool isOpposite = false;
		MoveDirection currentDirection = Ultil.GetMoveDirection (this.transform.forward);
		if (Ultil.IsOpposite (nextRoad.Direction, currentDirection)) {
			isOpposite = true;
		}
		
		//get point A1, A2, A3, A4
		Vector3 a1 = transform.position;
		Vector3 a2 = a1;
		
		float hRoad = Mathf.Abs (currentRoad.anchorUp.transform.position.z - currentRoad.anchorDown.transform.position.z);
		float wRoad = Mathf.Abs (currentRoad.anchorLeft.transform.position.x - currentRoad.anchorRight.transform.position.x);
		
		switch (currentDirection) {
		case MoveDirection.UP:
			a2.z -= hRoad/4;
			break;
			
		case MoveDirection.DOWN:
			a2.z += hRoad/4;
			break;
			
		case MoveDirection.LEFT:
			a2.x += wRoad/4;
			break;
			
		case MoveDirection.RIGHT:
			a2.x -= wRoad/4;
			break;
		}
		
		Vector3 a3 = Vector3.one;
		Vector3 a4 = Vector3.one;
		
		float hNextRoad = Mathf.Abs (nextRoad.anchorUp.transform.position.z - nextRoad.anchorDown.transform.position.z);
		float wNextRoad = Mathf.Abs (nextRoad.anchorLeft.transform.position.x - nextRoad.anchorRight.transform.position.x);
		
		float deltaX = (float)(Ultil.random.NextDouble () * wNextRoad * RANDOM_INROAD);
		float deltaZ = (float)(Ultil.random.NextDouble () * hNextRoad * RANDOM_INROAD);
		deltaX -= deltaX/2;
		deltaZ -= deltaZ/2;
		
		switch (nextRoad.Direction) {
		case MoveDirection.DOWN: //quay dau
			a3 = nextRoad.anchorUp.transform.position;
			a4 = a3;
			a4.z += hRoad/4;
			
			//random
			a3.x += deltaX;
			a4.x += deltaX;
			
			if (isOpposite) {
				a3.z -= hRoad/4;
				a4.z -= hRoad/4;
			}
			
			break;
			
		case MoveDirection.UP: //quay dau
			a3 = nextRoad.anchorDown.transform.position;
			a4 = a3;
			a4.z -= hRoad/4;
			
			//random
			a3.x += deltaX;
			a4.x += deltaX;
			
			if (isOpposite) {
				a3.z += hRoad/4;
				a4.z += hRoad/4;
			}
			
			break;
			
		case MoveDirection.LEFT: //quay dau
			a3 = nextRoad.anchorRight.transform.position;
			a4 = a3;
			a4.x += wRoad/4;
			
			//random
			a3.z += deltaZ;
			a4.z += deltaZ;
			
			if (isOpposite) {
				a3.x -= wRoad/4;
				a4.x -= wRoad/4;
			}
			
			break;
			
		case MoveDirection.RIGHT: //quay dau
			a3 = nextRoad.anchorLeft.transform.position;
			a4 = a3;
			a4.x -= wRoad/4;
			
			//random
			a3.z += deltaZ;
			a4.z += deltaZ;
			
			if (isOpposite) {
				a3.x += wRoad/4;
				a4.x += wRoad/4;
			}
			
			break;
		}
		
		//------------
		
		BezierCurve bc = new BezierCurve ();
		
		List<double> ptList = new List<double>();
		ptList.Add (a1.x); //
		ptList.Add (a1.z);
		ptList.Add (a2.x); //
		ptList.Add (a2.z);
		ptList.Add (a3.x); //
		ptList.Add (a3.z);
		ptList.Add (a4.x); //
		ptList.Add (a4.z);
		
		// how many points do you need on the curve?
		int POINTS_ON_CURVE = 80;
		
		double[] ptind = new double[ptList.Count];
		double[] curvePoints = new double[POINTS_ON_CURVE];
		ptList.CopyTo (ptind, 0);
		
		bc.Bezier2D(ptind, (POINTS_ON_CURVE) / 2, curvePoints);
		
		//remove 2 first + 1 last element
		for (int i = 3; i != POINTS_ON_CURVE-3; i += 2)
		{
			Vector3 newDest = new Vector3 ((float)curvePoints[i+1], transform.position.y, (float)curvePoints[i]);
			listDest.Add (newDest); 
		}
		
		a4.y = transform.position.y;
		listDest.Add (a4); 
		
		//-----------------------------------------------
		//Destination from next road
		Vector3 curPos = a4;
		Vector3 v2;
		
		switch (nextRoad.Direction) {
		case MoveDirection.UP:
			v2 = new Vector3 (curPos.x, curPos.y, curPos.z);
			v2.z = nextRoad.anchorUp.transform.position.z - DELTA_TO_ROAD;
			listDest.Add (v2); 
			break;
			
		case MoveDirection.DOWN:
			v2 = new Vector3 (curPos.x, curPos.y, curPos.z);
			v2.z = nextRoad.anchorDown.transform.position.z + DELTA_TO_ROAD;
			listDest.Add (v2); 
			break;
			
		case MoveDirection.LEFT:
			v2 = new Vector3 (curPos.x, curPos.y, curPos.z);
			v2.x = nextRoad.anchorLeft.transform.position.x + DELTA_TO_ROAD;
			listDest.Add (v2); 
			break;
			
		case MoveDirection.RIGHT:
			v2 = new Vector3 (curPos.x, curPos.y, curPos.z);
			v2.x = nextRoad.anchorRight.transform.position.x - DELTA_TO_ROAD;
			listDest.Add (v2); 
			break;
			
		default:
			Debug.Log ("Something wrong here!");
			break;
		}
		
		if (isRun == false) {
			NextStep ();
		}
	}

	public void CallbackCollideEnter (Collider col, AutoCollider side) {
		string sideName = side.gameObject.name;
		string colName = col.gameObject.name;

		if (colName == OBJ.AUTOCAR || colName == OBJ.AUTOBIKE || colName == OBJ.PLAYER || colName == OBJ.RED_STOP) 
		{
			if (sideName == AutoCollider.FAR_FRONT) {
				if (listCollision.Count == 0) {
					if (isInJunction == false) {
						accelerate = ACCEL_DOWN;
					}
				}
			}
			
			if (sideName == AutoCollider.FRONT) {
				listCollision.Add (col);
				currentSpeed = STOP_SPEED;
				accelerate = ACCEL_NORMAL;

				//check in deadlock
				AutoVehicleHandler other = col.gameObject.GetComponentInParent <AutoVehicleHandler>();
				if (other != null) {
					CheckInDeadLock (other, 1);
				}
			}
		}
	}

	void CheckInDeadLock (AutoVehicleHandler other, int level) {
		if (level > DEADLOCK_LEVEL) {
			return;
		}

		AutoVehicleHandler c1 = other;

		if (c1.listCollision.Contains (this.autoCollider)) { //Cap 1
			SolveDeadlock (level);
		} else {
			for (int i = 0; i < c1.listCollision.Count; ++i) {
				Collider col = c1.listCollision[i];
				AutoVehicleHandler c2 = col.gameObject.GetComponentInParent <AutoVehicleHandler>();
				if (c2 != null) {
					CheckInDeadLock (c2, level + 1);
				}
			}
		}
	}

	void SolveDeadlock (int level) {
		Debug.LogError ("Solve Deadlock: " + level);

		//Copy other's destination
		transform.position = transform.position + new Vector3 (0, -30, 0);
		isRun = false;
		currentSpeed = 0;
		accelerate = 0;
		
		//auto remove after
		Invoke ("RemoveSelf", TIME_REMOVE_SELF);
	}

	void RemoveSelf () {
		Destroy (gameObject);
	}
	
	public void CallbackCollideExit (Collider col, AutoCollider side) {
		string sideName = side.gameObject.name;
		string colName = col.gameObject.name;
		
		if (colName == OBJ.AUTOCAR || colName == OBJ.AUTOBIKE || colName == OBJ.PLAYER || colName == OBJ.RED_STOP)
		{
			if (sideName == AutoCollider.FRONT) {
				listCollision.Remove (col);
				if (listCollision.Count == 0) {
					accelerate = ACCEL_UP;
				}
			}
			
			if (sideName == AutoCollider.FAR_FRONT) {
				if (listCollision.Count == 0) {
					accelerate = ACCEL_UP;
				}
			}
		}
	}
}
