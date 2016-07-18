using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class DesktopIcon : MonoBehaviour, IPointerDownHandler {
	public GameObject window;
	public static bool unopened = true;
	Image img;
	public Sprite inactive, active;

	public GameObject securityPrompt;

	static List<DesktopIcon> icons = new List<DesktopIcon>();
	
	float firstClickTime;
	bool firstClick;
	RectTransform rectTransform;
	GameManager gameMan;
	//public string name;
	// Use this for initialization
	void Start () {
		img = GetComponent<Image>();
		rectTransform = GetComponent<RectTransform>();
		gameMan = GameObject.Find("Event Controller").GetComponent<GameManager>();

		icons.Add(GetComponent<DesktopIcon>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerDown (PointerEventData data) {
		// print("test");
		if (unopened && !gameMan.GetLockWindows()) {
			if (!firstClick) {
				DeactivateIcons();
				firstClickTime = Time.timeSinceLevelLoad;
				SetActiveSprite();
				StartCoroutine("StartClickTimer");
				firstClick = true;
			}
			else if (firstClick && (Time.timeSinceLevelLoad - firstClickTime < 0.5f)) {
				//checks if settings icon
				if (gameObject.name.Contains("Settings")) {
					//checks security clearance
					if (gameMan.securityClearance < 3) {
						//if too low, give warning prompt
						CreateWindow(securityPrompt);
					}
						
					else {
						GameObject.Find("Task Window/Window Background/Timer").GetComponent<Timer>().SetPauseTime(true);
						StartCoroutine("SpawnWindow");
					}
						
				}
				else {
					StartCoroutine("SpawnWindow");
				}
				
			}
			
			else {
				firstClickTime = Time.timeSinceLevelLoad;
			}
		}
		
	}

	IEnumerator SpawnWindow () {
		gameMan.SetLoadingCursor(2);
		yield return new WaitForSeconds(0.75f);

		if (!gameMan.GetLockWindows()) {
			//Make sure this works
			CreateWindow(window);
			unopened = false;
			firstClick = false;
		}
		//SetInactiveSprite();
	}

	void CreateWindow (GameObject givenWindow) {
		GameObject winObj;
		winObj = (GameObject) Instantiate (givenWindow, new Vector2(0f, 0f), transform.rotation);
		winObj.transform.SetParent(GameObject.Find("Canvas").transform, false);
		winObj.transform.localPosition = new Vector2(-130f, 0f);
	}

	public IEnumerator Flash () {
		bool flashOn = false;
		//yield return new WaitForSeconds(1f);
		while (unopened) {
			if (!flashOn) {
				img.color = Color.red;
			}

			else {
				img.color = Color.white;
			}

			flashOn = !flashOn;

			yield return new WaitForSeconds(0.75f);
		}

	}

	public void SetActiveSprite () {
		img.sprite = active;
		rectTransform.sizeDelta = new Vector2(120f, 120f);	
		
	}

	public void SetInactiveSprite () {
		img.sprite = inactive;
		/*if (gameObject.name.Equals("Power Icon")) {
			rectTransform.sizeDelta = new Vector2(86f, 98f);

		}

		else {
			rectTransform.sizeDelta = new Vector2(84f, 98f);
			
		}*/

	}

	void DeactivateIcons () {
		
		foreach (DesktopIcon icon in icons) {
			icon.SetInactiveSprite();
		}

	}

	IEnumerator StartClickTimer () {
		yield return new WaitForSeconds(0.5f);
		firstClick = false;
	}

	public void SetUnopened (bool val) {
		unopened = val;
	}
}
