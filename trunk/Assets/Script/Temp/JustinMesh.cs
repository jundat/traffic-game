using UnityEngine;
using System.Collections;

public class JustinMesh : MonoBehaviour {

	void Start() {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector2[] uvs = mesh.uv;
		int i = 0;
		while (i < uvs.Length) {
			uvs[i] = new Vector2 (uvs[i].x * 0.5f, uvs[i].y * 0.5f);
			i++;
		}
		mesh.uv = uvs;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
