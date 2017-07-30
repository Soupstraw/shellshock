using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class WormAI : MonoBehaviour {

	Vector3 target;
	Movement movement;

	// Use this for initialization
	void Start () {
		movement = GetComponent<Movement> ();
		target = transform.position;
	}

	// Update is called once per frame
	void Update () {
		float angle = Vector2.SignedAngle (transform.up, target - transform.position);
		movement.SetTorque (angle);
		movement.SetThrust (Mathf.Clamp01(Mathf.Cos(angle)));
	}

	public void SetTarget(Vector3 target){
		this.target = target;
	}
}
