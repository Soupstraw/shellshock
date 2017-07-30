using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ThrusterFlame : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	public void SetIntensity(float intensity){
		anim.SetFloat ("Intensity", Mathf.Clamp(intensity, 0f, 0.99f));
		GetComponent<AudioSource> ().volume = intensity;
	}

}
