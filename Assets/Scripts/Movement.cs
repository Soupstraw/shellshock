using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConstantForce2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {
	[SerializeField]
	public float thrustMultiplier = 1f;
	[SerializeField]
	public float torqueMultiplier = 1f;

	Rigidbody2D rigid;
	ConstantForce2D force;

	void Start(){
		force = GetComponent<ConstantForce2D> ();
		rigid = GetComponent<Rigidbody2D> ();
	}

	public void SetThrust(float thrust){
		force.relativeForce = new Vector2(0, Mathf.Clamp (thrust, -0.2f, 1) * thrustMultiplier);
	}

	public void SetTorque(float torque){
		rigid.angularVelocity = torque * torqueMultiplier;
	}
}
