using System;
using UnityEngine;

public class ControlParticle:MonoBehaviour
{

	GameObject gbo;

	ParticleSystem ps;

	public float lagTime=0.0f;
	float curTime;
	void Start(){

		ps = GetComponent<ParticleSystem> ();

		var em = ps.emission;
		em.enabled = true;

		em.type = ParticleSystemEmissionType.Time;

		em.SetBursts(
			new ParticleSystem.Burst[]{
				new ParticleSystem.Burst(0.1f, 200)
			});
	}


	void Update(){
		if (Time.time - curTime > lagTime) {
			curTime = Time.time;

			if (!ps.IsAlive ()) {
				ps.Play ();

			}

		}
	}
}