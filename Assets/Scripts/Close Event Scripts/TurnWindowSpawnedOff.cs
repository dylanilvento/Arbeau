using UnityEngine;
using System.Collections;

public class TurnWindowSpawnedOff : CloseEvent {

	public string settingName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void StartEvent () {

		GameObject.Find(settingName + "/Disable Button").GetComponent<EnableDisable>().windowSpawned = false;
	}

}
