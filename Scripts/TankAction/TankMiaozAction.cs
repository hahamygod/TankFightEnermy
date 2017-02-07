using UnityEngine;
using System.Collections;
using System;

public class TankMiaozAction : TankAction {
	public GameObject Turret;
	public GameObject Cannon;
	public GameObject GunCam;
	Vector3 vH;
	void OnDrawGizmos() {
		Gizmos.color=Color.red;
		if(Turret)
			
		Gizmos.DrawRay(Turret.transform.position,vH);
	}
	public void Miaozhun(){
		try{
			if(enermyObj!=null){
				//Debug.Log (enermyObj);

				Vector3 dir = enermyObj.transform.FindChild("ZombieA_Pivot").position - myTrans.position;
				float dist = Vector3.Magnitude (new Vector3(dir.x,dir.y,dir.z));

				dist-=Vector3.Magnitude(myTrans.InverseTransformPoint(GunCam.transform.position));
				//Debug.Log (dist);
				Turret.BroadcastMessage ("SetRange", dist, SendMessageOptions.DontRequireReceiver);
				//Vector3 turretDir=Turret.transform.InverseTransformDirection(dir);

				//Debug.Log(dir.ToString());
				float angle = Vector2.Angle (Vector2.up, new Vector2 (dir.x, dir.z))*Mathf.Sign(dir.x);

				angle+=-Turret.transform.parent.localRotation.eulerAngles.y;
				//float angle2=Vector2.Angle(Vector2.up,new Vector2(turretDir.x,turretDir.z))*Mathf.Sign(turretDir.x);
				Turret.BroadcastMessage ("SetTurretAngle", angle, SendMessageOptions.DontRequireReceiver);

				vH=enermyObj.transform.FindChild("ZombieA_Pivot").position-Turret.transform.position;
				//vH=dir;
				vH=Turret.transform.InverseTransformDirection(vH);
				Vector3 vX=Turret.transform.InverseTransformDirection(Turret.transform.forward);
				/*
				vX=new Vector3(vH.x,vX.y,vX.z);

				float sign=Vector2.Dot(new Vector2(vH.z,vH.y),new Vector2(vX.z,vX.y));
				//Gizmos.DrawLine(myTrans.position,myTrans.position+vX);
				//Gizmos.DrawLine(myTrans.position,myTrans.position+vH);
				float cannonAduAngle=Vector3.Angle(vH,vX);
				float canAngle=Cannon.transform.localRotation.eulerAngles.x;

				//Debug.Log(canAngle.ToString());
				canAngle+=cannonAduAngle*(-Mathf.Sign(sign));

				Debug.Log(canAngle.ToString());
				//Debug.Log(cannonAduAngle.ToString());
				Debug.Log(Mathf.Sign(sign));
				*/
				//vH=vX;

				float canAngle=Vector2.Angle(new Vector2(vH.z,vH.y),Vector2.right)*Mathf.Sign(vH.y);
				//Debug.Log(vH.y.ToString());
				//Debug.Log(canAngle.ToString());
				Cannon.BroadcastMessage("SetCannonAngle",canAngle,SendMessageOptions.DontRequireReceiver);
			}
		}catch(Exception e){
			Debug.Log ("Enermy doesn't exist!");
		}
	}
}
