using UnityEngine;
using System;
using System.IO;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class Main : SingletonMono<Main> {

	private DateTime _startTime;
	public DateTime time;

	void Start () {
		MapManager.Instance.Init ();
	}

	void Update () {
		TrafficLightManager.Instance.Update ();

		time = _startTime.AddSeconds (Time.realtimeSinceStartup);
		UI2DManager.Instance.SetTime (time);
	}

	public void SetStartTime (DateTime d) {
		_startTime = d;
		time = _startTime;
	}
}
