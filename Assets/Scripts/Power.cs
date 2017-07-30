using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour {

	public float maxPower = 100f;
	public float power = 0f;

	// Use this for initialization
	void Start () {
		if(power <= 0)
			power = maxPower;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddPower(float val){
		power = Mathf.Min (power + val, maxPower);
	}
}
