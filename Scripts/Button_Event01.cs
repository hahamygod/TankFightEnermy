using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Button_Event01 : MonoBehaviour {

	public void OnPlayButtonClick(){
		print ("Clicke Play Button!");
	}

	public void OnExitButtonClick(){
		Application.Quit ();
		//print ("Click Exit Button!");
	}

	public void OnLeftButtonClick_Panel_SelTank(){
		print ("Click OnLeftButtonClick_Panel_SelTank()");
	}

	public void OnRightButtonClick_Panel_SelTank(){
		print ("Click OnRightButtonClick_Panel_SelTank()");
	}

	public void OnStartGameButton_Panel_Main(){
		SceneManager.LoadScene ("Base_01_Mobile");
	}
	/*
	void Update(){
		/*
		if (targetObj != null) {
			if (Time.time - curTime > lagTime) {
				index = (index + 1) % 4 + 5;
				curTime = Time.time;
				sprite1 = targetObj.GetComponent<UISprite> ();
				sprite1.spriteName = "light0" + index;
				sprite1.GetAtlasSprite ();
			}
		}*/
	/*
	}
	*/

}
