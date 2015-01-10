using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveOnlyOne : MonoBehaviour {

	public List<GameObject> listItems = new List<GameObject> ();

	void Start () {
		int count = listItems.Count;
		if (count > 0) {
			int rd = Ultil.random.Next (0, count);
			for (int i = 0; i < count; ++i) {
				listItems[i].SetActive (false);
			}
			listItems[rd].SetActive (true);
		}
	}
}
