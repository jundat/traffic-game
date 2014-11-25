using UnityEngine;
using System;
using System.IO;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class Main : SingletonMono<Main> {

	public Light mainLight;
	public Material daySkybox;
	public Material nightSkybox;
	
	public AudioClip dayMusic;
	public AudioClip nightMusic;

	private DateTime _startTime;
	public DateTime time;
	public bool needTheLight = false;


	void Start () {
		//StartCoroutine (InitScene ());

		//Debug.Log ("Begin: " + Time.realtimeSinceStartup);
		MapManager.Instance.Init ();
		//Debug.Log ("Finish: " + Time.realtimeSinceStartup);
	}

//	public IEnumerator InitScene () {
//
//	}
	
	void Update () {
		TrafficLightManager.Instance.Update ();

		time = _startTime.AddSeconds (Time.realtimeSinceStartup);
		UI2DManager.Instance.SetWorldTime (time);
	}

	public void SetStartTime (DateTime d) {
		_startTime = d;
		time = _startTime;

		//Update state
		if (time.Hour > Global.TIME_START_LIGHT || time.Hour < Global.TIME_STOP_LIGHT) {
			needTheLight = true;
			mainLight.intensity = 0.1f;
			RenderSettings.skybox = nightSkybox;

			audio.clip = nightMusic;
			audio.Play ();

		} else {
			needTheLight = false;
			mainLight.intensity = 0.5f;
			RenderSettings.skybox = daySkybox;
			
			audio.clip = dayMusic;
			audio.Play ();

		}
	}
}
