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
		bool isUp = bool.Parse ( Ultil.GetString (TileKey.LE_TREN, "false", tile.properties));
		bool isDown = bool.Parse ( Ultil.GetString (TileKey.LE_DUOI, "false", tile.properties));
  		bool isRight = bool.Parse ( Ultil.GetString (TileKey.LE_PHAI, "false", tile.properties));
   		bool isLeft = bool.Parse ( Ultil.GetString (TileKey.LE_TRAI, "false", tile.properties));

		up.SetActive (isUp);
		down.SetActive (isDown);
		left.SetActive (isLeft);
		right.SetActive (isRight);
	}
}
