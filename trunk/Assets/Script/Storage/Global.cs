using UnityEngine;
using System.Collections;

public class Global {

	public const bool DEBUG_LIGHT = true;

	public const float DELTA_HEIGH = 0.001f;

	public const float SCALE_TILE = 1.0f / 32; //0.03125f; // 1/32
	public const float SCALE_SIZE = 10.0f;

	public const string ROAD_RES = "1";
	public const string SIGN_RES = "100";
	public const string VIEW_RES = "200";
	public const string OTHER_RES = "300";

	public const string LAYER_OTHER = "1";
	public const string LAYER_VIEW = "2";
	public const string LAYER_SIGN = "3";
	public const string LAYER_ROAD = "4";
}


public struct MyDirection {
	public const string UP = "UP";
	public const string DOWN = "DOWN";
	public const string LEFT = "LEFT";
	public const string RIGHT = "RIGHT";
}

public class OBJ {
	public const string START_POINT = "302(Clone)";
	public const string FINISH_POINT = "303(Clone)";
	public const string CHECK_POINT = "304(Clone)";
}