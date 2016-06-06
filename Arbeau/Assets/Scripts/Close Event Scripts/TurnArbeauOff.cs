using UnityEngine;
using System.Collections;

public class TurnArbeauOff : CloseEvent {

	GameManager gameMan;

	// Use this for initialization
	void Start () {

		gameMan = GameObject.Find("Event Controller").GetComponent<GameManager>();	
		print(gameMan);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void StartEvent () {
		print("Button event got called");
		//gameMan.SetArbeauOff(true);
		GameObject.Find("Event Controller").GetComponent<GameManager>().SetArbeauOff(true);
	}
}
