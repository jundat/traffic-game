using UnityEngine;
using System;
using System.IO;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class Main : MonoBehaviour {

	void Start () {
		MapManager.Instance.Init ();
	}

	void Update () {
		TrafficLightManager.Instance.Update ();
	}
}
