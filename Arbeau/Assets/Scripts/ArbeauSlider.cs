using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ArbeauSlider : MonoBehaviour {
	public SubmitArbeauWindow submit;
	Slider slider;


	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider>();
		slider.onValueChanged.AddListener(delegate{submit.SetActive();});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
