using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Noise : MonoBehaviour {

	CircleCollider2D col;

	void Start(){
		col = GetComponent<CircleCollider2D> ();
	}

	void Update () {
		if (enabled) {
			Collider2D[] res = new Collider2D[16];
			ContactFilter2D filter = new ContactFilter2D ();
			filter.layerMask = LayerMask.GetMask ("Worm");
			col.OverlapCollider (filter, res);
			foreach (Collider2D col in res) {
				WormAI worm = col.GetComponent<WormAI> ();
				if (worm != null) {
					worm.SetTarget (transform.position);
				}
			}
		}
	}
}
