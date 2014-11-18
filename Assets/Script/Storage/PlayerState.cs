using UnityEngine;
using System.Collections;

public class PlayerState {

	public RoadHandler road;
	public InRoadPosition inRoadPos;
	public MoveDirection direction;
	public MoveDirection vachKeDuong;
	public float time;
	public bool isHelmetOn;
	public bool isLightOn;
	public bool isNearLight;
	public TurnLight turnLight; //-1: left, 0: none, 1: right

	public float speed; //real 0->160 km/h

	public PlayerState () {}

	public PlayerState Copy () {
		return new PlayerState ();
	}
}
