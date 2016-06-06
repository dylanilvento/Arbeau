using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class FolderIcon : MonoBehaviour, IPointerDownHandler {
	public GameObject window;
	public static bool unopened = true;
	Image img;
	public Sprite inactive, active;
	float firstClickTime;
	bool firstClick;
	RectTransform rectTransform;
	GameManager gameMan;

	public enum SpawnType {FolderWindow, ArbeauWindow};
	public SpawnType spawn; //set in editor

	public FolderIcon[] folderIcons;
	public List<string> winText;

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

		//gameMan.GetLockWindows() keeps windows locked if desktop icon is opened
		if (unopened /*&& !gameMan.GetLockWindows()*/) {
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

		GameObject winObj;
		winObj = (GameObject) Instantiate (window, new Vector2(0f, 0f), transform.rotation);
		winObj.transform.SetParent(GameObject.Find("Canvas").transform, false);
		winObj.transform.localPosition = new Vector2(-130f, 0f);
		//unopened = false;
		firstClick = false;

		if (spawn == SpawnType.ArbeauWindow) {
			winObj.name = "Arbeau Alert";
			CloseWindow closeButton = winObj.transform.GetChild(3).GetComponent<CloseWindow>();
			closeButton.SetTextList(winText);
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

	}

	public void SetInactiveSprite () {
		img.sprite = inactive;

	}

	void DeactivateIcons () {

		foreach (FolderIcon icon in folderIcons) {
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
