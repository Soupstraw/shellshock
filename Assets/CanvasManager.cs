using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class CanvasManager : MonoBehaviour {

	Power power;

	public GameObject deathPanel, hudPanel, popupPanel, upgradePanel;
	public Text infoText;

	AudioSource source;

	// Use this for initialization
	void Start () {
		power = GameObject.FindGameObjectWithTag ("Player").GetComponent<Power> ();
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (power.power <= 0) {
			deathPanel.SetActive (true);
			hudPanel.SetActive (false);
		}
	}

	public void ShowInfo(string text){
		infoText.text = text;
		source.Play ();
	}

	public void RestartGame(){
		SceneManager.LoadScene( SceneManager.GetActiveScene().name );
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
