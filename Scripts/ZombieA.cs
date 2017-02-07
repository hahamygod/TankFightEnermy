using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieA : MonoBehaviour {

	public GameObject targetTank;
	public GameObject targetBlood;
	public GameObject targetMoney;
	Transform targetTrans;

	//public List<Animator> animatorList;
	Animator animator;
	AnimatorClipInfo[] animatorClipInfo;
	AnimatorStateInfo animatorStateInfo;
	public List<GameObject> effectObjList;
	public List<MonoBehaviour> effectScriptList;

	public int health=10;
	public float minRange=25.0f;//100
	public float maxRange=100.0f;
	public float walkSpeed=0.01f;
	public float runSpeed = 0.5f;
	public float fightRange=0.1f;
	int hurtPoint;

	public GameObject bloodGbo;
	bool isHit;//是否被攻击
	bool wasHit;//是否刚被攻击
	bool isAttacking;//是否正在攻击
	Vector3 dir;//direction

	Vector3 offset;//偏移位置 对动画
	string[] stateStr=new string[]{
		"isStand",
		"isWalk",
		"isRun",
		"isKnockback",
		"isDamage",
		"isDeath",
		"isAttack",
		"isSkill"
	};
	int currentState;
	//0 Stand
	//1 Walk
	//2 Run
	//3 Knockback
	//4 Damage
	//5 Death
	//6 Attack
	//7 Skill

	void Start(){
		isAttacking = false;
		wasHit = false;
		hurtPoint = 3;
		offset = Vector3.zero;
		isHit = false;
		targetTrans = targetTank.transform;

		animator = GetComponent<Animator> ();
		animatorClipInfo = animator.GetCurrentAnimatorClipInfo (0);
		animatorStateInfo = animator.GetCurrentAnimatorStateInfo (0);
		currentState = 0;
	}
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < effectObjList.Count; i++) {
			GameObject effectGbo = effectObjList [i];

			ParticleSystem ps = effectGbo.GetComponent<ParticleSystem> ();

			if (animatorStateInfo.IsName ("Run") ) {
				//pa.ClearParticles ();
				ps.Play();
			} else if(animatorStateInfo.IsName("Stand")){
				
			}
		}

		for (int i = 0; i < effectScriptList.Count; i++) {
			MonoBehaviour script = effectScriptList [i];


		}

		DoAction ();
	}

	void SetHit(){
		this.isHit = true;
	}

	void DoAction(){

		/*
		KeyCode key;
		if (Input.GetKeyDown (KeyCode.A)) {
			Walk ();
		} else if (Input.GetKeyDown (KeyCode.S)) {
			Run ();
		} else if (Input.GetKeyDown (KeyCode.D)) {
			Skill ();
		} else if (Input.GetKeyDown (KeyCode.F)) {
			Attack ();
		} else if (Input.GetKeyDown (KeyCode.G)) {
			Damage ();
		} else if (Input.GetKeyDown (KeyCode.H)) {
			Death ();
		} else if (Input.GetKeyDown (KeyCode.J)) {
			Knockback ();
		} else if (Input.GetKeyDown (KeyCode.K)) {
			Stand ();
		}*/

		if (targetTank != null) {
			if (health <= 0) {
				Destroy (this);
			}
			ActionDeal ();
			//Debug.Log (animatorClipInfo.Length.ToString ());

		}
		//Debug.Log (currentState.ToString ());
	}

	float Dist(Transform targetTrans){
		return Vector3.Distance (targetTrans.position, transform.position);
	}

	void ActionDeal(){
		float dist = Dist (targetTrans);
		if (isHit) {
			Knockback ();

			isHit = false;

			wasHit = true;
		} else if (wasHit) {
			Stand ();
		} else if (dist < maxRange && dist > minRange + walkSpeed + fightRange) {
			if (currentState != 1&&currentState!=0) {//not Walk not Stand
				Stand ();
			} else {
				Walk ();
			}
		} else if (dist <= minRange + walkSpeed + fightRange) {
			if (currentState != 6 && currentState != 0) {//not attack not Stand
				Stand ();
			} else {
				Attack ();
			}
		} else if (dist > maxRange) {
			Stand ();
		}
	}

	void LookAtTargetTrans(Transform targetTrans){
		if (targetTrans != null) {
			dir = targetTrans.position - transform.position;
			dir = new Vector3 (dir.x, 0.0f, dir.z);
			dir.Normalize ();
			transform.forward = dir;
		}
	}

	void Stand(){
		animatorStateInfo = animator.GetCurrentAnimatorStateInfo (0);

		if (wasHit&&animatorStateInfo.IsName("Knockback")) {//|| animatorStateInfo.normalizedTime <1.0f


			//Debug.Log(animatorStateInfo.normalizedTime.ToString());
			if (animatorStateInfo.normalizedTime >= 0.90f) {
				offset = transform.position - targetTrans.position;
				offset = new Vector3 (offset.x, 0.0f, offset.z);
				transform.position += offset.normalized * 10.5f;

				wasHit = false;
				//offset = Vector3.zero;
				//Debug.Log ("Knockback");
			}

		}else if (currentState != 0 && currentState != 5) {
			//animator.SetBool (stateStr [currentState], false);
			currentState = 0;
			for (int i = 1; i <= 7; i++) {
				animator.SetBool (stateStr [i], false);
			}
			animator.SetBool (stateStr [0], true);
		}
		//Debug.Log (stateStr [currentState]);
	}

	void Walk(){
		
		if (currentState == 0) {//Stand
			animator.SetBool (stateStr [0], false);
			animator.SetBool (stateStr [1], true);
			currentState = 1;
		} else if (currentState == 1) {//Walk
			LookAtTargetTrans (targetTrans);
			//float dist = Dist (targetTrans);
			//if (dist > minRange + walkSpeed+fightRange && dist < maxRange) {
				//animator.SetFloat ("speed", walkSpeed);
			transform.position = transform.position + dir * walkSpeed;
		}
		//Debug.Log (stateStr [currentState]);
	}

	void Run(){
		if (currentState == 1) {//walk
			animator.SetBool (stateStr [2], true);
			currentState = 2;
		}
		//Debug.Log (stateStr [currentState]);
	}

	void Skill(){
		if (currentState == 0) {//idle
			animator.SetBool(stateStr[0],false);
			animator.SetBool (stateStr [7], true);
			currentState = 7;
		}
		//Debug.Log (stateStr [currentState]);
	}

	void Attack(){
		if (currentState <= 2) {//idle walk run
			animator.SetBool (stateStr [0], false);
			animator.SetBool (stateStr [6], true);
			currentState = 6;
		} else if (currentState == 6) {
			LookAtTargetTrans (targetTrans);

			animatorStateInfo = animator.GetCurrentAnimatorStateInfo (0);

			float minus = animatorStateInfo.normalizedTime - Mathf.FloorToInt (animatorStateInfo.normalizedTime)*1.0f;
			//Debug.Log (minus.ToString());
			if (isAttacking && minus > 0.5f) {
				targetBlood.GetComponent<UISlider> ().value -= 0.4f;
				isAttacking = false;
			} else if (!isAttacking&&minus<0.2f) {
				isAttacking = true;
			}
		}

		//Debug.Log (stateStr [currentState]);
	}

	void Damage(){
		if (currentState <= 2 || currentState == 6) {//idle walk run attack
			animator.SetBool(stateStr[4],true);
			currentState = 4;
		}
		//Debug.Log (stateStr [currentState]);
	}

	void Death(){
		if (currentState <= 4||currentState==6) {//damage konckback attack
			animator.SetBool(stateStr[5],true);
			currentState = 5;
			//Destroy (gameObject);
		}

		//Debug.Log (stateStr [currentState]);
	}

	void Knockback(){
		if (currentState <= 2 || currentState == 6) {//idle walk run attack
			animator.SetBool(stateStr[0],false);
			animator.SetBool(stateStr[3],true);
			currentState = 3;

			health -= hurtPoint;
			bloodGbo.BroadcastMessage ("SetBloodMinus", (float)hurtPoint, SendMessageOptions.DontRequireReceiver);
		}
		//Debug.Log (stateStr [currentState]);
	}

	void OnDestroy(){
		Death ();
		if (targetMoney != null) {
			int money = int.Parse (targetMoney.GetComponent<UILabel> ().text);
			targetMoney.GetComponent<UILabel> ().text = money + 10 + "";
		}
		Destroy (gameObject,6.0f);
		Destroy (bloodGbo);
	}
}
