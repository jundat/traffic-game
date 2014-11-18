using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour {

	public RoadHandler road;
	public GameObject objTest;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTest () {
		InRoadPosition pos = road.CheckInOutLen (objTest.transform.position);

		Debug.Log (pos);
	}
}
