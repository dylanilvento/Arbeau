using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurnArbeauOn : CloseEvent {
	public GameObject welcome;
	// Use this for initialization
	GameManager gameMan;
	public GameObject etiquetteDisableButton;

	// Use this for initialization
	void Start () {

		gameMan = GameObject.Find("Event Controller").GetComponent<GameManager>();	

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void StartEvent () {

		GameObject win;

        //CanvasScaler scaler = GameObject.Find("Canvas").GetComponent<CanvasScaler>();

		//win = (GameObject) Instantiate (arbeauWin[index], new Vector2(0f, 0f), transform.rotation);
		win = (GameObject) Instantiate (welcome, new Vector2(0f, 0f), transform.rotation);

		//win.SetActive(false);
		//win.name = "Arbeau Pop-up Window";
		win.transform.SetParent(GameObject.Find("Canvas").transform, false);

		//gameMan.SetArbeauOff(false);
		GameObject.Find("Event Controller").GetComponent<GameManager>().SetArbeauOff(false);
		GameObject.Find("AI Etiquette/Disable Button").GetComponent<Button>().interactable = true;
	}
}
