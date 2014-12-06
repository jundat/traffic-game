using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct TileKey {
	public static string ROAD_MIN_VEL = "MIN_VEL";
	public static string ROAD_MAX_VEL = "MAX_VEL";
	//
	public static string ROAD_RE_TRAI = "RE_TRAI";
	public static string ROAD_RE_PHAI = "RE_PHAI";
	public static string ROAD_RE_THANG = "RE_THANG";
	public static string ROAD_QUAY_DAU = "QUAY_DAU";
	public static string RE_QUAY_DAU = "QUAY_DAU";
	//
	public static string ROAD_LOAI_XE = "LOAI_XE";
	public static string ROAD_DI = "DI_";
	public static string ROAD_DUNG = "DUNG_";
	//
	public static string ROAD_LE_TRAI = "LE_TRAI";
	public static string ROAD_LE_PHAI = "LE_PHAI";
	public static string ROAD_LE_TREN = "LE_TREN";
	public static string ROAD_LE_DUOI = "LE_DUOI";
	//
	public static string PCACH_TRAI = "PCACH_TRAI";
	public static string PCACH_PHAI = "PCACH_PHAI";
	public static string PCACH_TREN = "PCACH_TREN";
	public static string PCACH_DUOI = "PCACH_DUOI";

	//------------------------
	//Light
	
	public static string LIGHT_GROUP_ID = "LIGHT_GROUP_ID";
	public static string LIGHT_HUONG = "LIGHT_HUONG";
	public static string LIGHT_LAN_DUONG = "LIGHT_LAN_DUONG";

	//------------------------
	//Sign
	
	public static string SIGN_DIR = "SIGN_DIR";
	public static string SIGN_MAX_TOCDO = "SIGN_MAX_TOCDO";
	public static string SIGN_MIN_TOCDO = "SIGN_MIN_TOCDO";

	//------------------------
	//Auto Car
	public static string AUTOCAR_DIR = "AUTOCAR_DIR";
}

public class TileID {

	public const int ROAD_DOWN = 1;
	public const int ROAD_LEFT = 2;
	public const int ROAD_RIGHT = 3;
	public const int ROAD_UP = 4;
	public const int ROAD_MIN = ROAD_DOWN;
	public const int ROAD_MAX = ROAD_UP;

	public const int ROAD_NONE = 7;

	public const int ROAD_BUS_UP = 8;
	public const int ROAD_BUS_DOWN = 9;
	public const int ROAD_BUS_RIGHT = 10;
	public const int ROAD_BUS_LEFT = 11;
	public const int ROAD_BUS_MIN = ROAD_BUS_UP;
	public const int ROAD_BUS_MAX = ROAD_BUS_LEFT;

	public const int START_POINT = 302;
	public const int END_POINT = 303;
	public const int CHECK_POINT = 304;

	public const int LIGHT = 301;

	public const int AUTO_CAR = 310;
}

[System.Serializable]
public class ModelTile {

	public int objId;
	public int typeId;
	public LayerType layerType;
	public float x;
	public float y;
	public float w;
	public float h;
	public Dictionary<string, string> properties = new Dictionary<string, string> ();

	public ModelTile Copy () {
		ModelTile t = new ModelTile ();

		t.objId = this.objId;
		t.typeId = this.typeId;
		t.layerType = this.layerType;
		t.w = this.x;
		t.y = this.y;
		t.w = this.w;
		t.h = this.h;
		this.properties = new Dictionary<string, string> (this.properties);

		return t;
	}
}
