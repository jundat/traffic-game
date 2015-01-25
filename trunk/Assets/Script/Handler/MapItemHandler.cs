using System;
using UnityEngine;
using System.Collections;

public class MapItemHandler : MonoBehaviour {

	public ModelMapNetwork map;

	public UILabel lbName;
	public UILabel lbTime;
	public UILabel lbLevel;
	public UITexture ttThumnail;
	public UISprite sprSelected;

	void Start () {}
	
	void Update () {}

	public void Init (ModelMapNetwork m) {
		this.map = m;

		lbName.text = map.name;
		lbLevel.text = "Level " + map.level;

		TimeSpan span = new TimeSpan (0, 0, (int)(60.0f * map.time));
		lbTime.text = string.Format("{0:0} phút {1:00} giây", span.Minutes, span.Seconds);

		if (map.thumnail != null) {
			if (map.thumnail.Length > 0) {
				StartCoroutine (LoadThumnail ());
			}
		}
	}

	public IEnumerator LoadThumnail () {
		WWWForm form = new WWWForm();
		form.AddField ("fake", 1);
		string s = map.thumnail;

		WWW w = new WWW(s, form);
		
		yield return w;
		
		if (string.IsNullOrEmpty(w.error)) {
			if (w.texture != null) {
				ttThumnail.mainTexture = w.texture;
			} else {
				Debug.LogError ("Null thumnail");
			}
		} else {
			Debug.LogError (w.error);
		}
	}

	public bool Selected {
		get {
			return sprSelected.enabled;
		}
		set {
			sprSelected.gameObject.SetActive (value);
		}
	}
}
