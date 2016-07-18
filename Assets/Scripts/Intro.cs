using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Intro : MonoBehaviour {
	public TextAsset textFile;
	string[] dialogLines;
	int currLine = 0;
	Text arbeauText;
	bool introFinished = true;
	// Use this for initialization
	void Start () {

		arbeauText = GameObject.Find("Arbeau Text").GetComponent<Text>();
		
	     
	     // Use this for initializati
	         // Make sure there this a text
	         // file assigned before continuing
	        if(textFile != null)
	        {
	            // Add each line of the text file to
	            // the array using the new line
	            // as the delimiter
	            dialogLines = (textFile.text.Split('\n'));
	        }
	    
	    PlayIntroWindow();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayIntroWindow () {
		//int ii = 0;
		string currDialog = null; //= ' ';
		int lineCnt = 0;
		//print(dialogLines.Length);
		if (currLine < dialogLines.Length) {
			print(currLine + " < " + dialogLines.Length);
			while (!(dialogLines[currLine].Equals("[Next]")) && !(dialogLines[currLine].Equals("[End]")) && !(dialogLines[currLine].Equals("[Event]"))) {
				if (currLine == 0) {
					currDialog = dialogLines[currLine];
					
				}

				else if (dialogLines[currLine].Equals("[New Line]")) {
					currDialog += "\n";
				}

				else {
					currDialog = currDialog + dialogLines[currLine] + " ";
				}

				currLine++;
				lineCnt++;
			}

			if (lineCnt == 1) {
				currDialog = currDialog + "\n" + "\n";
			}
			
			else if (lineCnt == 2) {
				currDialog = currDialog + "\n";
			}

			if (dialogLines[currLine].Equals("[End]")) {
				introFinished = true;
			}

			else if (dialogLines[currLine].Equals("[Event]")) {
				currLine++;
				int currEvent = Int32.Parse(dialogLines[currLine]);

				switch (currEvent) {
					case 1:
						StartEvent1();
						introFinished = true;
						break;
				}
			}

			currLine++;
		}

		arbeauText.text = currDialog;
	}

	public bool GetIntroFinished () {
		return introFinished;
	}

	void StartEvent1() {
		GameObject.Find("Power Icon").GetComponent<DesktopIcon>().StartCoroutine("Flash");
	}

}
