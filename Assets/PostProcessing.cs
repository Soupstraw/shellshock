using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PostProcessing : MonoBehaviour {

	public Material material;

	private GameObject player;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update(){
		float interference = 0.1f;
		Power pow = player.GetComponent<Power> ();
		if (pow.power <= 0) {
			interference = 1f;
		} else {
			Jammer[] jammers = FindObjectsOfType<Jammer> ();
			float maxInterference = 0;
			foreach (Jammer j in jammers) {
				float dist = (j.transform.position - player.transform.position).magnitude;
				maxInterference = Mathf.Max (j.interference.Evaluate(dist), maxInterference);
			}
			GetComponent<AudioSource> ().volume = maxInterference;
			interference = (1 - pow.power / pow.maxPower) * 0.2f + maxInterference;
		}
		material.SetFloat ("_Interference", interference);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest){
		Graphics.Blit (src, dest, material);
	}
}
