using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {
	public Power power;

	public float thrustForce;
	public float torqueForce;
	public float fuelCost;

	public bool powered = false;

	void Start(){
		
	}

	void Update(){
		if (powered) {
			power.power -= fuelCost * Time.deltaTime;
		}
	}
}
