using UnityEngine;
using System.Collections;

public class SignHandler : TileHandler {

	public MeshRenderer plane1;
	public MeshRenderer plane2;
	public TextMesh lbText;
	public TypogenicText lbText2;

	void Start () {}
	void Update () {}

	public void SetSign (Texture texture)
	{
		plane1.material.SetTexture ("_MainTex", texture);
	}

	public void SetText (string text, Color color) {
		if (text.Length > 0) {
			lbText2.gameObject.SetActive (true);
			//lbText.text = text;
			//lbText.color = color;

			//
			lbText2.Text = text;
			lbText2.ColorTopLeft = lbText2.ColorTopRight = lbText2.ColorBottomLeft = lbText2.ColorBottomRight = color;
		} else {
			lbText2.gameObject.SetActive (false);
		}
	}
}
