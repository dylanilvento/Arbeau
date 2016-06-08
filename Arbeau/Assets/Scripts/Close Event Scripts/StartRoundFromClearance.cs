using UnityEngine;
using System.Collections;

public class StartRoundFromClearance : CloseEvent {
	//public GameObject welcome;
	// Use this for initialization

	public override void StartEvent () {

		GameObject.Find("Event Controller").GetComponent<GameManager>().ContinueStartRound();
	}
}
