using UnityEngine;
using System.Collections;

public class ViewChange : MonoBehaviour {
    public GameObject target;
	public GameObject other;

	public float maxDist=30.0f;
	public float minDist=10.0f;
	public float maxHei=30.0f;
	public Camera camera;

	Transform localTransform;
    Transform targetTransform;


    float minLen=1.0f;
    float maxLen;

    bool flag;
    public float smooth = 20.0f;
	float angY;
	float angZ;

	// Use this for initialization
	void Start () {
		angY = transform.eulerAngles.y ;
		angZ = transform.eulerAngles.z ;

        flag = false;
        localTransform = transform;
        targetTransform = target.transform;
		camera.transform.localPosition = new Vector3(maxDist,0,0);
        camera.transform.LookAt(targetTransform); 
	}

	// Update is called once per frame
	/// <summary>
    /// 
    /// </summary>
    void Update () {
		
		//Debug.Log (targetObj);
		//targetObj==target&&

		if (Target.ListTar.IndexOf(other)>=0) {
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(Target.ListTar.IndexOf(other)).deltaPosition;

			float hx = touchDeltaPosition.x;
			float vy = touchDeltaPosition.y;

			//float hx = Input.GetAxis("Horizontal") ;
			///float vy = Input.GetAxis("Vertical");
		//targetObj==target&&
		/*
		if(Input.GetMouseButton(1)){
			float hx = Input.GetAxis ("Mouse X");
			float vy = Input.GetAxis ("Mouse Y");*/
			
			//Debug.Log(hx.ToString()+" "+vy.ToString());

			//水平控制摄像机围绕物体转动
			//垂直控制摄像机上下、靠近物体和远离物体移动
			if (Mathf.Abs (hx) < 1) {
				Vector3 v = camera.transform.localPosition;
				Vector3 nV = new Vector3 (v.x * Mathf.Cos (hx) + v.z * Mathf.Sin (hx), v.y, -v.x * Mathf.Sin (hx) + v.z * Mathf.Cos (hx));

				v = Vector3.Slerp (v, nV, Time.deltaTime*smooth);
				camera.transform.localPosition = v;
			}
        
			if (vy < 0 && camera.transform.localPosition.y >= 0.1) {//向下
				Vector3 pos = new Vector3 (camera.transform.localPosition.x, 0, camera.transform.localPosition.z);

				float len1 = Vector3.Magnitude (pos);
				if (len1 >= minLen) {
					camera.transform.localPosition = Vector3.Lerp (camera.transform.localPosition, pos, smooth * Time.deltaTime);
				}
				/*
            Vector3 pos = transform.localPosition;
            if (pos.y + vy <= 0)
            {
                pos.y = 0;
            }
            else
            {
                pos.y += vy;
            }
            transform.localPosition = pos;*/
			} else if (vy < 0 && Mathf.Abs (camera.transform.localPosition.y) < 0.1) {//到末尾了向前

				Vector3 pos = new Vector3 (camera.transform.localPosition.x, 0, camera.transform.localPosition.z);
				pos = pos.normalized;
				camera.transform.localPosition = Vector3.Lerp (camera.transform.localPosition, pos * minDist, smooth * Time.deltaTime);
			}
			flag = false;
			float len = Vector3.Magnitude (camera.transform.localPosition);
			if (len > maxDist || Mathf.Abs (len - maxDist) < 0.1)
				flag = true;

			if (vy > 0 && Mathf.Abs (camera.transform.localPosition.y) <= 0.1 && !flag) {//还没到末尾
				Vector3 pos = new Vector3 (camera.transform.localPosition.x, 0, camera.transform.localPosition.z);
				pos = pos.normalized;
				camera.transform.localPosition = Vector3.Lerp (camera.transform.localPosition, pos * maxDist, smooth * Time.deltaTime);
            
			} else if (vy > 0 && flag) {//到末尾了向上
				Vector3 pos = new Vector3 (camera.transform.localPosition.x, maxHei, camera.transform.localPosition.z);
				camera.transform.localPosition = Vector3.Lerp (camera.transform.localPosition, pos, smooth * Time.deltaTime);
				/*
            Vector3 pos = transform.localPosition;
            if (pos.y + vy >= 20)
            {
                pos.y = 20;
            }
            else
            {
                pos.y += vy;
            }
            transform.localPosition = pos;*/
			}
			camera.transform.LookAt (target.transform);//摄像机朝向物体
		}
	}
}
