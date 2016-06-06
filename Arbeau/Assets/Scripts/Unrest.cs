using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Unrest : MonoBehaviour {
	public Sprite on;
	public Sprite off;
	public string district;
	Image[] pips;

	DistrictManager districtMan;
	Text plus;

	int jj = 0;

	// Use this for initialization
	void Start () {
		pips = gameObject.GetComponentsInChildren<Image>();
		districtMan = GameObject.Find("Event Controller").GetComponent<DistrictManager>();
		plus = GameObject.Find(district + " Plus").GetComponent<Text>();
		plus.color = new Color(plus.color.r, plus.color.g, plus.color.b, 0f);

		UpdateUnrest();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		UpdateUnrest();
	
	}

	void UpdateUnrest () {
		
		if (district.Equals("h1")) {
			print("------------------");
			jj = districtMan.districts[0].GetUnrest();
			print("h1: " + jj);
			//jj = currPip + 1;
			//totalPips = jj;
		}
		else if (district.Equals("h2")) {
			jj = districtMan.districts[1].GetUnrest();
			print("h2: " + jj);
			//jj = currPip + 1;
			//totalPips = jj;
		}

		else if (district.Equals("i1")) {
			jj = districtMan.districts[2].GetUnrest();
			print("i1: " + jj);
			//jj = currPip + 1;
			//totalPips = jj;
		}

		else if (district.Equals("i2")) {
			jj = districtMan.districts[3].GetUnrest();
			print("i2: " + jj);
			//jj = currPip + 1;
			//totalPips = jj;
		}
		//print("Unrest for " + district + ": " + jj);
		int kk = jj;
		

		if (kk >= 0) {
			for (int ii = 0; ii < kk; ii++) {
				if (ii < 5) {
					pips[ii].sprite = on;
				}
			}

			while (kk < pips.Length && kk < 5) {
				//print("Test");
				//print(kk);
				//print(ii);
				//pips[ii].color = Color.gray;
				pips[kk].sprite = off;
				//jj++;
				kk++;
			}
		}

		else if (kk < 0) {
			foreach (Image pip in pips) {
				pip.sprite = off;
			}
		}

		if (jj > 5) {
			plus.color = new Color(plus.color.r, plus.color.g, plus.color.b, 1f);
		}
	}
}
