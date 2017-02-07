using UnityEngine;
using System.Collections;

public class LetTankGo : MonoBehaviour {
	public GameObject targetObj;
	public GameObject tankObj;
	public Camera camera;
	public GameObject background;

	Vector3 originWorld;
	float width;
	float height;
	Vector3 previousMousePos;
	// Use this for initialization
	void Start () {

		originWorld = targetObj.transform.position;
		width=Screen.width;
		height = Screen.height;
	}

	void SetTarget(GameObject gbo){
		targetObj = gbo;
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log(Input.mousePosition.ToString());
		//GameObject target = Target.GetTarget();

		//Debug.Log (target);
		if (Target.ListTar.IndexOf(this.gameObject)>=0) {
			Vector3 point = originWorld;
			//if (Input.GetMouseButton (0)) 
			//{
			//	Ray ray = camera.ScreenPointToRay (Input.mousePosition);
			if (Input.GetTouch (Target.ListTar.IndexOf (this.gameObject)).phase != TouchPhase.Ended) 
			{
				Ray ray = camera.ScreenPointToRay (Input.GetTouch (Target.ListTar.IndexOf (this.gameObject)).position);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {

					//Debug.Log (background);
					if (hit.collider.gameObject == background) {
						point = hit.point;
					} 
				}
			}
			//}
			//Debug.Log (targetObj.ToString ());
			//float vertical = Input.GetAxis ("Mouse Y");
			//float horizontal = Input.GetAxis ("Mouse X");

			Vector3 dir = point-originWorld;

			//Debug.Log (dir.ToString ());
			float len = Vector3.Magnitude( dir);

			//Debug.Log (len.ToString ());
			if (len <= 250.0f) {//500/2=150
				targetObj.transform.localPosition = 417f* dir;//width/0.6=417f
			}

			if (tankObj) {
				tankObj.BroadcastMessage ("SetVerticalAndHorizontal", 417.0f*dir, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
