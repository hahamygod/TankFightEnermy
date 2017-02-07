using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowEnermy : MonoBehaviour {
	float terrainWidth;
	float terrainHeight;

	public Mesh mesh;
	public Material material;
	GameObject[] targetObjs;
	Transform[] targetTrans;

	GameObject[] newObjs;
	Transform[] newTrans;

	// Use this for initialization
	void Start () {
		terrainWidth = 1024f;
		terrainHeight = 1024f;

		targetObjs = GameObject.FindGameObjectsWithTag ("ZombieA");
		targetTrans=new Transform[targetObjs.Length];

		//Debug.Log (targetObjs.Length);
		newObjs=new GameObject[targetObjs.Length];
		newTrans=new Transform[targetObjs.Length];
		for (int i = 0; i < targetObjs.Length; i++) {
			targetTrans [i] = targetObjs [i].transform;

			newObjs [i] = new GameObject ();
			newObjs [i].name = targetObjs [i].name;
			//Debug.Log (targetObjs [i].name.ToString ());
			newObjs [i].transform.parent = transform;
			newObjs [i].AddComponent<MeshFilter> ().mesh = mesh;
			MeshRenderer meshRend = newObjs [i].AddComponent<MeshRenderer> ();
			meshRend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			meshRend.receiveShadows = false;
			meshRend.sharedMaterial = material;

			newTrans [i] = newObjs [i].transform;
			newTrans [i].localScale = new Vector3 (0.5f	, 0.5f, 0.5f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i=0;i<targetTrans.Length;i++){
			Transform tarTran = targetTrans [i];
			Vector3 pos = new Vector3(tarTran.position.x,0,tarTran.position.z);
			pos = new Vector3 (pos.x / terrainWidth, 0.0f, pos.z / terrainHeight);
			Vector3 forward = tarTran.forward;

			float angle = Vector3.Angle (forward, Vector3.forward);
			//Debug.Log (forward.ToString ());
			newTrans[i].localRotation=Quaternion.Euler(90,0,angle);
			newTrans[i].localPosition = Vector3.up*0.4f-(pos * 10.0f - new Vector3 (5.0f, 0, 5.0f));//(2y-1)*5
		}
	}
}
