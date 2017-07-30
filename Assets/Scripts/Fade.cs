using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Fade : MonoBehaviour {

	Image rend;
	public float fadeTime;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Image> ();
		StartCoroutine (FadeCoroutine ());
	}

	IEnumerator FadeCoroutine(){
		float val = 1.0f;
		while (val >= 0) {
			val -= Time.deltaTime / fadeTime;
			Color c = rend.color;
			c.a = val;
			rend.color = c;
			yield return null;
		}
		Destroy (gameObject);
	}
}
