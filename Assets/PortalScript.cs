using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PortalScript : MonoBehaviour {

	CircleCollider2D coll;

	bool enterable = false;
	public float recoveryTime = 30f;

	// Use this for initialization
	void Start () {
		coll = GetComponent<CircleCollider2D> ();
		StartCoroutine (CollapseCoroutine());
	}

	void Update(){
		transform.rotation *= Quaternion.Euler(new Vector3 (0, 0, Time.deltaTime * 100f));
		if (enterable && coll.OverlapPoint (GameObject.FindGameObjectWithTag ("Player").transform.position)) {
			Debug.Log ("Escaped!");
			GameData data = FindObjectOfType<GameData> ();
			if (data.tutorial) {
				GameObject.FindGameObjectWithTag ("Player").GetComponent<Power>().maxPower = data.maxPower;
				GameObject.FindGameObjectWithTag ("Player").GetComponent<Power>().power = data.maxPower;
			}
			enterable = false;
			data.tutorial = false;
			data.daysLeft--;
			if (data.daysLeft < 0) {
				GameObject.FindGameObjectWithTag ("Player").GetComponent<Power>().power = -1f;
			} else {
				FindObjectOfType<CanvasManager> ().upgradePanel.GetComponent<UpgradePopupScript> ().SetPopupVisible (true);
			}
			//FindObjectOfType<Asteroid> ().Generate ();
		}
	}

	IEnumerator CollapseCoroutine(){
		float size = 1f;
		while (size > 0.1f) {
			size -= Time.deltaTime * 2f;
			transform.localScale = new Vector3 (size, size, size);
			yield return null;
		}
		StartCoroutine (ActivateCoroutine(size));
	}

	IEnumerator ActivateCoroutine(float a){
		float size = a;
		while (size < 1f) {
			size += Time.deltaTime / recoveryTime;
			transform.localScale = new Vector3 (size, size, size);
			yield return null;
		}
		enterable = true;
		StartCoroutine (BlinkCoroutine());
	}

	IEnumerator BlinkCoroutine(){
		float time = 0f;
		bool blink = false;
		while (enterable) {
			time += Time.deltaTime;
			if (time >= 0.5f) {
				time -= 0.5f;
				blink = !blink;
			}
			if (blink) {
				GetComponent<SpriteRenderer> ().color = Color.white;
			} else {
				GetComponent<SpriteRenderer> ().color = Color.gray;
			}
			yield return null;
		}
	}
}
