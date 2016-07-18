using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RetrievalWindow : MonoBehaviour {

	Text text;
	// Use this for initialization
	void Start () {
		text = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
		if (gameObject.name.Contains("Retrieval")) {
			StartCoroutine("CycleLoadingText", "Updating tasks");
		}
		else if (gameObject.name.Contains("Distribution")) {
			StartCoroutine("CycleLoadingText", "Distributing resources");
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator CycleLoadingText (string str) {
		int cnt = 0;
		while (true) {
			
			if (cnt == 0) {
			text.text = str;
			cnt++;
			}
			else if (cnt == 1) {
				text.text = str + ".";
				cnt++;
			}
			else if (cnt == 2) {
				text.text = str + "..";
				cnt++;
			}
			else if (cnt == 3) {
				text.text = str + "...";
				cnt = 0;
			}

			yield return new WaitForSeconds(0.5f);
		}
	}
}