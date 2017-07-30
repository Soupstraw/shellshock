using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class BatteryUI : MonoBehaviour {

	Power power;
	Animator anim;

	// Use this for initialization
	void Start () {
		power = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Power> ();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat ("Power", 1f-power.power/power.maxPower);
		GetComponentInChildren<Text> ().text = string.Format("{0:f0}%", power.power/power.maxPower*100);
	}
}
