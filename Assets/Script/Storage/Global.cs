using UnityEngine;
using System;
using System.Collections;

public class Global {
	public const float ZERO_POINT = 0.001f;

//#if UNITY_EDITOR
	public const bool DEBUG_BUIDING = true;
//#else
//	public const bool DEBUG_BUIDING = true;
//#endif

	public const bool DEBUG_LIGHT = false;

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

	public const int DEF_MAX_TOCDO = 40;
	public const int DEF_MIN_TOCDO = 0;

	public const int RUN_SPEED_POINT = 1;

	public const float IN_BORDER_PERCENT = 0.15f;

	public const float TIME_TO_LANGLACH = 3.0f;

	public static DateTime TIME_STOP_HORN = new DateTime (DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 0, 0);
	public static DateTime TIME_START_HORN = new DateTime (DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 0, 0);
}

public enum MoveDirection {
	NONE,
	UP,
	DOWN,
	LEFT,
	RIGHT
}


public class OBJ {
	public const string START_POINT = "302(Clone)";
	public const string FINISH_POINT = "303(Clone)";
	public const string CHECK_POINT = "304(Clone)";

	public const string ROAD = "Road(Clone)";

	public const string RoadBorderRight 	= "RoadBorderRight";
	public const string RoadBorderLeft 		= "RoadBorderLeft";
	public const string RoadBorderUp 		= "RoadBorderUp";
	public const string RoadBorderDown 		= "RoadBorderDown";


}

public enum VihicleType {
	MoToA1,
	MoToA2,
	MoToA3,
	Oto,
	XeDap,
	XeKhach,
	XeTai,
	Romooc,
	XeLam,
	XichLo,
	ThoSo,
}

public enum TurnLight {
	LEFT = -1,
	NONE = 0,
	RIGHT = 1
}
