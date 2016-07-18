using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Submit : MonoBehaviour {
	GameManager gameMan;
	Button button;
	// Use this for initialization
	void Start () {
		gameMan = GameObject.Find("Event Controller").GetComponent<GameManager>();
		button = GetComponent<Button>();
		button.onClick.AddListener(EndRound);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void EndRound () {
		print("Ended round from submit button");
		gameMan.EndRound();
	}
}
