using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class ArbeauAnimate : MonoBehaviour {
	public Sprite[] arbeauSprites;
	Image img;
	int spriteIndex = 0;
	// Use this for initialization
	void Start () {
		img = GetComponent<Image>();
		StartCoroutine("AnimatePortrait");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator AnimatePortrait () {
		while (true) {
			img.sprite = arbeauSprites[spriteIndex];

			spriteIndex++;
			if (spriteIndex >= arbeauSprites.Length) spriteIndex = 0;
			yield return new WaitForSeconds(0.25f);
		}
	}
}
