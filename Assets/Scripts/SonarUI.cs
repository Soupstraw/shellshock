using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SonarUI : MonoBehaviour {

	public GameObject dotPrefab;
	public float maxRange = 20f;
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Ping(){
		GameObject[] enemies;
		if (GameData.instance.sonarLevel >= 0) {
			enemies = GameObject.FindGameObjectsWithTag ("Enemy");
			foreach (GameObject obj in enemies) {
				Vector3 rel = obj.transform.position - player.transform.position;
				rel.z = 0;
				if (rel.magnitude <= maxRange) {
					GameObject dot = Instantiate (dotPrefab, transform);
					dot.GetComponent<Image> ().color = Color.red;
					RectTransform rect = GetComponent<RectTransform> ();
					dot.GetComponent<RectTransform> ().localPosition = rel / maxRange * rect.rect.width / 2;
				}
			}
		}
		if (GameData.instance.sonarLevel >= 2) {
			enemies = GameObject.FindGameObjectsWithTag ("Wreck");
			foreach (GameObject obj in enemies) {
				Vector3 rel = obj.transform.position - player.transform.position;
				rel.z = 0;
				if (rel.magnitude <= maxRange) {
					GameObject dot = Instantiate (dotPrefab, transform);
					dot.GetComponent<Image> ().color = Color.blue;
					RectTransform rect = GetComponent<RectTransform> ();
					dot.GetComponent<RectTransform> ().localPosition = rel / maxRange * rect.rect.width / 2;
				}
			}
		};
		enemies = GameObject.FindGameObjectsWithTag ("Portal");
		foreach (GameObject obj in enemies) {
			Vector3 rel = obj.transform.position - player.transform.position;
			rel.z = 0;
			if (rel.magnitude <= maxRange) {
				GameObject dot = Instantiate (dotPrefab, transform);
				dot.GetComponent<Image> ().color = Color.magenta;
				RectTransform rect = GetComponent<RectTransform> ();
				dot.GetComponent<RectTransform>().localPosition = rel / maxRange * rect.rect.width/2;
			}
		}
		if (GameData.instance.sonarLevel >= 1) {
			enemies = GameObject.FindGameObjectsWithTag ("Mineral");
			foreach (GameObject obj in enemies) {
				Vector3 rel = obj.transform.position - player.transform.position;
				rel.z = 0;
				if (rel.magnitude <= maxRange) {
					GameObject dot = Instantiate (dotPrefab, transform);
					dot.GetComponent<Image> ().color = Color.yellow;
					RectTransform rect = GetComponent<RectTransform> ();
					dot.GetComponent<RectTransform> ().localPosition = rel / maxRange * rect.rect.width / 2;
				}
			}
		}
	}
}
