using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneInput : MonoBehaviour {

	void Start(){
		
	}

	// Update is called once per frame
	void Update () {
		
		Movement movement = GetComponentInChildren<Movement> ();
		if (movement != null) {
			movement.SetThrust (Input.GetAxis ("Thrust"));
			if (Mathf.Approximately (Input.GetAxis ("Thrust"), 0)) {
				GetComponentInChildren<Thruster> ().powered = false;
			} else {
				GetComponentInChildren<Thruster> ().powered = true;
			}
			movement.SetTorque (-Input.GetAxis ("Rotate"));
			ThrusterFlame[] flames = GetComponentsInChildren<ThrusterFlame> ();
			foreach (ThrusterFlame f in flames) {
				f.SetIntensity (Input.GetAxis ("Thrust"));
			}
		}

		Drill drill = GetComponentInChildren<Drill> ();
		if (drill != null) {
			drill.powered = Input.GetButton ("Fire");
		}

		Sonar sonar = GetComponentInChildren<Sonar> ();
		if (sonar != null && Input.GetButtonDown("Ping")) {
			sonar.Ping ();
		}
	}
}
