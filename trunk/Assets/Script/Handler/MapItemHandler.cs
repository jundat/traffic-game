using UnityEngine;
using System.Collections;

public class MapItemHandler : MonoBehaviour {

	public ModelMapNetwork map;

	public UILabel lbName;
	public UILabel lbTime;
	public UITexture ttThumnail;
	public UISprite sprSelected;

	void Start () {}
	
	void Update () {}

	public void Init (ModelMapNetwork m) {
		this.map = m;

		lbName.text = map.name;
		if (map.time >= 1) {
			lbTime.text = map.time + " minutes";
		} else {
			lbTime.text = (int)(60 * map.time) + " seconds";
		}

		if (map.thumnail != null) {
			if (map.thumnail.Length > 0) {
				StartCoroutine (LoadThumnail ());
			}
		}
	}

	public IEnumerator LoadThumnail () {
		WWWForm form = new WWWForm();
		string s = map.thumnail;

		WWW w = new WWW(s, form);
		
		yield return w;
		
		if (string.IsNullOrEmpty(w.error)) {
			if (w.texture != null) {
				ttThumnail.mainTexture = w.texture;
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
