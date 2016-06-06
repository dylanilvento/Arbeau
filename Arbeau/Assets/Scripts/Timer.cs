using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour {
	float timeRemaining = 20;
	Text timer;
	double seconds;
	string fmt = "00";
	bool roundEnd = false;
	GameManager gameMan;
	
	//number of Arbeau windows active, set to -1 when not in use
	int winNum = -1;

	// Use this for initialization
	void Start () {
		timer = GetComponent<Text>();
		seconds = (Math.Truncate(timeRemaining) % 60);
		timer.text = ((int) timeRemaining / 60) + ":" + seconds.ToString(fmt);
		gameMan = GameObject.Find("Event Controller").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		//float newTime = Time.deltaTime;
		//timeRemaining -= Mathf.FloorToInt(newTime);
		if (winNum == 0 && timeRemaining >= 0f) {
			timeRemaining -= Time.deltaTime;
			seconds = (Math.Truncate(timeRemaining) % 60);
			timer.text = ((int) timeRemaining / 60) + ":" + seconds.ToString(fmt);//Math.Truncate(timeRemaining) + "";	
		}

		else if (timeRemaining <= 0f && !roundEnd) {
			roundEnd = true;
			gameMan.SetLockWindows(true);
			print("Ended round from timer");
			gameMan.EndRound();

		}
		
	}

	public void SetWinNum (int val) {
		winNum = val;
		//gameMan.SetWinNum(val);
	}

	public void SetRoundEnd (bool val) {
		roundEnd = val;
	}

	//called from CloseWindow.cs
	public void DecrementWinNum () {
		winNum--;
		
		if (winNum == 0) {
			/*GameObject.Find("Power Icon").GetComponent<DesktopIcon>().SetUnopened(true);
			gameMan.SetLockWindows(false);*/
			gameMan.LockWindows(false);
		}
	}

	public void SetTimer (float val) {
		timeRemaining = val;
	}

}
