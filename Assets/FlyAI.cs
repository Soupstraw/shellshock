using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class FlyAI : MonoBehaviour {

	Movement movement;
	FlyManager flyMan;

	public CircleCollider2D vicinity;

	// Use this for initialization
	void Start () {
		movement = GetComponent<Movement> ();
		flyMan = FindObjectOfType<FlyManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 centre = flyMan.centre - transform.position;
		Collider2D[] res = new Collider2D[16];
		ContactFilter2D filter = new ContactFilter2D ();
		filter.layerMask = LayerMask.GetMask ("Fly");
		vicinity.OverlapCollider (filter, res);
		Vector3 evasion = Vector3.zero;
		foreach (Collider2D col in res) {
			if (col != null && col.GetComponent<FlyAI> () != null) {
				Vector3 e = transform.position - col.transform.position;
				evasion += e / e.magnitude;
			}
		}
		
		float angle = Vector2.SignedAngle (transform.up, centre.normalized + flyMan.heading.normalized + evasion.normalized);
		movement.SetTorque (angle);
		movement.SetThrust (Mathf.Clamp01(Mathf.Cos(angle)));
	}
}
