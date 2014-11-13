﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Ultil {

	private static int layerId = 0;
	private static int objId = 0;

	public static void ResetLayerId () {
		layerId = 0;
	}
	public static int GetNewLayerId () {
		layerId++;
		return layerId;
	}

	public static void ResetObjId (int defaultId = 0) {
		objId = defaultId;
	}
	public static int GetNewObjId () {
		objId++;
		return objId;
	}

	public static string GetString (string key, string defaul, Dictionary<string,string> dict) {
		string value = null;
		dict.TryGetValue (key, out value);

		if (string.IsNullOrEmpty (value)) {
			Debug.LogWarning ("default: " + key + " : " + defaul);
			value = defaul;
			dict[key] = value;
		}

		return value;
	}

	public static Vector2 ParseMap2Real (float tilex, float tiley) {
		float x = tilex * Global.SCALE_TILE * Global.SCALE_SIZE;
		float y = tiley * Global.SCALE_TILE * Global.SCALE_SIZE;

		return new Vector2 (x, y);
	}

	public static Vector2 ParseReal2Map (float x, float y) {
		float tilex = x / Global.SCALE_TILE / Global.SCALE_SIZE;
		float tiley = y / Global.SCALE_TILE / Global.SCALE_SIZE;

		return new Vector2 (tilex, tiley);
	}
	
	public static int CompareTextureName(Texture x, Texture y) {
		int xx = int.Parse (x.name);
		int yy = int.Parse (y.name);
		
		return xx - yy;
	}
	
	public static string GetFolderTile (string tileName) {
		int v = int.Parse (tileName);
		
		if (v <= 100) {
			return "1";
		} else if (v <= 200) {
			return "100";
		} else if (v <= 300) {
			return "200";
		} else if (v <= 400) {
			return "300";
		}
		
		Debug.LogError ("Wrong Tile Name");
		
		return "";
	}

	public static MoveDirection ToMoveDirection (string s) {
		MoveDirection m = (MoveDirection) Enum.Parse (typeof (MoveDirection), s);
		return m;
	}

	public static bool IsOpposite (MoveDirection d1, MoveDirection d2) {
		if (d1 == MoveDirection.LEFT && d2 == MoveDirection.RIGHT) {
			return true;
		}

		if (d1 == MoveDirection.RIGHT && d2 == MoveDirection.LEFT) {
			return true;
		}

		if (d1 == MoveDirection.UP && d2 == MoveDirection.DOWN) {
			return true;
		}

		if (d1 == MoveDirection.DOWN && d2 == MoveDirection.UP) {
			return true;
		}

		return false;
	}

	public static bool IsRoad (int typeid) {
		if (typeid == 1 || typeid == 2 ||typeid == 3 ||typeid == 4||typeid == 7) {
			return true;
		} else {
			return false;
		}
	}

	public static PlayerState GetPreviousDiffState (PlayerState currentState, Queue queue) {
		if (queue == null || currentState == null) {
			return null;
		}

		Debug.LogError (currentState.time);

		object[] arr = queue.ToArray ();
		for (int i = arr.Length-1; i >= 0; --i) {

			PlayerState pl = (PlayerState) arr[i];

			Debug.Log (i + ": " + pl.road.Direction + " : " + pl.time);

			if (currentState.time >= pl.time) {

				if (i > 0) {
					Debug.LogError ("There");
					return (PlayerState) arr[i-1];
				} else {
					Debug.LogError ("This");
					return null;
				}
			}
		}
		Debug.LogError ("Here");
		return null;
	}

	public static MoveDirection LeftOf (MoveDirection dir) {
		switch (dir) {
		case MoveDirection.DOWN:
			return MoveDirection.RIGHT;

		case MoveDirection.LEFT:
			return MoveDirection.DOWN;

		case MoveDirection.RIGHT:
			return MoveDirection.UP;

		case MoveDirection.UP:
			return MoveDirection.LEFT;
		}

		return MoveDirection.NONE;
	}

	public static MoveDirection RightOf (MoveDirection dir) {
		switch (dir) {
		case MoveDirection.DOWN:
			return MoveDirection.LEFT;
			
		case MoveDirection.LEFT:
			return MoveDirection.UP;
			
		case MoveDirection.RIGHT:
			return MoveDirection.DOWN;
			
		case MoveDirection.UP:
			return MoveDirection.RIGHT;
		}
		
		return MoveDirection.NONE;
	}

	public static MoveDirection OppositeOf (MoveDirection dir) {
		switch (dir) {
		case MoveDirection.DOWN:
			return MoveDirection.UP;
			
		case MoveDirection.UP:
			return MoveDirection.DOWN;
			
		case MoveDirection.LEFT:
			return MoveDirection.RIGHT;
			
		case MoveDirection.RIGHT:
			return MoveDirection.LEFT;
		}

		return MoveDirection.NONE;
	}
}


