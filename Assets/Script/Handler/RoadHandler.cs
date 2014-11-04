using UnityEngine;
using System.Collections;

public class RoadHandler : TileHandler {

	private LightStatus lightStatus = LightStatus.none;

	public LightStatus LightStatus {
		get {
			return lightStatus;
		}
		set {
			lightStatus = value;

			//-------- debug --------
			if (Global.DEBUG_LIGHT) { 
				MeshRenderer render = gameObject.GetComponent <MeshRenderer> ();
				switch (lightStatus) {
				case LightStatus.none:
					render.material.color = Color.white;
					break;

				case LightStatus.green:
					render.material.color = Color.green;
					break;

				case LightStatus.red:
					render.material.color = Color.red;
					break;
					
				case LightStatus.yellow:
					render.material.color = Color.yellow;
					break;
				}
			}
		}
	}

	void Start () {}
	
	void Update () {}
}
