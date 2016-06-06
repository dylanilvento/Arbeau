using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class AppearOption : MonoBehaviour {
	Button button;
	public GameObject appear, disappear;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button>();
		button.onClick.AddListener(ChangeMenu);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ChangeMenu () {
		appear.active = true;
		disappear.active = false;
	}
}
