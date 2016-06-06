using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Resource : MonoBehaviour, IPointerDownHandler {
	bool filled = false;
	Image img;
	public Resource[] pips;
	GameObject parent;
	int pipNum;
	ReserveResource resResource;
	public Sprite on, off, closed;
	public string district;
	public string resource;

	bool closedVal = false;

	Text warning;

	GameManager gameMan;
	DistrictManager districtMan;
	//GameObject[] pips = new GameObject[5];

	// Use this for initialization
	void Start () {

		//print(pipNum);
		districtMan = GameObject.Find("Event Controller").GetComponent<DistrictManager>();
		gameMan = GameObject.Find("Event Controller").GetComponent<GameManager>();
		warning = GameObject.Find(district + " Warning Text").GetComponent<Text>();
		warning.color = new Color (warning.color.r, warning.color.g, warning.color.b, 0f);

		img = GetComponent<Image>();
		parent = gameObject.transform.parent.gameObject;
		pips = parent.GetComponentsInChildren<Resource>();
		resResource = GameObject.Find("Reserve Resource Group").GetComponent<ReserveResource>();
		//print(pips[0]);

		/*for (int ii = 0; ii < pips.Length; ii++) {
			print(pips[ii]);
		}*/
		img.sprite = off;

		if (pips.Length == 5) {
			switch(gameObject.name) {
				case "Resource Pip 1":
					pipNum = 1;
					break;
				case "Resource Pip 2":
					pipNum = 2;
					break;
				case "Resource Pip 3":
					pipNum = 3;
					break;
				case "Resource Pip 4":
					pipNum = 4;
					break;
				case "Resource Pip 5":
					pipNum = 5;
					break;
			}
		}

		else if (pips.Length == 3) {
			switch(gameObject.name) {
				case "Resource Pip 1":
					pipNum = 1;
					break;
				case "Resource Pip 2":
					pipNum = 2;
					break;
				case "Resource Pip 3":
					pipNum = 3;
					break;
			}
		}

		else {
			print ("Pips don't fit premade length.");
		}


		if (district.Equals("h1") && (districtMan.districts[0].GetResource(resource) >= pipNum)) {
			filled = true;
			img.sprite = on;
		}

		else if (district.Equals("h2") && (districtMan.districts[1].GetResource(resource) >= pipNum)) {
			filled = true;
			img.sprite = on;
		}

		else if (district.Equals("i1") && (districtMan.districts[2].GetResource(resource) >= pipNum)) {
			filled = true;
			img.sprite = on;
		}

		else if (district.Equals("i2") && (districtMan.districts[3].GetResource(resource) >= pipNum)) {
			filled = true;
			img.sprite = on;
		}

		//print(pipNum);

		//filled = false; //don't move this 

		if (district.Equals("h1")) {
			closedVal = districtMan.districts[0].GetClosed();
			if (closedVal) img.sprite = closed;
		}

		else if (district.Equals("h2")) {
			closedVal = districtMan.districts[1].GetClosed();
			if (closedVal) img.sprite = closed;
		}

		else if (district.Equals("i1")) {
			closedVal = districtMan.districts[2].GetClosed();
			if (closedVal) img.sprite = closed;
		}

		else if (district.Equals("i2")) {
			closedVal = districtMan.districts[3].GetClosed();
			if (closedVal) img.sprite = closed;
		}
		
			
		//print (filled);
	}
	
	// Update is called once per frame
	void Update () {
		/*if (filled) {
			//img.color = Color.green;
			img.sprite = on;
		}

		else {
			//img.color = Color.gray;
			img.sprite = off;
		}*/
	
	}

	void FixedUpdate () {
	
	}

	public void OnPointerDown (PointerEventData data) {
		if (!closedVal) {
			//print(pipNum);
			if (pips.Length == 5) {
				if (pipNum == 1 && !pips[1].GetFilled()) {
					ChangePip();
				}
				else if (pipNum == 5 && pips[pipNum - 2].GetFilled()) {
					ChangePip();
				}
				else if (pipNum != 1 && pips[pipNum - 2].GetFilled() && !pips[pipNum].GetFilled()) {
					ChangePip();
				}
			}

			else if (pips.Length == 3) {
				if (pipNum == 1 && !pips[1].GetFilled()) {
					ChangePip();
				}
				else if (pipNum == 3 && pips[pipNum - 2].GetFilled()) {
					ChangePip();
				}
				else if (pipNum != 1 && pips[pipNum - 2].GetFilled() && !pips[pipNum].GetFilled()) {
					ChangePip();
				}
			}
 		}
		
	}

	void ChangePip () {
		if (filled) {
			if (district.Equals("h1") && districtMan.districts[0].DecrementResource(resource)) {
				//filled = !filled;
				filled = false;
				img.sprite = off;
				resResource.IncrementPips();
				warning.color = new Color (warning.color.r, warning.color.g, warning.color.b, 0f);
			}
			else if (district.Equals("h2") && districtMan.districts[1].DecrementResource(resource)) {
				//filled = !filled;
				filled = false;
				img.sprite = off;
				resResource.IncrementPips();
				warning.color = new Color (warning.color.r, warning.color.g, warning.color.b, 0f);
			}
			else if (district.Equals("i1") && districtMan.districts[2].DecrementResource(resource)) {
				//filled = !filled;
				filled = false;
				img.sprite = off;
				resResource.IncrementPips();
				warning.color = new Color (warning.color.r, warning.color.g, warning.color.b, 0f);
			}
			else if (district.Equals("i2") && districtMan.districts[3].DecrementResource(resource)) {
				//filled = !filled;
				filled = false;
				img.sprite = off;
				resResource.IncrementPips();
				warning.color = new Color (warning.color.r, warning.color.g, warning.color.b, 0f);
			}
			else {
				warning.color = new Color (warning.color.r, warning.color.g, warning.color.b, 1f);
			}

			//filled = false;

		}
		else if (!filled && resResource.GetTotalPips() > 0) {
			if (district.Equals("h1") && districtMan.districts[0].IncrementResource(resource)) {
				//filled = !filled;
				filled = true;
				img.sprite = on;
				resResource.DecrementPips();
				warning.color = new Color (warning.color.r, warning.color.g, warning.color.b, 0f);
			}
			else if (district.Equals("h2") && districtMan.districts[1].IncrementResource(resource)) {
				//filled = !filled;
				filled = true;
				img.sprite = on;
				resResource.DecrementPips();
				warning.color = new Color (warning.color.r, warning.color.g, warning.color.b, 0f);
			}
			else if (district.Equals("i1") && districtMan.districts[2].IncrementResource(resource)) {
				//filled = !filled;
				filled = true;
				img.sprite = on;
				resResource.DecrementPips();
				warning.color = new Color (warning.color.r, warning.color.g, warning.color.b, 0f);
			}
			else if (district.Equals("i2") && districtMan.districts[3].IncrementResource(resource)) {
				//filled = !filled;
				filled = true;
				img.sprite = on;
				resResource.DecrementPips();
				warning.color = new Color (warning.color.r, warning.color.g, warning.color.b, 0f);
			}
			else {
				warning.color = new Color (warning.color.r, warning.color.g, warning.color.b, 1f);
			}
			//filled = !filled;
			//resResource.DecrementPips();
		}
		//print(districtMan.districts[0].GetResource(resource));
		//print(pipNum);
	}

	public bool GetFilled () {
		//print(pipNum + ", filled = " + filled);
		return filled;
	}
}
