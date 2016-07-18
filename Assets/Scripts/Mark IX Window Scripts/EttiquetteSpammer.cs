using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class EttiquetteSpammer : MonoBehaviour, IPointerEnterHandler {
	bool spammerOn = false;
	public GameObject spamWindow;
	//
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSpammerOn (bool val) {
		spammerOn = val;
		print("Spammer for " + gameObject.name + " is on");

	}

	public bool GetSpammerOn () {
		return spammerOn;
	}

	public void OnPointerEnter (PointerEventData eventData) {
		if (spammerOn) {
			GameObject win;

			win = (GameObject) Instantiate (spamWindow, new Vector2(0f, 0f), transform.rotation);

			win.name = "Arbeau Pop-up Window";
			win.transform.SetParent(GameObject.Find("Canvas").transform, false);

			Vector2 newPos = eventData.position;
		
			if (newPos.x > Screen.width * 0.70f) {
				newPos = new Vector2 (Screen.width * 0.70f, newPos.y);
			}

			win.transform.position = newPos;
		}
	}
}
