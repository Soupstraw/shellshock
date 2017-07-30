using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Drain : MonoBehaviour {

	CircleCollider2D coll;
	public float drainRate = 10f;

	void Start(){
		coll = GetComponent<CircleCollider2D> ();
	}

	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (coll.OverlapPoint (player.transform.position)) {
			Vector3 diff = player.transform.position - transform.position;
			player.GetComponent<Power> ().power -= Time.deltaTime * drainRate / diff.magnitude;
		}
	}
}
