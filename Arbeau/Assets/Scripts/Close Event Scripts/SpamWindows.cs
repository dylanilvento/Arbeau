using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

//[System.Serializable]
public class SpamWindows : CloseEvent {

	public GameObject arbeau;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void StartEvent () {
		//print("JUST MAKING SURE THIS TEST WORKS");
		SpawnSpam();
		
	}

	void SpawnSpam () {
		for (int ii = 0; ii < 25; ii++) {
			GameObject win;

			win = (GameObject) Instantiate (arbeau, new Vector2(0f, 0f), transform.rotation);

			win.transform.SetParent(GameObject.Find("Canvas").transform, false);
			
			win.transform.localPosition = RandomizePosition();

			//yield return new WaitForSeconds(0.5f);		
		}

	}

	Vector2 RandomizePosition () {
		float yPos, xPos;
		float range = 100f;

		yPos = UnityEngine.Random.Range(-150f, 240f);

		if (yPos <= 20f) {
			xPos = UnityEngine.Random.Range(-265f, 220f);
		}

		else {
			xPos = UnityEngine.Random.Range(-265f, -70f);
		}

		Vector2 winPos = new Vector2(xPos, yPos);

		return winPos;
	}

}
