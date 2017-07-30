using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Log : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		Power pow = GameObject.FindGameObjectWithTag("Player").GetComponent<Power> ();
		if (col.GetComponent<Collector>() && pow.power > 0) {
			GameData gd = FindObjectOfType<GameData> ();
			gd.ShowNextLog ();
			Destroy (gameObject);
		}
	}
}
