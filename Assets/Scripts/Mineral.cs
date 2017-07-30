using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour {

	public GameObject cracksPrefab;
	[SerializeField]
	private float maxIntegrity = 100f;
	[SerializeField]
	private float integrity = 100f;
	public GameObject[] drops;

	void Start(){
		integrity = maxIntegrity;
	}

	public void DealDamage(float damage){
		integrity -= damage;

		if (cracksPrefab != null) {
			Animator anim = GetComponentInChildren<Animator> ();
			if (anim == null) {
				GameObject cracks = Instantiate (cracksPrefab, transform);
				cracks.transform.position = transform.position + new Vector3 (0, 0, -1);
				anim = cracks.GetComponent<Animator> ();
			}
			anim.SetFloat ("Damage", 1f - integrity / maxIntegrity);
		}

		if (integrity <= 0) {
			foreach (GameObject obj in drops) {
				Instantiate (obj, transform.position, Quaternion.Euler (new Vector3 (0, 0, Random.value * 360f)));
			}
			Destroy (gameObject);
		}
	}
}
