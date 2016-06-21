using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class YesQuitButton : MonoBehaviour {

	Button button;
	// Use this for initialization
	void Start () {
		button = GetComponent<Button>();
		button.onClick.AddListener(QuitGame);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void QuitGame () {
		Application.Quit();
	}
}
