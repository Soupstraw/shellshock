using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Ore : MonoBehaviour {

	public int iron = 0;
	public int fuel = 0;

	void OnTriggerEnter2D(Collider2D col){
		Power pow = GameObject.FindGameObjectWithTag("Player").GetComponent<Power> ();
		if (col.GetComponent<Collector>() && pow.power > 0) {
			GameData gd = FindObjectOfType<GameData> ();
			gd.iron += iron;
			gd.fuel += fuel;
			if (iron > 0) {
				FindObjectOfType<CanvasManager> ().ShowInfo ("Picked up " + iron + " iron.");
			} else if (fuel > 0) {
				FindObjectOfType<CanvasManager> ().ShowInfo ("Picked up " + fuel + " exotic ore.");
			}
			Destroy (gameObject);
		}
	}
}
