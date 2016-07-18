using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class VolumeController : MonoBehaviour {

	public GameObject volumeSlider;
	Button button;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button>();
		button.onClick.AddListener(ToggleShowVolume);
		volumeSlider.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ToggleShowVolume () {
		volumeSlider.SetActive(!volumeSlider.activeSelf);
	}
}
