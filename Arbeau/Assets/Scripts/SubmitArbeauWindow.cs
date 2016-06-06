using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SubmitArbeauWindow : MonoBehaviour {
	GameManager gameMan;
	GameObject window;
	Button button;

	Color orgButtonBG;
	Color orgButtonText;

	public Image buttonBG;
	public Text buttonText;
	// Use this for initialization
	void Start () {
		//gameMan = GameObject.Find("Event Controller").GetComponent<GameManager>();
		window = gameObject.transform.parent.gameObject;
		button = GetComponent<Button>();

		button.onClick.AddListener(CloseWindow);

		orgButtonBG = buttonBG.color;
		orgButtonText = buttonText.color;

		buttonBG.color = Color.clear;
		buttonText.color = Color.clear;

		button.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CloseWindow () {
		Destroy(window);
		//print("Ended round from submit button");
		//gameMan.EndRound();
	}

	public void SetActive () {
		buttonBG.color = orgButtonBG;
		buttonText.color = orgButtonText;

		button.interactable = true;
	}
}
