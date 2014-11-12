using UnityEngine;
using System.Collections;

public class PlayerState {

	public RoadHandler road;
	public MoveDirection direction;
	public float time;
	public bool isHelmetOn;
	public bool isLightOn;
	public bool isNearLight;
	public int leftRightLight; //-1: left, 0: none, 1: right

	public float speed; //real 0->160 km/h

	public PlayerState () {}

	public PlayerState Copy () {
		return new PlayerState ();
	}
}
