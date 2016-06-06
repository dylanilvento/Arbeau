using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class StartGame : MonoBehaviour {
	Button button;
	
	// Use this for initialization
	void Start () {
		button = GetComponent<Button>();
		button.onClick.AddListener(BeginGame);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void BeginGame () {
		Application.LoadLevel(1);
	}

}
