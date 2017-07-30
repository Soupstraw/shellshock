using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupScript : MonoBehaviour {

	public Text body;

	public void SetLog(string body){
		this.body.text = body;
	}

	public void SetPopupVisible(bool visible){
		gameObject.SetActive (visible);
		if (visible) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}
	}
}
