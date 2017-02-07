using UnityEngine;
using System.Collections;

public class BloodChange : MonoBehaviour {
	Mesh mesh;
	Vector3[] vertices;
	int[] triangles= new int[6] {
		2,1,0,3,2,0	//0, 1, 2, 0, 2, 3,
	};
	Vector2[] uvs;

	public Camera camera;
	public float bloodMag=10.0f;
	public float bloodCur=10.0f;
	public float bloodMinus=0.0f;
	public float height=1.0f;
	public float width=1.0f;
	public float fkHeight=1.0f;
	public float fkWidth=3.0f;

	Vector3 dirX;
	Vector3 dirY;
	float curHeight;
	float curWidth;
	float curFkHeight;
	float curFkWidth;

	MeshFilter meshFilter;
	public GameObject targetObj;
	Transform targetTrans;

	public void SetBloodMinus(float minus){
		bloodMinus = minus;
	}
	// Use this for initialization
	void Start () {
		meshFilter = GetComponent<MeshFilter> ();
		mesh = meshFilter.mesh;

		if (targetObj == null)
			targetObj = gameObject;
		targetTrans = targetObj.GetComponent<Transform> ();

		curWidth = width;
		curHeight = height;
		curFkWidth = fkWidth;
		curFkHeight = fkHeight;

		uvs = new Vector2[4];
		uvs [0] = new Vector2 (0, 0);
		uvs [1] = new Vector2 (1, 0);
		uvs [2] = new Vector2 (0, 1);
		uvs [3] = new Vector2 (1, 1);

		vertices = new Vector3[4];
		dirY = new Vector3 (0.0f, fkHeight, 0.0f);


		dirX = new Vector3 (fkWidth*bloodCur/bloodMag, 0.0f, 0.0f);

		vertices [0] = new Vector3 (targetTrans.position.x+width, targetTrans.position.y + height, targetTrans.position.z);
		vertices [1] = new Vector3 (targetTrans.position.x+width, targetTrans.position.y + height, targetTrans.position.z)+dirX;
		vertices [2] = new Vector3 (targetTrans.position.x+width, targetTrans.position.y + height, targetTrans.position.z)+dirX+dirY;
		vertices [3] = new Vector3 (targetTrans.position.x+width, targetTrans.position.y + height, targetTrans.position.z)+dirY;

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;



	}

	void Update(){
		//transform.LookAt (camera.transform);
		Vector3 euler=camera.transform.eulerAngles;

		//Debug.Log (euler.ToString ());
		Vector3 dirO = Vector3.right;
		Quaternion q = Quaternion.Euler (euler);
		Vector3 dirx = q*dirO;
		Vector3 diry = q * Vector3.up;
		//dirx.Normalize ();
		//Debug.Log (dirx.ToString());
		dirX = dirx * fkWidth * bloodCur / bloodMag;
		dirY = diry * fkHeight;
		vertices = new Vector3[4];

		if (bloodMinus > 0) {
			bloodCur -= bloodMinus;
			if (bloodCur < 0)
				bloodCur = 0.0f;
			bloodMinus = 0;
		}

		vertices [0] = new Vector3 (targetTrans.position.x+width, targetTrans.position.y + height, targetTrans.position.z);
		vertices [1] = new Vector3 (targetTrans.position.x+width , targetTrans.position.y + height, targetTrans.position.z)+dirX;
		vertices [2] = new Vector3 (targetTrans.position.x+width , targetTrans.position.y + height , targetTrans.position.z)+dirX+dirY;
		vertices [3] = new Vector3 (targetTrans.position.x+width, targetTrans.position.y + height , targetTrans.position.z)+dirY;

		mesh.vertices = vertices;

		mesh.uv = uvs;

		//GetComponent<Transform> ().position = new Vector3 (targetTrans.position.x+width+fkWidth/2.0f, targetTrans.position.y+height+fkHeight/2.0f,0.0f);

		meshFilter.mesh = mesh;
	}
}
