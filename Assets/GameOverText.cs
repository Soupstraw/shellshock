using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameOverText : MonoBehaviour {

	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameData.instance.fuel > 1000) {
			text.text = "You activate the wormhole and warp back to Earth. You sell the excess " + (GameData.instance.fuel - 1000) + " exotic ore and buy yourself a villa on the Mars. ";
		} else if (GameData.instance.fuel == 1000) {
			text.text = "You have just enough fuel to make it back home safely. You tell your friends about your adventures. They don't seem to believe you. It's a shame you don't have any exotic ores to show them. Not a single piece of ore. Nada. Nil.";
		} else {
			text.text = "You rush to the wormhole but the ore you have collected doesn't seem to have enough energy to make it possible for your mothership to pass. You see the wormhole close right in front of your eyes. You are stranded in space. Forever.";
		}
	}
}
