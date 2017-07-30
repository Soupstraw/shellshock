using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour {

	public Power power;
	public float pingCost;

	public void Ping(){
		if (power.power <= 0)
			return;
		power.power -= pingCost;
		FindObjectOfType<SonarUI> ().Ping ();
		GetComponent<AudioSource> ().Play ();
		TentacleMonsterAI[] monsters = FindObjectsOfType<TentacleMonsterAI> ();
		foreach (TentacleMonsterAI ai in monsters) {
			ai.target = transform.position;
		}
	}
}
