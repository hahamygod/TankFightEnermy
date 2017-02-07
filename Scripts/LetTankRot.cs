using UnityEngine;
using System.Collections;

public class LetTankRot : MonoBehaviour {
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

		//point = originWorld;
		width=Screen.width;
		height = Screen.height;
	}

	void SetTarget(GameObject gbo){
		targetObj = gbo;
	}
	//Vector3 point;
	// Update is called once per frame
	void Update () {
		//Debug.Log(Input.mousePosition.ToString());
		//GameObject target = Target.GetTarget();

		//Debug.Log (target);
		if (Target.ListTar.IndexOf(this.gameObject)>=0) {
			Vector3 point = originWorld;

			//if(Input.GetMouseButton(0)){
			if (Input.GetTouch (Target.ListTar.IndexOf (this.gameObject)).phase != TouchPhase.Ended) {

				//Ray ray = camera.ScreenPointToRay (Input.mousePosition);
				Ray ray = camera.ScreenPointToRay (Input.GetTouch (Target.ListTar.IndexOf (this.gameObject)).position);

				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.gameObject == background) {
						point = hit.point;
					}
				}
			}
			//Debug.Log (targetObj.ToString ());
			//float vertical = Input.GetAxis ("Mouse Y");
			//float horizontal = Input.GetAxis ("Mouse X");

			Vector3 dir = point-originWorld;

			if (dir == Vector3.zero)
				dir += Vector3.up;
			//Debug.Log (dir.ToString ());
			float len = Vector3.Magnitude( dir);
			float angle = Vector2.Angle (Vector2.up, new Vector2 (dir.x, dir.y))*Mathf.Sign(dir.x);

			//Debug.Log (angle.ToString ());
			if (len <= 200.0f) {//width
				targetObj.transform.localRotation = Quaternion.Euler (0, 0, -angle);
			}

			//Debug.Log (-angle + "");
			if (tankObj) {
				tankObj.BroadcastMessage ("SetTurretAngle", angle, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
