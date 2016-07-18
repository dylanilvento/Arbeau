using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class EnableDisable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	//EventTrigger et;
	public GameObject[] window;
	GameManager gameMan;
	int windowIndex = 0;
	Button button;
	public Button other;

	public CloseEvent buttonEvent;

	EttiquetteSpammer etSpam = null;

	// Use this for initialization
	void Start () {
		//et = GetComponent<EventTrigger>();
		button = GetComponent<Button>();
		button.onClick.AddListener(TriggerButtonEvent);

		gameMan = GameObject.Find("Event Controller").GetComponent<GameManager>();

		if (gameObject.name.Contains("Disable")) {
			etSpam = GetComponent<EttiquetteSpammer>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse enter");
        if (windowIndex < window.Length && CheckSpammer() && !(gameMan.GetArbeauOff())) {
            GameObject win;

            //CanvasScaler scaler = GameObject.Find("Canvas").GetComponent<CanvasScaler>();

			//win = (GameObject) Instantiate (arbeauWin[index], new Vector2(0f, 0f), transform.rotation);
			win = (GameObject) Instantiate (window[windowIndex], new Vector2(0f, 0f), transform.rotation);

			//win.SetActive(false);
			win.name = "Arbeau Pop-up Window";
			win.transform.SetParent(GameObject.Find("Canvas").transform, false);
			//win.transform.localPosition = gameObject.transform.position;


			Vector2 newPos = eventData.position;
			
			//print("X:" + newPos.x);
			//print("screen width:" + Screen.width * 0.70f);
			//print("Ref res:" + newPos.x * scaler.referenceResolution.x);

			//if (newPos.x > 370f * scaler.referenceResolution.x) {
			if (newPos.x > Screen.width * 0.70f) {
				newPos = new Vector2 (Screen.width * 0.70f, newPos.y);
			}

			
			//win.GetComponent<RectTransform>().anchoredPosition = newPos;
			win.transform.position = newPos;
			windowIndex++;
	        //isOver = true;    	
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // Debug.Log("Mouse exit");
        //isOver = false;
    }

    bool CheckSpammer () {
    	if (gameObject.name.Contains("Disable") && etSpam != null) {
			if (etSpam.GetSpammerOn() == true) return false;
		}
		else {
			return true;
		}

		return true;
    }

    void TriggerButtonEvent () {
    	button.interactable = false;
    	other.interactable = true;



	  	if (buttonEvent != null) {
	  		//print("gonna call the button event");
	  		buttonEvent.StartEvent();
	  	}
    }

}
