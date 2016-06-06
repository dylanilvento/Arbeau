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
				StartCoroutine("SpawnWindow");

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
			GameObject winObj;
			winObj = (GameObject) Instantiate (window, new Vector2(0f, 0f), transform.rotation);
			winObj.transform.SetParent(GameObject.Find("Canvas").transform, false);
			winObj.transform.localPosition = new Vector2(-130f, 0f);
			unopened = false;
			firstClick = false;
		}
		//SetInactiveSprite();
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
	
		GameObject.Find("Power Icon").GetComponent<DesktopIcon>().SetInactiveSprite();
		GameObject.Find("Suppression Icon").GetComponent<DesktopIcon>().SetInactiveSprite();
		GameObject.Find("Entertainment Icon").GetComponent<DesktopIcon>().SetInactiveSprite();
		GameObject.Find("Settings Icon").GetComponent<DesktopIcon>().SetInactiveSprite();

	}

	IEnumerator StartClickTimer () {
		yield return new WaitForSeconds(0.5f);
		firstClick = false;
	}

	public void SetUnopened (bool val) {
		unopened = val;
	}
}
