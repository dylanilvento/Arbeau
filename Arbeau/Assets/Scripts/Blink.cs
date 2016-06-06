using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class Blink : MonoBehaviour {
	Image img;
	// Use this for initialization
	void Start () {
		img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BlinkWin () {
		StartCoroutine(Blinking(Color.green));
	}

	public void BlinkLose () {
		StartCoroutine(Blinking(Color.red));
	}

	public void SetClose () {
		//StartCoroutine("Blinking");
		img.color = Color.black;
	}

	IEnumerator Blinking (Color newColor) {
		bool flashOn = false;
		//yield return new WaitForSeconds(1f);
		for (int ii = 0; ii < 8; ii++) {
			if (!flashOn) {
				img.color = newColor;
			}

			else {
				img.color = Color.white;
			}

			flashOn = !flashOn;

			yield return new WaitForSeconds(0.5f);
		}
		//yield return new WaitForSeconds(0.5f);
	}
}
