using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class SelectMap : MonoBehaviour {

	private MapItemHandler currentMapHandler;

	public UILabel lbMapInfo;
	public UIScrollView scrollViewInfo;
	public UIScrollBar scrollBarInfo;
	public GameObject grid;
	public GameObject prefabMapItem;
	public UIButton btnPlay;

	public UIScrollView scrollView;
	public UIScrollBar scrollBar;
	public GameObject objWait;
	public LoadingHandler loadingHandler;
	public UILabel lbError;

	List<MapItemHandler> listMaps = new List<MapItemHandler> ();

	void Start () {
		btnPlay.SetState (UIButtonColor.State.Disabled, true);
		btnPlay.isEnabled = false;
		objWait.SetActive (true);

		if (UserManager.Instance.user != null) {
			StartCoroutine( LoadListMap (UserManager.Instance.user.username));
		} else {
			Debug.Log ("Null user");
		}
	}

	void Update () {}
	
	public IEnumerator LoadListMap (string username) {
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		
		WWW w = new WWW(Global.URL_SERVER + Global.URL_GETMAP, form);
		
		yield return w;
		
		if (!string.IsNullOrEmpty(w.error))
		{
			objWait.SetActive (false);
			lbError.text = "Can not connect to server!";
		}
		else
		{
			objWait.SetActive (false);
			lbError.text = "";
			
			ModelMapResponse res = JsonReader.Deserialize <ModelMapResponse> (w.text) as ModelMapResponse;
			if (res != null) {
				if (res.is_success) {
					SetupUI (res);
				} else {
					lbError.text = res.message;
				}
			} else {
				lbError.text = "Something wrong with API";
			}
		}
	}

	public void SetupUI (ModelMapResponse res) {

		listMaps.Clear ();
		foreach (Transform t in grid.transform) {
			Destroy (t.gameObject);
		}

		scrollView.ResetPosition ();
		scrollBar.value = 0;

		for (int i = 0; i < res.maps.Count; ++i) {
			GameObject ins = NGUITools.AddChild (grid.gameObject, prefabMapItem);
			MapItemHandler item = ins.GetComponent<MapItemHandler> ();
			item.Init (res.maps[i]);
			listMaps.Add (item);

			ins.transform.localPosition = new Vector3 (i * 290, 0, 0);
			ins.transform.localScale = Vector3.one;
		}

		grid.GetComponent<UICenterOnChild>().enabled = true;

		btnPlay.SetState (UIButtonColor.State.Normal, true);
		btnPlay.isEnabled = true;
	}

	public IEnumerator LoadSelectedMap (string url) {
		WWWForm form = new WWWForm();
		string s = url;

		WWW w = new WWW(s, form);
		
		yield return w;
		
		if (!string.IsNullOrEmpty(w.error))
		{
			loadingHandler.gameObject.SetActive (false);
			lbError.text = w.error;
		}
		else
		{
			lbError.text = "";
			
			if (! string.IsNullOrEmpty (w.text)) {
				
				MapManager.Instance.LoadJSON (w.text);
				
				StartCoroutine (LoadMainGameScene ());
			} else {
				loadingHandler.gameObject.SetActive (false);
				lbError.text = "Something wrong!";
			}
		}
	}

	public IEnumerator LoadMainGameScene () {
		AsyncOperation asyn = Application.LoadLevelAsync ("Main");
		while (asyn.isDone == false) {
			loadingHandler.SetValue (asyn.progress);
			yield return null;
		}
		
		loadingHandler.gameObject.SetActive (false);
	}


	public void OnButtonPlayClicked () {
		if (currentMapHandler != null && currentMapHandler.map != null) {
			MapManager.Instance.mapNetwork = currentMapHandler.map.Copy ();

			loadingHandler.gameObject.SetActive (true);
			StartCoroutine (LoadSelectedMap (currentMapHandler.map.url));

		} else {
			Debug.LogError ("Plz choose 1 map");
		}
	}

	public void OnButtonBackClicked () {
		Application.LoadLevel ("Menu");
	}
	
	public void OnCenterObjectChange () {
		UICenterOnChild c = grid.gameObject.GetComponent<UICenterOnChild> ();
		if (c != null) {
			MapItemHandler item = c.centeredObject.GetComponent<MapItemHandler> ();
			if (item != null && item.map != null) {

				foreach (MapItemHandler it in listMaps) {
					it.Selected = false;
				}
				
				currentMapHandler = item;
				item.Selected = true;
				lbMapInfo.text = item.map.info;
				scrollViewInfo.ResetPosition ();
				//scrollBarInfo.value = 0;
			}
		}
	}
}
