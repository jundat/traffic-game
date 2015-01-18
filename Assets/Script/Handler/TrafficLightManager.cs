using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrafficLightManager : SingletonMono <TrafficLightManager> {

	public const float RED_TIME = 20;
	public const float YELLOW_TIME = 3;
	public const float GREEN_TIME = RED_TIME - YELLOW_TIME;
	public const float TOTAL_TIME = RED_TIME + YELLOW_TIME + GREEN_TIME;

	public List<TrafficLightHandler> lightsUpdown = new List<TrafficLightHandler> ();
	public List<TrafficLightHandler> lightsLeftRight = new List<TrafficLightHandler> ();

	float time = 0;

	void Start () {}

	void Update () {
		time += Time.deltaTime;
		if (time >= TOTAL_TIME) {
			time = 0;
		}

		//up-down
		TrafficLightStatus ud = GetStatus (time);
		TrafficLightStatus lr = GetStatus (time + RED_TIME);

		for (int i = 0; i < lightsUpdown.Count; ++i) {
			lightsUpdown[i].Status = ud;
		}

		for (int i = 0; i < lightsLeftRight.Count; ++i) {
			lightsLeftRight[i].Status = lr;
		}
	}

	public void AddLight (TrafficLightHandler light) {
		if (light.Direction == MoveDirection.UP.ToString() || light.Direction == MoveDirection.DOWN.ToString()) {
			lightsUpdown.Add (light);
		} else {
			lightsLeftRight.Add (light);
		}
	}

	private TrafficLightStatus GetStatus (float t) {
		int t2 = (int)t;
		int total = (int) TOTAL_TIME;

		t2 %= total;

		if (t2 < GREEN_TIME) {
			return TrafficLightStatus.green;
		} else if (t2 < GREEN_TIME + YELLOW_TIME) {
			return TrafficLightStatus.yellow;
		} else {
			return TrafficLightStatus.red;
		}
	}
}
