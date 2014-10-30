using UnityEngine;
using System.Collections;

public class Global {
	public const float DELTA_HEIGH = 0.001f;

	public const float SCALE_TILE = 1.0f / 32; //0.03125f; // 1/32
	public const float SCALE_SIZE = 10.0f;

	public const string ROAD_RES = "1";
	public const string SIGN_RES = "100";
	public const string VIEW_RES = "200";
	public const string OTHER_RES = "300";
}


public struct MyDirection {
	public const string UP = "UP";
	public const string DOWN = "DOWN";
	public const string LEFT = "LEFT";
	public const string RIGHT = "RIGHT";
}