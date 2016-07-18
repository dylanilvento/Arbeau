using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReserveResource : MonoBehaviour {
	int currPip, totalPips;
	Image[] pips;
	public Sprite on;
	public Sprite off;
	public string resource;

	DistrictManager districtMan;
	// Use this for initialization
	void Start () {
		pips = gameObject.GetComponentsInChildren<Image>();
		districtMan = GameObject.Find("Event Controller").GetComponent<DistrictManager>();
		//currPip = 0;
		//totalPips = 1;
		int jj = 0;
		
		if (resource.Equals("Power")) {
			currPip = districtMan.districts[0].GetReservePower();
			jj = currPip + 1;
			totalPips = jj;
		}
		else if (resource.Equals("Suppression")) {
			currPip = districtMan.districts[0].GetReserveSuppression();
			jj = currPip + 1;
			totalPips = jj;
		}

		else if (resource.Equals("Entertainment")) {
			currPip = districtMan.districts[0].GetReserveEntertainment();
			jj = currPip + 1;
			totalPips = jj;
		}

		while (jj < pips.Length) {
			//print(ii);
			//pips[ii].color = Color.gray;
			pips[jj].sprite = off;
			jj++;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncrementPips () {
		//print("increment");
		//pips[currPip + 1].color = Color.green;
		pips[currPip + 1].sprite = on;
		currPip++;
		totalPips++;

		if (resource.Equals("Power")) {
			districtMan.districts[0].IncrementReservePower();
		}
		else if (resource.Equals("Suppression")) {
			districtMan.districts[0].IncrementReserveSuppression();
		}

		else if (resource.Equals("Entertainment")) {
			districtMan.districts[0].IncrementReserveEntertainment();
		}
	}

	public void DecrementPips () {
		//pips[currPip].color = Color.gray;
		pips[currPip].sprite = off;
		currPip--;
		totalPips--;

		if (resource.Equals("Power")) {
			districtMan.districts[0].DecrementReservePower();
			
		}
		else if (resource.Equals("Suppression")) {
			districtMan.districts[0].DecrementReserveSuppression();
		}

		else if (resource.Equals("Entertainment")) {
			districtMan.districts[0].DecrementReserveEntertainment();
		}
	}

	public int GetTotalPips () {
		return totalPips;
	}
}
