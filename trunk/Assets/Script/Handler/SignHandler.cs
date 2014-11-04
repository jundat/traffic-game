using UnityEngine;
using System.Collections;

public class SignHandler : TileHandler {

	public MeshRenderer plane1;
	public MeshRenderer plane2;

	void Start () {}
	void Update () {}

	public void SetSign (Texture texture)
	{
		plane1.material.SetTexture ("_MainTex", texture);
	}
}
