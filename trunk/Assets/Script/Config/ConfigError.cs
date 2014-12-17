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
	private static bool isInited = false;
	public Dictionary<int, ConfigErrorItem> dict = new Dictionary<int, ConfigErrorItem> ();

	public void Load () {
		if (isInited == false) {
			//--------

			FileHelperEngine<ConfigErrorItem> engine = new FileHelperEngine<ConfigErrorItem>(); 
			UnityEngine.Object obj = Resources.Load (Global.CONFIG_ERROR_FILE);
			string data = obj.ToString ();

			ConfigErrorItem[] res = engine.ReadString (data);
			for (int i = 0; i < res.Length; ++i) {
				dict[res[i].id] = res[i];
			}

			//--------
			isInited = true;
		}
	}

	public void DebugShow () {
		Load ();
		foreach (KeyValuePair<int, ConfigErrorItem> p in dict) {
			Debug.Log (p.Key + " : " + p.Value.name);
		}
	}

	public ConfigErrorItem GetError (int errorId) {
		Load ();
		ConfigErrorItem item = null;
		dict.TryGetValue (errorId, out item);
		return item;
	}
}
