using UnityEngine;
using System;
using System.Collections;

public enum LightStatus {
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

	private LightStatus status = LightStatus.none;

	public void Init (ModelTile tile) {
		this.tile = tile;
	}

	public LightStatus Status {
		get {
			return status;
		}
		set {
			if (status != value) {
				status = value;
				road.LightStatus = status;

				switch (status) {
				case LightStatus.green:
					redLight.material = black;
					yellowLight.material = black;
					greenLight.material = green;
					break;

				case LightStatus.red:
					redLight.material = red;
					yellowLight.material = black;
					greenLight.material = black;
					break;

				case LightStatus.yellow:
					redLight.material = black;
					yellowLight.material = yellow;
					greenLight.material = black;
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
