using UnityEngine;
using System.Collections;

public class SignHandler : TileHandler {

	public MeshRenderer plane1;
	public MeshRenderer plane2;
	public TextMesh lbText;

	void Start () {}
	void Update () {}

	public void SetSign (Texture texture)
	{
		plane1.material.SetTexture ("_MainTex", texture);
	}

	public void SetText (string text, Color color) {
		if (text.Length > 0) {
			lbText.gameObject.SetActive (true);
			lbText.text = text;
			lbText.color = color;
		} else {
			lbText.gameObject.SetActive (false);
		}
	}
}
