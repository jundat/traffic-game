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
		up.SetActive (false);
		down.SetActive (false);
		left.SetActive (false);
		right.SetActive (false);
		
		switch (tile.typeId) {
		case 1: //down
			left.SetActive (true);
			right.SetActive (true);
			break;
			
		case 2://left
			up.SetActive (true);
			down.SetActive (true);
			break;
			
		case 3://right
			up.SetActive (true);
			down.SetActive (true);
			break;
			
		case 4://up
			left.SetActive (true);
			right.SetActive (true);
			break;
		}
	}
}
