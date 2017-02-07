using UnityEngine;
using System.Collections;

public class FollowTank : MonoBehaviour {

	public GameObject targetObj;

	float terrainWidth;
	float terrainHeight;
	Transform targetTrans;

	// Use this for initialization
	void Start () {
		targetTrans = targetObj.transform;
		terrainWidth = 1024f;
		terrainHeight = 1024f;
	}

	//对坦克在地形坐标进行变换，变成地图坐标
	// Update is called once per frame
	void Update () {
		Vector3 pos = new Vector3(targetTrans.position.x,0,targetTrans.position.z);
		pos = new Vector3 (pos.x / terrainWidth, 0.0f, pos.z / terrainHeight);
		Vector3 forward = targetTrans.forward;

		float angle = Vector3.Angle (forward, Vector3.forward);
		//Debug.Log (forward.ToString ());
		transform.localRotation=Quaternion.Euler(90,0,angle);
		transform.localPosition = Vector3.up*0.4f-(pos * 10.0f - new Vector3 (5.0f, 0, 5.0f));//(2y-1)*5
	}
}
