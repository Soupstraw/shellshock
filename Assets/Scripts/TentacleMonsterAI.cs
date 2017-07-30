using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class TentacleMonsterAI : MonoBehaviour {

	Movement movement;
	Transform player;

	public Vector3 target;

	// Use this for initialization
	void Start () {
		target = transform.position;
		movement = GetComponent<Movement> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		float angle; 
		if ((player.position - transform.position).magnitude < 10f) {
			angle = Vector2.SignedAngle (transform.up, player.position - transform.position);
		} else {
			angle = Vector2.SignedAngle (transform.up, target - transform.position);
		}
		movement.SetTorque (angle);
		movement.SetThrust (Mathf.Clamp01(Mathf.Cos(angle)));
	}
}
