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
}


public class ConfigError : Singleton <ConfigError>
{
	Dictionary<int, ConfigErrorItem> dict = new Dictionary<int, ConfigErrorItem> ();

	public void Load (string filepath) {
		FileHelperEngine<ConfigErrorItem> engine = new FileHelperEngine<ConfigErrorItem>(); 

		UnityEngine.Object obj = Resources.Load (filepath);
		string data = obj.ToString ();

		ConfigErrorItem[] res = engine.ReadString (data);
		for (int i = 0; i < res.Length; ++i) {
			dict[res[i].id] = res[i];
		}
	}

	public void DebugShow () {
		foreach (KeyValuePair<int, ConfigErrorItem> p in dict) {
			Debug.Log (p.Value.name);
		}
	}
}
