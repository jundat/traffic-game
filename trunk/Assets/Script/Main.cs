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

	public bool isStarted = false;
	public bool isEndGame = false;
	public DateTime startTime;

	private DateTime _startTime;
	public DateTime time;
	public bool needTheLight = false;

	public PlayerHandler player;

	void Start () {
		UI2DManager.Instance.ShowHideTutorial (true);
		MapManager.Instance.Init ();
	}
	
	void Update () {
		//World Time
		time = _startTime.AddSeconds (Time.realtimeSinceStartup);
		UI2DManager.Instance.SetWorldTime (time);

		if (Main.Instance.isStarted == false || Main.Instance.isEndGame == true) {return;}

		TrafficLightManager.Instance.Update ();

		//Run Time 
		if (Main.Instance.isStarted) {
			int pastSeconds = (int)((Main.Instance.time - startTime).TotalSeconds);
			int totalSecond = (int)(60.0f * MapManager.Instance.mapNetwork.time);
			int remainSeconds = totalSecond - pastSeconds;
			
			TimeSpan span = new TimeSpan (0, 0, remainSeconds);
			
			if (span.TotalSeconds >= 0) {
				UI2DManager.Instance.SetRemainTime (span);
			} else {
				Debug.LogError ("Out of time :(");
				Main.Instance.isEndGame = true;
				SoundManager.Instance.PlayOutOfTime ();
				ShowEndGame ();
			}
		}
	}

	public void OnStartGame () {
		Main.Instance.isStarted = true;
		Main.Instance.startTime = Main.Instance.time;
		SoundManager.Instance.PlayStart ();
	}

	public void OnCompleteGame () {
		Debug.LogError ("Complete game! :D");
		Main.Instance.isEndGame = true;
		SoundManager.Instance.PlayCompleteGame ();
		ShowEndGame ();
	}

	public void ShowEndGame () {
		player.StopRunning ();
		UI2DManager.Instance.ShowHideTutorial (false);
		UI2DManager.Instance.ShowScore ();
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
