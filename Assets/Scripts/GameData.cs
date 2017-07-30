using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

	public int iron = 0; 
	public int fuel = 0;
	public int daysLeft = 7;
	public float maxPower = 200f;
	public bool tutorial = true;

	public int batteryLevel = 0;
	public int engineLevel = 0;
	public int drillLevel = 0;
	public int sonarLevel = 0;

	public string[] batteryHints;
	public string[] engineHints;
	public string[] drillHints;
	public string[] sonarHints;

	public int[] upgradeCost;

	public static GameData instance;

	[TextArea(3, 100)]
	public string[] logMessages;
	int logIndex = 0;

	PopupScript popup;

	public void ApplyUpgradeStats(){
		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		Power power = player.GetComponent<Power> ();
		switch (batteryLevel) {
		case 0:
			power.maxPower = 200f;
			break;
		case 1:
			power.maxPower = 250f;
			break;
		case 2:
			power.maxPower = 300f;
			break;
		case 3:
			power.maxPower = 400f;
			break;
		}

		Thruster thruster = player.GetComponentInChildren<Thruster> ();
		Movement movement = player.GetComponentInChildren<Movement> ();
		switch (engineLevel) {
		case 0:
			movement.thrustMultiplier = 1f;
			movement.torqueMultiplier = 100f;
			thruster.fuelCost = 2f;
			break;
		case 1:
			movement.thrustMultiplier = 1.2f;
			movement.torqueMultiplier = 110f;
			thruster.fuelCost = 2.2f;
			break;
		case 2:
			movement.thrustMultiplier = 1.5f;
			movement.torqueMultiplier = 120f;
			thruster.fuelCost = 2.5f;
			break;
		case 3:
			movement.thrustMultiplier = 2f;
			movement.torqueMultiplier = 150f;
			thruster.fuelCost = 1f;
			break;
		}

		Drill drill = player.GetComponentInChildren<Drill> ();
		switch (drillLevel) {
		case 0:
			drill.drillStrength = 50f;
			drill.powerConsumption = 2f;
			break;
		case 1:
			drill.drillStrength = 60f;
			drill.powerConsumption = 2.2f;
			break;
		case 2:
			drill.drillStrength = 75f;
			drill.powerConsumption = 2.5f;
			break;
		case 3:
			drill.drillStrength = 90f;
			drill.powerConsumption = 1f;
			break;
		}

		SonarUI sonarui = FindObjectOfType<SonarUI> ();
		Sonar sonar = player.GetComponentInChildren<Sonar> ();
		switch (sonarLevel) {
		case 0:
			sonar.pingCost = 5f;
			sonarui.maxRange = 20f;
			break;
		case 1:
			sonar.pingCost = 6.5f;
			sonarui.maxRange = 22f;
			break;
		case 2:
			sonar.pingCost = 8f;
			sonarui.maxRange = 25f;
			break;
		case 3:
			sonar.pingCost = 2f;
			sonarui.maxRange = 40f;
			break;
		}
	}

	public void UpdateAsteroidParameters(){
		Asteroid asteroid = FindObjectOfType<Asteroid> ();
		switch (daysLeft) {
		case 6:
			asteroid.flyCount = 1;
			asteroid.wreckCount = 2;
			asteroid.maxSize = 150;
			asteroid.mineralProbabilities [0] = 0.3f;
			asteroid.mineralProbabilities [1] = 0.2f;
			break;
		case 5:
			asteroid.flyCount = 1;
			asteroid.wormCount = 1;
			asteroid.wreckCount = 2;
			asteroid.maxSize = 300;
			asteroid.mineralProbabilities [0] = 0.35f;
			asteroid.mineralProbabilities [1] = 0.25f;
			break;
		case 4:
			asteroid.flyCount = 2;
			asteroid.wormCount = 1;
			asteroid.bigMommaCount = 1;
			asteroid.wreckCount = 3;
			asteroid.maxSize = 500;
			asteroid.mineralProbabilities [0] = 0.4f;
			asteroid.mineralProbabilities [1] = 0.3f;
			break;
		case 3:
			asteroid.flyCount = 2;
			asteroid.wormCount = 2;
			asteroid.bigMommaCount = 1;
			asteroid.wreckCount = 3;
			asteroid.maxSize = 750;
			asteroid.mineralProbabilities [0] = 0.4f;
			asteroid.mineralProbabilities [1] = 0.35f;
			break;
		case 2:
			asteroid.flyCount = 1;
			asteroid.wormCount = 4;
			asteroid.bigMommaCount = 1;
			asteroid.wreckCount = 3;
			asteroid.maxSize = 1000;
			asteroid.mineralProbabilities [0] = 0.4f;
			asteroid.mineralProbabilities [1] = 0.4f;
			break;
		case 1:
			asteroid.flyCount = 1;
			asteroid.wormCount = 3;
			asteroid.bigMommaCount = 2;
			asteroid.wreckCount = 5;
			asteroid.maxSize = 1000;
			asteroid.mineralProbabilities [0] = 0.2f;
			asteroid.mineralProbabilities [1] = 0.2f;
			break;
		case 0:
			asteroid.flyCount = 3;
			asteroid.wormCount = 5;
			asteroid.bigMommaCount = 3;
			asteroid.wreckCount = 5;
			asteroid.maxSize = 1200;
			asteroid.mineralProbabilities [0] = 0.6f;
			asteroid.mineralProbabilities [1] = 0.6f;
			break;
		}
		asteroid.seed = (int) (Time.time * 1000f);
	}

	// Use this for initialization
	void Start () {
		instance = this;
		popup = FindObjectOfType<CanvasManager> ().popupPanel.GetComponent<PopupScript>();
	}

	public void ShowNextLog(){
		if (logIndex < logMessages.Length) {
			popup.SetLog (logMessages [logIndex]);
			popup.SetPopupVisible (true);
			logIndex++;
		}
	}

}
