using UnityEngine;
using System.Collections;

public class TexturePlay : MonoBehaviour {

	public int offsetX;
	public int offsetY;
	public int frame;

	Material mat;
	Texture mainTex;

	public GameObject targetObj;
	Mesh mesh;
	Vector2[] uvs;
	Vector2[] origUvs;
	int count;
	int number;

	// Use this for initialization
	void Start () {
		mesh = targetObj.GetComponent<MeshFilter> ().mesh;

		origUvs=new Vector2[mesh.uv.Length];
		for (int i = 0; i < mesh.uv.Length; i++) {
			origUvs [i] = new Vector2 (mesh.uv[i].x,mesh.uv[i].y);
		}

		count = -1;
		mat = GetComponent<Renderer> ().material;
		mainTex = mat.GetTexture ("_MainTex");

		number = offsetX * offsetY;

		//IEnumerator coroutine = UpdateTex (1.0f/(float)frame);
		StartCoroutine ("UpdateTex");
	}

	IEnumerator UpdateTex(){

		while(true) {
			count = (count + 1) % number;

			int ofx = count % offsetX;
			int ofy = count / offsetX;
			uvs = new Vector2[origUvs.Length];
			for (int i = 0; i < origUvs.Length; i++) {
				uvs [i] = new Vector2 (origUvs [i].x / (float)offsetX, origUvs [i].y / (float)offsetY) + new Vector2 ((float)ofx / (float)offsetX, (float)ofy / (float)offsetY);
			}

			mesh.uv = uvs;

			yield return new WaitForSeconds(1.0f/(float)frame);
		}
	}

}
