using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour {

	public AudioSource drillAudio;
	public AudioSource grindAudio;

	public Power power;
	public bool powered = false;
	public float powerConsumption = 1f;
	public float drillStrength = 20f;

	void Update(){
		Animator anim = GetComponent<Animator> ();
		if(anim != null){
			anim.SetBool ("Powered", powered);
		}
		if(drillAudio)
			drillAudio.volume = 0f;
		if (powered) {
			if (power != null) {
				if (drillAudio)
					drillAudio.volume = 1f;
				power.power -= Time.deltaTime * powerConsumption;
			}
		} else {
			if(grindAudio)
				grindAudio.volume = 0f;
		}
		Noise noise = GetComponentInChildren<Noise> ();
		if (noise != null) {
			noise.enabled = powered;
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (powered) {
			Mineral mineral = col.GetComponent<Mineral> ();
			if (mineral != null) {
				if(grindAudio)
					grindAudio.volume = 1f;
				mineral.DealDamage (Time.deltaTime * drillStrength);
			}
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if(grindAudio)
			grindAudio.volume = 0f;
	}
}
