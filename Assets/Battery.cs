using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Battery : MonoBehaviour {

	public float chargeValue = 0.5f;

	void OnTriggerEnter2D(Collider2D col){
		Power pow = GameObject.FindGameObjectWithTag("Player").GetComponent<Power> ();
		if (col.GetComponent<Collector>() && pow.power > 0) {
			FindObjectOfType<CanvasManager> ().ShowInfo (string.Format("Picked up {0:f0}% battery", chargeValue * 100f));
			pow.AddPower(pow.maxPower * chargeValue);
			Destroy (gameObject);
		}
	}

}
