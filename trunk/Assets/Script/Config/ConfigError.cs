using UnityEngine;
using System.Collections.Generic;
using System;
using FileHelpers;

[FileHelpers.DelimitedRecord("\t")]
[FileHelpers.IgnoreFirst(1)]
public class ConfigErrorItem {
	public int id;
	public string name;
	public string phase1;
	public string phase2;
	public int moneyFrom;
	public int moneyTo;
	public int sub;
}


public class ConfigError : Singleton <ConfigError>
{
	public static bool IsInited = false;
	public Dictionary<int, ConfigErrorItem> dict = new Dictionary<int, ConfigErrorItem> ();

	public void Load (string data) {
		if (IsInited == false) {
			try {
				FileHelperEngine<ConfigErrorItem> engine = new FileHelperEngine<ConfigErrorItem>(); 
				ConfigErrorItem[] res = engine.ReadString (data);
				for (int i = 0; i < res.Length; ++i) {
					dict[res[i].id] = res[i];
				}
				IsInited = true;
			} catch (Exception) {
				IsInited = false;
				LoadBuildData ();
			}
		}
	}

	public void LoadBuildData () {
		if (IsInited == false) {
			try {
				FileHelperEngine<ConfigErrorItem> engine = new FileHelperEngine<ConfigErrorItem>(); 
				UnityEngine.Object obj = Resources.Load (Global.LOCAL_CONFIG_ERROR_FILE);
				string data = obj.ToString ();

				ConfigErrorItem[] res = engine.ReadString (data);
				for (int i = 0; i < res.Length; ++i) {
					dict[res[i].id] = res[i];
				}
				IsInited = true;
			} catch (Exception) {
				IsInited = false;
				Debug.LogError ("Can not load build-in config");
			}
		}
	}

	public void DebugShow () {
		LoadBuildData ();
		foreach (KeyValuePair<int, ConfigErrorItem> p in dict) {
			Debug.Log (p.Key + " : " + p.Value.name);
		}
	}

	public ConfigErrorItem GetError (int errorId) {
		LoadBuildData ();
		ConfigErrorItem item = null;
		dict.TryGetValue (errorId, out item);
		return item;
	}
}
