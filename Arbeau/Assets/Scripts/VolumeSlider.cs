using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class VolumeSlider : MonoBehaviour {
	Slider slider;
	//AudioListener al;
	// Use this for initialization
	void Start () {
	//	al = GameObject.Find("Main Camera").GetComponent<AudioListener>();
		slider = GetComponent<Slider>();
		slider.onValueChanged.AddListener(delegate{ChangeVolume();});
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ChangeVolume () {
		AudioListener.volume = slider.value;
	}
}
