using UnityEngine;
using System.Collections;

public class FollowHouse : MonoBehaviour {

	GameObject[] myHouses;
	Transform[] myTrans;
	GameObject[] enermyHouse;
	Transform[] enermyTrans;

	GameObject[] newMyObjs;
	Transform[] newMyTrans;

	GameObject[] newEneObjs;
	Transform[] newEneTrans;

	public Material myMat;
	public Material enermyMat;

	float terrainWidth;
	float terrainHeight;

	// Use this for initialization
	void Start () {
		terrainWidth = 1024f;
		terrainHeight = 1024f;

		myHouses = GameObject.FindGameObjectsWithTag ("MyHouse");
		enermyHouse = GameObject.FindGameObjectsWithTag ("EnermyHouse");

		//Debug.Log (myHouses.Length.ToString ());
		myTrans=new Transform[myHouses.Length];
		enermyTrans=new Transform[enermyHouse.Length];
		for (int i = 0; i < myHouses.Length; i++) {
			myTrans [i] = myHouses [i].transform;
		}

		for (int i = 0; i < enermyHouse.Length; i++) {
			enermyTrans [i] = enermyHouse[i].transform;
		}

		newMyObjs=new GameObject[myHouses.Length];
		newMyTrans=new Transform[myTrans.Length];
		for (int i = 0; i < myHouses.Length; i++) {
			newMyObjs [i] = GameObject.CreatePrimitive (PrimitiveType.Quad);
			newMyObjs [i].name = "myHouse";
			newMyObjs [i].transform.parent = transform;


			MeshRenderer meshRend = newMyObjs [i].GetComponent<MeshRenderer> ();
			meshRend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			meshRend.receiveShadows = false;
			meshRend.sharedMaterial = myMat;

			newMyTrans [i] = newMyObjs [i].transform;
			newMyTrans [i].localScale = Vector3.one * 0.5f;
		}

		newEneObjs=new GameObject[enermyHouse.Length];
		newEneTrans=new Transform[enermyTrans.Length];
		for (int i = 0; i < enermyHouse.Length; i++) {

			newEneObjs [i] = GameObject.CreatePrimitive (PrimitiveType.Quad);
			newEneObjs [i].name = "EnermyHouse";
			newEneObjs [i].transform.parent = transform;

			MeshRenderer meshRend = newEneObjs [i].GetComponent<MeshRenderer> ();
			meshRend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			meshRend.receiveShadows = false;
			meshRend.sharedMaterial = enermyMat;

			newEneTrans [i] = newEneObjs [i].transform;
			newEneTrans [i].localScale = Vector3.one * 0.5f;
		}

		for(int i=0;i<myTrans.Length;i++){
			Transform tarTran = myTrans [i];
			Vector3 pos = new Vector3(tarTran.position.x,0,tarTran.position.z);
			pos = new Vector3 (pos.x / terrainWidth, 0.0f, pos.z / terrainHeight);
			Vector3 forward = tarTran.forward;

			float angle = Vector3.Angle (forward, Vector3.forward);
			//Debug.Log (forward.ToString ());
			newMyTrans[i].localRotation=Quaternion.Euler(90,0,angle);
			newMyTrans[i].localPosition = Vector3.up*0.4f-(pos * 10.0f - new Vector3 (5.0f, 0, 5.0f));//(2y-1)*5
		}

		for (int i = 0; i < enermyTrans.Length; i++) {
			Transform tarTran = enermyTrans [i];
			Vector3 pos = new Vector3(tarTran.position.x,0,tarTran.position.z);
			pos = new Vector3 (pos.x / terrainWidth, 0.0f, pos.z / terrainHeight);
			Vector3 forward = tarTran.forward;

			float angle = Vector3.Angle (forward, Vector3.forward);
			//Debug.Log (forward.ToString ());
			newEneTrans[i].localRotation=Quaternion.Euler(90,0,angle);
			newEneTrans[i].localPosition = Vector3.up*0.4f-(pos * 10.0f - new Vector3 (5.0f, 0, 5.0f));//(2y-1)*5
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
