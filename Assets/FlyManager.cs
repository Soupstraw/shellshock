using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyManager : MonoBehaviour {

	public Vector3 centre;
	public Vector3 heading;

	void Update () {
		centre = Vector3.zero;
		heading = Vector3.zero;

		FlyAI[] flies = FindObjectsOfType<FlyAI> ();
		foreach (FlyAI fly in flies) {
			centre += fly.transform.position;
			heading += fly.transform.up;
		}
		centre /= flies.Length;
	}
}
