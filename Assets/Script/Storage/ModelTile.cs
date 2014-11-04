﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct TileKey {
	public static string COI = "COI";
	public static string CHIEU = "CHIEU";
	public static string MIN_VEL = "MIN_VEL";
	public static string MAX_VEL = "MAX_VEL";
	public static string RE_TRAI = "RE_TRAI";
	public static string RE_PHAI = "RE_PHAI";
	public static string RE_THANG = "RE_THANG";
	public static string LOAI_XE = "LOAI_XE";
	public static string DI = "DI_";
	public static string DUNG = "DUNG_";
	
	//Light
	
	public static string LIGHT_GROUP_ID = "LIGHT_GROUP_ID";
	public static string LIGHT_HUONG = "LIGHT_HUONG";
	public static string LIGHT_LAN_DUONG = "LIGHT_LAN_DUONG";
	
	//Sign
	
	public static string SIGN_DIR = "SIGN_DIR";
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
}
