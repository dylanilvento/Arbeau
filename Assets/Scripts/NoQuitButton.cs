using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class NoQuitButton : MonoBehaviour {

	Button button;
	// Use this for initialization
	void Start () {
		button = GetComponent<Button>();
		button.onClick.AddListener(CloseWindow);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CloseWindow () {
		Destroy(gameObject.transform.parent);
	}
}
