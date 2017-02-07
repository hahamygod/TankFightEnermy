using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_Event02 : MonoBehaviour {

	public GameObject Panel_Pause;
	public GameObject Panel_UI;
	public GameObject Panel_Map;

	bool isTankBtn;
	public GameObject tankButton;
	public GameObject rotateTankBtn;
	public GameObject tankFire;

	public GameObject GunCamera;
	public GameObject tank;
	public GameObject cannonVal;
	public GameObject cannonTank;

	float curTime;
	bool isGunCamOn;
	int index;

	void Start(){
		index = 1;
		curTime = 0.0f;
		isGunCamOn = false;
		isTankBtn = false;
	}
	public void OnButtonPause(){
		if (Panel_Pause != null)
			Panel_Pause.SetActive (true);
		if (Panel_UI != null)
			Panel_UI.SetActive (false);
		print ("OnButtonPause()");
		//System.GC.Collect ();

		Object[] objects = FindObjectsOfType (typeof(GameObject));

		foreach (GameObject go in objects) {
			go.SendMessage ("OnPauseGame", SendMessageOptions.DontRequireReceiver);
		}
	}

	public void OnButtonResume(){
		if (Panel_Pause != null)
			Panel_Pause.SetActive (false);
		if (Panel_UI != null)
			Panel_UI.SetActive (true);
		Object[] objects = FindObjectsOfType (typeof(GameObject));

		foreach (GameObject go in objects) {
			go.SendMessage ("OnResumeGame", SendMessageOptions.DontRequireReceiver);
		}
	}

	public void OnButtonOpenMap(){
		if (Panel_Map != null)
			Panel_Map.SetActive (true);
		if (Panel_UI != null)
			Panel_UI.SetActive (false);
	}

	public void OnButtonCloseMap(){
		if (Panel_Map != null)
			Panel_Map.SetActive (false);
		if (Panel_UI != null)
			Panel_UI.SetActive (true);
	}

	public void OnButtonExit(){
		SceneManager.LoadScene ("UI_Scene01");
		//print ("OnButtonExit()");
	}

	public void OnButtonControlTank(){
		//print ("OnButtonControlTank()");
		Target.SetTarget (tankButton);
		//Target.SetIsTarget (true);
	}

	public void OnButtonTankFire(){
		tank.BroadcastMessage ( "Fire" , SendMessageOptions.DontRequireReceiver ) ;
	}

	public void OnButtonTankRotate(){
		Target.SetTarget (rotateTankBtn);
		//Target.SetIsTarget (true);
	}

	public void OnButtonGunCameraOnAndOff(){
		//print ("OnButtonGunCam");
		if (!isGunCamOn) {
			GunCamera.BroadcastMessage ("GunCam_On", SendMessageOptions.DontRequireReceiver);
			isGunCamOn = true;
		} else {
			GunCamera.BroadcastMessage ("GunCam_Off", SendMessageOptions.DontRequireReceiver);
			isGunCamOn = false;
		}
	}

	public void OnButtonTankCannon(){
		if (cannonVal) {
			float angle =cannonVal.GetComponent<UISlider> ().value*45.0f-22.5f;
			cannonTank.BroadcastMessage ("SetCannonAngle", angle, SendMessageOptions.DontRequireReceiver);
		}
	}

	void Update(){
		if (tankFire != null) {
			if (Time.time - curTime > 0.1f) {
				index = (index + 1) % 5+1;
				curTime = Time.time;
				UISprite sprite1 = tankFire.GetComponent<UISprite> ();
				sprite1.spriteName = "redlig0" + index;
				sprite1.GetAtlasSprite ();
			}
		}
	}

	public void OnButtonTankViewChange(){
		Target.SetTarget (tank);
	}

	public void OnButtonTankMiaozhun(){
		tank.GetComponent<TankMiaozAction> ().Miaozhun ();
	}
}
