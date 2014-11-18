using UnityEngine;
using System;
using System.IO;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class Main : SingletonMono<Main> {

	public Light mainLight;
	public Material daySkybox;
	public Material nightSkybox;

	private DateTime _startTime;
	public DateTime time;
	public bool needTheLight = false;


	void Start () {
		MapManager.Instance.Init ();
	}

	void Update () {
		TrafficLightManager.Instance.Update ();

		time = _startTime.AddSeconds (Time.realtimeSinceStartup);
		UI2DManager.Instance.SetTime (time);

		if (time.Hour > Global.TIME_START_LIGHT || time.Hour < Global.TIME_STOP_LIGHT) {
			needTheLight = true;
			mainLight.intensity = 0.1f;
			RenderSettings.skybox = nightSkybox;

		} else {
			needTheLight = false;
			mainLight.intensity = 0.5f;
			RenderSettings.skybox = daySkybox;

		}
	}

	public void SetStartTime (DateTime d) {
		_startTime = d;
		time = _startTime;
	}
}
