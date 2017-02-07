using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TankAction : MonoBehaviour {
	protected GameObject enermyObj;
	protected bool isActive;
	protected GameObject myGbo;
	protected Transform myTrans;

	int pressNum;
	public bool IsActive{
		get{ 
			return isActive;
		}
		set{ 
			isActive = value;
		}
	}

	void Start(){
		pressNum = 0;

		myGbo = this.gameObject;
		myTrans = myGbo.transform;
		enermyObj = null;
	}
	// Update is called once per frame
	void Update () {
		try{
			int i;
			bool flag=false;//没有空隙
			for (i = 0; i < Target.ListTar.Count; i++) {
				GameObject gbo = Target.ListTar [i];
				if (gbo.name.Substring(0,7).Equals("ZombieA")) {
					
					SkinnedMeshRenderer render = gbo.GetComponentInChildren<SkinnedMeshRenderer> ();
					if(enermyObj!=gbo&&enermyObj!=null){
						SkinnedMeshRenderer render1 = enermyObj.GetComponentInChildren<SkinnedMeshRenderer> ();
						render1.sharedMaterial.SetFloat ("_Outline", 0.0f);
						enermyObj = gbo;
						render.sharedMaterial.SetFloat ("_Outline", 0.0001f);

						pressNum=1;
						return;
					}else if(enermyObj!=gbo){
						enermyObj = gbo;
						render.sharedMaterial.SetFloat ("_Outline", 0.0001f);

						pressNum=1;
						return;
					}
					flag=true;
				}
			}
			if(!flag&&pressNum<=2){//找不到该对象 未按两次
				pressNum=2;
			}else if(enermyObj&&pressNum==2){//找到该对象 已经按了2次
				SkinnedMeshRenderer render = enermyObj.GetComponentInChildren<SkinnedMeshRenderer> ();
				render.sharedMaterial.SetFloat ("_Outline", 0.0f);
				pressNum=3;
			}else if(!flag&&pressNum==3){//找不到该对象 已经按了2次
				pressNum=1;
				enermyObj=null;
			}
		}catch(Exception e){
			enermyObj = null;
		}
	}

}
