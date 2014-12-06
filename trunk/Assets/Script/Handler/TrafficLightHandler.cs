using UnityEngine;
using System;
using System.Collections;

public enum TrafficLightStatus {
	none,
	green,
	yellow,
	red
} 

public class TrafficLightHandler : TileHandler {

	public RoadHandler road;

	public Material red;
	public Material yellow;
	public Material green;
	public Material black;

	public MeshRenderer redLight;
	public MeshRenderer yellowLight;
	public MeshRenderer greenLight;

	public MeshRenderer redLight2;
	public MeshRenderer yellowLight2;
	public MeshRenderer greenLight2;


	private TrafficLightStatus status = TrafficLightStatus.none;

	public void Init (ModelTile tile) {
		this.tile = tile;
	}

	public TrafficLightStatus Status {
		get {
			return status;
		}
		set {
			if (status != value) {
				status = value;
				road.LightStatus = status;

				switch (status) {
				case TrafficLightStatus.green:
					redLight.material = black;
					yellowLight.material = black;
					greenLight.material = green;
					//
					redLight2.material = black;
					yellowLight2.material = black;
					greenLight2.material = green;
					break;

				case TrafficLightStatus.red:
					redLight.material = red;
					yellowLight.material = black;
					greenLight.material = black;
					//
					redLight2.material = red;
					yellowLight2.material = black;
					greenLight2.material = black;
					break;

				case TrafficLightStatus.yellow:
					redLight.material = black; 	//MissingReferenceException: The object of type 'MeshRenderer' has been destroyed but you are still trying to access it.
												//Your script should either check if it is null or you should not destroy the object.
					yellowLight.material = yellow;
					greenLight.material = black;
					//
					redLight2.material = black;
					yellowLight2.material = yellow;
					greenLight2.material = black;
					break;
				}
			}
		}
	}

	void Start () {}

	void Update () {}

	public string Direction {
		get {
			string s = Ultil.GetString (TileKey.LIGHT_HUONG, "UP", this.tile.properties);
			return s;
		}
	}
}
