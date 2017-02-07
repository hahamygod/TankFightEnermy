using UnityEngine;
using System.Collections;

public class TrailRend : MonoBehaviour {
	public GameObject startObj;
	public GameObject endObj;
	//public GameObject targetObj;

	Transform startTrans;
	Transform endTrans;

	GameObject gbo;
	Mesh mesh;
	public int VertLen=40;
	Vector3[] vertices;
	int[] triangles;
	Vector2[] uvs;

	int count;

	public Material mat1;

	bool paused=false;

	void OnPauseGame(){
		paused = true;
	}

	void OnResumeGame(){
		paused = false;
	}

	void OnDisable(){
		if (gbo != null)
			gbo.SetActive (false);
	}

	void OnEnable(){
		if (gbo != null) {
			gbo.SetActive (true); 
		}
	}

	// Use this for initialization
	void Start () {
		paused = false;

		startTrans = startObj.transform;
		endTrans = endObj.transform;

		vertices=new Vector3[VertLen];
		uvs=new Vector2[VertLen];

		triangles=new int[(VertLen-2)*6];
		if(mat1==null)
			mat1 = Resources.Load ("Materials/mat1", typeof(Material)) as Material;

		createSwordLight ();
	}

	void createSwordLight(){
		count = 0;
		gbo = new GameObject ();
		//gbo.transform.parent = transform;
		gbo.name = "Sword Light";

		MeshRenderer rend=gbo.AddComponent<MeshRenderer> ();
		rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		rend.receiveShadows = false;

		mesh = gbo.AddComponent<MeshFilter> ().mesh;
		rend.material = mat1;

		vertices [count] = startTrans.position;
		count++;
		vertices [count] = endTrans.position;
		count++;
	}

	void destroySwordLight(){
		if (gbo != null)
			Destroy (gbo);
	}
	
	// Update is called once per frame
	void Update () {
		if (paused)
			return;
		/*
		if (count < VertLen) {
				
			vertices [count] = startTrans.position;
			count++;
			vertices [count] = endTrans.position;
			count++;
		} else if (count == VertLen) {
			int j = 0;
			for (int i = 0; i < VertLen - 2; i++) {
				triangles [j] = i;
				triangles [j + 1] = i + 1;
				triangles [j + 2] = i + 2;
				triangles [j + 3] = i + 2;
				triangles [j + 4] = i + 1;
				triangles [j + 5] = i;
				j += 6;
			}
			mesh.vertices = vertices;
			mesh.triangles = triangles;

			count++;
		} else {
			mesh.Clear ();
			count = 0;
		}
		*/
		if (count == VertLen) {
			int i;
			for (i = 0; i < VertLen - 2; i++) {
				vertices [i] = vertices [i + 2];
			}
			count = VertLen - 2;
		}
		vertices [count] = startTrans.position;
		count++;
		vertices [count] = endTrans.position;
		count++;
		int j = 0;
		for (int i = count-1; i >1; i--) {
			triangles [j] = i;
			triangles [j + 1] = i - 1;
			triangles [j + 2] = i - 2;
			triangles [j + 3] = i - 2;
			triangles [j + 4] = i - 1;
			triangles [j + 5] = i;
			j += 6;
		}
		float len = (float)(count)/2.0f-1.0f;
		for (int i = count - 1; i >= 0; i--) {
			int nj=count-1-i;
			if (nj % 2 == 0)
				uvs [i] = new Vector2 ((float)nj/(2.0f*len),0.0f);//0
			else {
				uvs [i] = new Vector2 ((float)(nj-1)/(2.0f*len),1.0f);//1
			}
		}
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;

	}

	void OnDestroy(){

		Destroy (gbo, 3.0f);
	}
}
