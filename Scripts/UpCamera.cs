using UnityEngine;
using System.Collections;

public class UpCamera : MonoBehaviour {
	int myID ;
	bool isCurrentID ;
	Camera thisCamera ;

	public GameObject mainTank;

	Transform thisTransform ;
	Transform mainTankTransform;

	// Use this for initialization
	void Start () {
		thisTransform = transform;
		mainTankTransform = mainTank.transform;
	}

	// Update is called once per frame
	void Update () {
		transform.position = mainTankTransform.position + Vector3.up * 56.6f;
	}

}
