using UnityEngine;
using System.Collections;

public class RoadHandler : TileHandler {

	private TrafficLightStatus lightStatus = TrafficLightStatus.none;

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

	void Start () {}
	
	void Update () {}
}
