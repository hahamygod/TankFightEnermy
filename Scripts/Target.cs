using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Target : MonoBehaviour {

	public Camera cameraUI;
	public Camera cameraObj;
	public GameObject other;
	static GameObject target;

	static bool isSetTarget;
	static List<GameObject> listTarget;

	StringBuilder infoBuil;
	// Use this for initialization
	void Start () {
		infoBuil = new StringBuilder ();
		infoBuil.Append ("");
		target = null;
		isSetTarget = false;
		listTarget = new List<GameObject> ();
	}

	public static void SetIsTarget(bool isSetTarget){
		Target.isSetTarget = isSetTarget;
	}

	public static bool GetIsTarget(){
		return isSetTarget;
	}

	public static void SetTarget(GameObject target){
		Target.target = target;
	}

	public static GameObject GetTarget(){
		return target;
	}

	public static List<GameObject> ListTar{
		get{ 
			return listTarget;
		}
		set{ 
			listTarget = value;
		}
	}

	void OnGUI(){
		GUILayout.BeginArea (new Rect(10,10,100,100));
		GUILayout.Space (100);
		GUILayout.Label (infoBuil.ToString());
		GUILayout.EndArea ();
	}
	// Update is called once per frame

	void Update () {
		/*
		Ray ray = cameraUI.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			target = hit.collider.gameObject;
			if (Input.GetMouseButton (0)) {


				if (listTarget.IndexOf (target) < 0) {
					listTarget.Add (target);
				}
			} else if(Input.GetMouseButtonUp(0)){
				
				if (listTarget.IndexOf (target) >= 0) {
					listTarget.Remove (target);
				}
			}

			//Debug.Log (target.ToString ());
			//isSetTarget = true;
		}

		Ray ray1 = cameraObj.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray1, out hit)) {
			target = hit.collider.gameObject;
			if (Input.GetMouseButton (0)) {
				if (listTarget.IndexOf (target) < 0) {
					listTarget.Add (target);
				}
			} else if(Input.GetMouseButtonUp(0)){
				if (listTarget.IndexOf (target) >= 0) {
					listTarget.Remove (target);
				}
			}
			//Debug.Log (target.ToString ());
			//isSetTarget = true;
		}
		*/

		//infoBuil.Remove (0, infoBuil.Length);
	
		for(int i=0;i<Input.touchCount;i++){
			
			if (Input.GetTouch(i).phase == TouchPhase.Began) {
				Ray ray = cameraUI.ScreenPointToRay (Input.GetTouch(i).position);
				Ray ray1 = cameraObj.ScreenPointToRay (Input.GetTouch (i).position);

				RaycastHit hit;

				if (Physics.Raycast (ray, out hit)) {
					GameObject gbo = hit.collider.gameObject;
					if (listTarget.IndexOf (gbo) < 0) {
						listTarget.Add (gbo);
						infoBuil.Append (gbo.ToString () + "  ");
					}
				}else if(Physics.Raycast(ray1,out hit)){
					GameObject gbo = hit.collider.gameObject;

					if(gbo.name.Equals("Terrain")){
						listTarget.Add (other);
						infoBuil.Append (other.ToString () + "  ");
					}else if (listTarget.IndexOf (gbo) < 0) {
						listTarget.Add (gbo);
						infoBuil.Append (gbo.ToString () + "  ");
					}
				}else {
					if (listTarget.IndexOf (other) < 0) {
						listTarget.Add (other);
						infoBuil.Append (other.ToString () + "  ");
					}
				}
			} 
			else if (Input.GetTouch(i).phase == TouchPhase.Ended) {
				infoBuil.Remove (i, i+1);
				listTarget.RemoveAt (i);
			}
		}

	}
}
