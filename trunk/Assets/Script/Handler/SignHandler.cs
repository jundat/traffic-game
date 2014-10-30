using UnityEngine;
using System.Collections;

public class SignHandler : MonoBehaviour {

	public MeshRenderer plane1;
	public MeshRenderer plane2;

	public void SetSign (Texture texture)
	{
		plane1.material.SetTexture ("_MainTex", texture);
	}
}
