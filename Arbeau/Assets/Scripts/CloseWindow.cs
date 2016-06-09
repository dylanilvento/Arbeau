using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class CloseWindow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	GameObject parent;
	Image img;
	public Sprite active, deactive;

	Intro intro;
	TaskCreator taskCreator;
	Timer timer;

	public enum WindowType {Context, Win, Lose, Desktop, Alert};

	public WindowType winType;

	Text displayText;
	int textIndex = 0;

	public bool textFromInspector;
	public List<string> textList = new List<string>();
	public List<CloseEvent> events = new List<CloseEvent>();
	//public CloseEvent test;

	//Dictionary<int, CloseEvent> eventsMap = new Dictionary<int, CloseEvent>(); //index to event

	GameManager gameMan;
	// Use this for initialization
	void Start () {
		parent = gameObject.transform.parent.gameObject;
		img = GetComponent<Image>();
		intro = GameObject.Find("Event Controller").GetComponent<Intro>();
		taskCreator = GameObject.Find("Event Controller").GetComponent<TaskCreator>();
		gameMan = GameObject.Find("Event Controller").GetComponent<GameManager>();

		SetWinType();

		if (textFromInspector) {
			for (int ii = 0; ii < textList.Count; ii++) {
				textList[ii] = textList[ii].Replace("NEWLINE","\n");
			}
			
			displayText = gameObject.transform.parent.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
			if (textList[textIndex].Length <= 33) textList[textIndex] += "\n\n";
			displayText.text = textList[textIndex];
			textIndex++;
			print(textIndex);

		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetWinType () {
		
		if (parent.name.Contains("Context")) {
			winType = WindowType.Context;
		}

		else if (parent.name.Contains("Lose") || parent.name.Contains("Closure")) {
			winType = WindowType.Lose;
		}

		else if (parent.name.Contains(" Win ")) {
			winType = WindowType.Win;
		}

		else if (parent.name.Contains("Power") || parent.name.Contains("Suppression") || parent.name.Contains("Entertainment") || parent.name.Contains("Settings") || parent.name.Contains("System")) {
			winType = WindowType.Desktop;
		}

		else if (parent.name.Contains("Alert")) {
			winType = WindowType.Alert;
		}

		else if (textFromInspector) {
			winType = WindowType.Alert;
		}

		else {
			winType = WindowType.Desktop;
		}

	}

	public void SetTextList (List<string> list) {
		textList = list;

		displayText = gameObject.transform.parent.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
		
		for (int ii = 0; ii < textList.Count; ii++) {
			textList[ii] = textList[ii].Replace("NEWLINE","\n");
		}

		displayText.text = textList[textIndex];
		textIndex++;
	}

	public void OnPointerDown (PointerEventData data) {
		img.sprite = active;
	}

	public void OnPointerUp (PointerEventData data) {
		//print("Closing window");
		img.sprite = deactive;
		//print(textIndex);
		if (textIndex <= events.Count && events.Count > 0) {
			//print("Test 1");
			if (events[textIndex - 1] != null) {
				events[textIndex - 1].StartEvent();
			}
		}

		if (winType == WindowType.Desktop) {
			GameObject.Find("Power Icon").GetComponent<DesktopIcon>().SetUnopened(true);

			if (gameObject.transform.parent.gameObject.name.Contains("System")) {
			
				GameObject.Find("Task Window/Window Background/Timer").GetComponent<Timer>().SetPauseTime(false);
			
			}

			Destroy(parent);
		}	

		else if (textIndex <= textList.Count) {
			//print("Test 2");
		//if (winType != WindowType.Desktop) {
			/*if (!(intro.GetIntroFinished()) && parent.name.Equals("Arbeau Window")) {
				//print("Test A");
				intro.PlayIntroWindow();
			}*/

			/*else if (parent.name.Contains("Pop-up")) {
				print("Test B");
				Destroy(parent);
			}*/

			
				//print("Test C");
				//print(textIndex + " < " + textList.Count);
			if (textIndex < textList.Count) {
				//print("Test 3");
				if (textList[textIndex].Length <= 33) textList[textIndex] += "\n\n";

				displayText.text = textList[textIndex];
				textIndex++;
			}

			else {

				if (winType == WindowType.Context) {
					timer = GameObject.Find("Timer").GetComponent<Timer>();
					timer.DecrementWinNum();

					//print("Started timer");
				}
				else if (winType == WindowType.Lose || winType == WindowType.Win) {
					gameMan.DecrementWinNum();

					//print("Ended round");
				}
				//taskCreator.ResetSpeechIndex(num);
				Destroy(parent);	
			}
			
		}

		//this is pretty buggy, cause it's dependent on what comes first in the if/else chain
		//find a way for close windows to meet only one of these if statements
		else if (winType == WindowType.Context) {
			timer = GameObject.Find("Timer").GetComponent<Timer>();
			timer.DecrementWinNum();
			Destroy(parent);
			//print("Started timer");
		}

		else {
			Destroy(parent);	
		}
		
	}

}
