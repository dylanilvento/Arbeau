using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class QuitGame : MonoBehaviour {
	Button button;
	
	// Use this for initialization
	void Start () {
		button = GetComponent<Button>();
		button.onClick.AddListener(ExitGame);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ExitGame () {
		Application.Quit();
	}

}