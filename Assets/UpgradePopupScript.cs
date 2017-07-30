using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePopupScript : MonoBehaviour {

	public Text hintText;
	public Text resourceDisplay;

	void Update(){
		GameData data = GameData.instance;
		resourceDisplay.text = 
			"IRON: " + data.iron + 
			"\nEXOTIC: " + data.fuel +
			"\nDAYS LEFT: " + data.daysLeft;
	}

	public void SetPopupVisible(bool visible){
		gameObject.SetActive (visible);
		if (visible) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}
	}

	public void UpgradeBattery(){
		if (GameData.instance.upgradeCost [GameData.instance.batteryLevel] <= GameData.instance.iron) {
			GameData.instance.iron -= GameData.instance.upgradeCost [GameData.instance.batteryLevel];
			GameData.instance.batteryLevel++;
			GameData.instance.ApplyUpgradeStats ();
			hintText.text = "UPGRADE PURCHASED";
		} else {
			hintText.text = "NOT ENOUGH MATERIALS";
		}
	}

	public void UpgradeEngine(){
		if (GameData.instance.upgradeCost [GameData.instance.engineLevel] <= GameData.instance.iron) {
			GameData.instance.iron -= GameData.instance.upgradeCost [GameData.instance.engineLevel];
			GameData.instance.engineLevel++;
			GameData.instance.ApplyUpgradeStats ();
			hintText.text = "UPGRADE PURCHASED";
		} else {
			hintText.text = "NOT ENOUGH MATERIALS";
		}
	}

	public void UpgradeDrill(){
		if (GameData.instance.upgradeCost [GameData.instance.drillLevel] <= GameData.instance.iron) {
			GameData.instance.iron -= GameData.instance.upgradeCost [GameData.instance.drillLevel];
			GameData.instance.drillLevel++;
			GameData.instance.ApplyUpgradeStats ();
			hintText.text = "UPGRADE PURCHASED";
		} else {
			hintText.text = "NOT ENOUGH MATERIALS";
		}
	}

	public void UpgradeSonar(){
		if (GameData.instance.upgradeCost [GameData.instance.sonarLevel] <= GameData.instance.iron) {
			GameData.instance.iron -= GameData.instance.upgradeCost [GameData.instance.sonarLevel];
			GameData.instance.sonarLevel++;
			GameData.instance.ApplyUpgradeStats ();
			hintText.text = "UPGRADE PURCHASED";
		} else {
			hintText.text = "NOT ENOUGH MATERIALS";
		}
	}

	public void ShowBatteryHint(){
		hintText.text = GameData.instance.batteryHints[GameData.instance.batteryLevel] + "\n\nCOST " + GameData.instance.upgradeCost[GameData.instance.batteryLevel] + " iron";
	}

	public void ShowEngineHint(){
		hintText.text = GameData.instance.engineHints[GameData.instance.engineLevel] + "\n\nCOST " + GameData.instance.upgradeCost[GameData.instance.engineLevel] + " iron";
	}

	public void ShowDrillHint(){
		hintText.text = GameData.instance.drillHints[GameData.instance.drillLevel] + "\n\nCOST " + GameData.instance.upgradeCost[GameData.instance.drillLevel] + " iron";
	}

	public void ShowSonarHint(){
		hintText.text = GameData.instance.sonarHints[GameData.instance.sonarLevel] + "\n\nCOST " + GameData.instance.upgradeCost[GameData.instance.sonarLevel] + " iron";
	}

	public void NextScreen(){
		SetPopupVisible (false);
		Power pow = GameObject.FindGameObjectWithTag ("Player").GetComponent<Power> ();
		pow.power = pow.maxPower;
		GameData.instance.UpdateAsteroidParameters ();
		FindObjectOfType<Asteroid> ().Generate ();
	}

}
