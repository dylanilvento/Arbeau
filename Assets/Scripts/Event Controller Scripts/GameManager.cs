using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public bool playGame;
	public bool arbeauOff = false;

	public GameObject retrievalWin, distrWin, introWin, gameOverWin; //retrieval and distribution
	
	public Texture2D defaultCursor;
	public Texture2D[] loadingCursor = new Texture2D[7];

	DistrictManager distMan;
	TaskCreator taskCreator;
	ArbeauSpawner arbeauSpawner;

	public GameObject[] securityWin = new GameObject[4];
	public GameObject[] securityWinNoArbeau = new GameObject[2];

	public GameObject[] endOfGameWin = new GameObject[3];
	public GameObject[] endOfGameWinNoArbeau = new GameObject[3];

	int closedCnt = 0;

	[Range(1, 3)]
	public int securityClearance;

	int winNum = -1;

	[Range(1, 11)]
	public int round = 1;
	bool lockWindows = false;

	Dictionary<string, int> closeMap = new Dictionary<string, int>();
	
	// Use this for initialization
	void Start () {
		distMan = GetComponent<DistrictManager>();
		taskCreator = GetComponent<TaskCreator>();
		arbeauSpawner = GetComponent<ArbeauSpawner>();

		closeMap.Add("h1", 0);
		closeMap.Add("h2", 1);
		closeMap.Add("i1", 2);
		closeMap.Add("i2", 3);

		if (playGame) StartCoroutine("PlayIntro");
		//if (playGame) StartCoroutine("StartRound");
		//if (playGame) StartRound();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int GetRound () {
		//print("Returning round");
		return round;
	}

	public void SetWinNum (int val) {
		winNum = val;
		//print("Number of context windows:" + val);
	}

	//called from TaskCreator.cs ???
	public void DecrementWinNum () {
		winNum--;
		if (winNum <= 0) {
			//StartCoroutine("StartRound");
			StartRound ();
		}
	}

	public void SetArbeauOff (bool val) {
		//print("It worked!");
		arbeauOff = val;
		//taskCreator.SetArbeauOff(val);
	}

	//called from ArbeauSpawner.cs, 
	public bool GetArbeauOff () {
		return arbeauOff;
	}

	IEnumerator PlayIntro () {
		SetLoadingCursor(5);
		yield return new WaitForSeconds(2f);
		arbeauSpawner.SetLockWindow(true);
		GameObject win;
		win = (GameObject) Instantiate (introWin, new Vector2(0f, 200f), transform.rotation);
		
		win.transform.SetParent(GameObject.Find("Canvas").transform, false);
	}

	public void StartRound () {
		
		if (closedCnt >= 4) {
			//print("closedCnt > 4?");
			StartCoroutine(LoadSpawn(gameOverWin));
		}

		else if (round > 10) {
			StartCoroutine("EndGame");
		}

		else if (round == 3 || round == 7) {
			StartCoroutine("LoadSecurityCursor");
			
		}

		else {
			StartCoroutine("ContinueRoundStart");
		}
	}

	public void ContinueStartRound () {
		StartCoroutine("ContinueRoundStart");
	}

	IEnumerator EndGame () {
		SetLoadingCursor(5);
		yield return new WaitForSeconds(2f);
		//if arbeau windows have been turned off
		if (arbeauOff) {
			if (closedCnt == 0) {
				SpawnWindow(endOfGameWinNoArbeau[0]);		
			}
			else if (closedCnt == 1 || closedCnt == 2) {
				SpawnWindow(endOfGameWinNoArbeau[1]);
			}
			else {
				SpawnWindow(endOfGameWinNoArbeau[2]);
			}
		}
		else {
			if (closedCnt == 0) {
				SpawnWindow(endOfGameWin[0]);		
			}
			else if (closedCnt == 1 || closedCnt == 2) {
				SpawnWindow(endOfGameWin[1]);
			}
			else {
				SpawnWindow(endOfGameWin[2]);
			}
		}
	}

	IEnumerator LoadSecurityCursor () {
		SetLoadingCursor(5);
		yield return new WaitForSeconds(2f);
		SpawnSecurityPrompt();
	}

	IEnumerator LoadSpawn (GameObject win) {
		SetLoadingCursor(5);
		yield return new WaitForSeconds(2f);
		SpawnWindow(win); 
	}

	IEnumerator ContinueRoundStart () {

		SetLoadingCursor(5);
		yield return new WaitForSeconds(2f);

		LockWindows(true);
		

		GameObject win;
		win = (GameObject) Instantiate (retrievalWin, new Vector2(0f, 0f), transform.rotation);
		
		win.transform.SetParent(GameObject.Find("Canvas").transform, false);
		//win.transform.localPosition = new Vector2(/*-270f-80f*/Random.Range(-270f, -80f), Random.Range(-240f, 130f));

		//win.name = "Retrieval Window";

		yield return new WaitForSeconds(2f);

		Destroy (win);

		taskCreator.StartCoroutine("SpawnArbeauContext");
		//SetLoadingCursor(false);
	}

	void SpawnSecurityPrompt () {
		if (round == 3) {
			if (closedCnt == 0 && !arbeauOff) {
				SpawnWindow(securityWin[0]); //good level 2
			}
			else if (closedCnt > 0 && !arbeauOff) {
				SpawnWindow(securityWin[1]); //bad level 2
			}
			else if (arbeauOff) {
				SpawnWindow(securityWinNoArbeau[0]);
			}
		}

		if (round == 7) {
			if (closedCnt == 0 && !arbeauOff) {
				SpawnWindow(securityWin[2]); //good level 3
			}
			else if (closedCnt > 0 && !arbeauOff) {
				SpawnWindow(securityWin[3]); //bad level 3
			}
			else if (arbeauOff) {
				SpawnWindow(securityWinNoArbeau[1]);
			}
		}

		securityClearance++;
	}

	void SpawnWindow (GameObject winToSpawn) {
		GameObject win;
		win = (GameObject) Instantiate (winToSpawn, new Vector2(0f, 0f), transform.rotation);
		win.name = "Distribution Window";
		win.transform.SetParent(GameObject.Find("Canvas").transform, false);
	}

	public void EndRound () {

		//print("Game manager recognized call");
		
		GameObject[] objs = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		foreach (GameObject obj in objs) {
			if (obj.name.Contains("Window")) {
				Destroy(obj);
			}
		}

		/*GameObject.Find("Power Icon").GetComponent<DesktopIcon>().SetUnopened(false);
		lockWindows = true;*/
		LockWindows(true);

		GameObject win;
		win = (GameObject) Instantiate (distrWin, new Vector2(0f, 0f), transform.rotation);
		win.name = "Distribution Window";
		win.transform.SetParent(GameObject.Find("Canvas").transform, false);
		//win.transform.localPosition = new Vector2(/*-270f-80f*/Random.Range(-270f, -80f), Random.Range(-240f, 130f));
		SetLoadingCursor(3);
		//win.name = "Distribution Window";

		StartCoroutine("CheckTasks");

	}

	IEnumerator CheckTasks () {
		yield return new WaitForSeconds(2f);

		GameObject win = GameObject.Find("Distribution Window");
		Destroy(win);
		

		Task[] tasks = taskCreator.tasks;
		
		SetWinNum(tasks.Length);

		District[] districts = distMan.districts;
		string winOrLose = "";
		string goal = "";
		int taskNum = 0;
		
		foreach (Task task in tasks) {
			District currDist = null;

			foreach (District district in districts) {

				if (district.name.Equals(task.GetDistrict(), StringComparison.InvariantCultureIgnoreCase)) {
					currDist = district;
				}
			}

			//print(task.GetDistrict() + " = " + currDist.name + "?");
			//print(task.GetGoal() + " " + task.GetScore() + ", " + currDist.name + ": " + currDist.GetResource(task.GetResource()));
			//if (task.GetResource().Contains("Power")) {
			goal = task.GetGoal().Trim();
			//print("(" + goal + ") equals 'Allocate'? " + (goal == "Allocate"));
			//print("(" + goal + ") equals 'Don't Allocate'? " + (goal == "Don't Allocate"));

			if (task.GetGoal().Equals("Don't Allocate")) {
				if (task.GetScore() >= currDist.GetResource(task.GetResource())) {
					winOrLose = "Win";
				}

				else {
					winOrLose = "Lose";
				}
			}

			else if (task.GetGoal().Equals("Allocate")) {

				if (task.GetScore() <= currDist.GetResource(task.GetResource())) {
					winOrLose = "Win";
				}

				else {
					winOrLose = "Lose";
				}
			}			

			if (winOrLose.Equals("Win")) {
				Blink blink = GameObject.Find(currDist.name.ToUpper() + " Menu Bar Background").GetComponent<Blink>();
				blink.BlinkWin();
				if (!arbeauOff) taskCreator.SpawnArbeauWinLose(taskNum, winOrLose);
			}

			else if (winOrLose.Equals("Lose")) {
				foreach (District district in distMan.districts) {
					if (currDist.name.Equals(district.name, StringComparison.InvariantCultureIgnoreCase)) {
						if (district.GetUnrest() >= 5) {
							district.CloseDistrict();
							//print(district.name + "is closed");
							Blink blink = GameObject.Find(currDist.name.ToUpper() + " Menu Bar Background").GetComponent<Blink>();
							blink.SetClose();

							int closeIndex;
							closeMap.TryGetValue(currDist.name, out closeIndex);

							taskCreator.SpawnArbeauClose(closeIndex);

							closedCnt++;
							print("closedCnt: " + closedCnt);
						}

						else {
							Blink blink = GameObject.Find(currDist.name.ToUpper() + " Menu Bar Background").GetComponent<Blink>();
							blink.BlinkLose();
							if (!arbeauOff) taskCreator.SpawnArbeauWinLose(taskNum, winOrLose);
						}

						district.IncrementUnrest();

					}
				}
			}

			
			winOrLose = "";
			
			taskNum++;
			//}
		}
		//print("increment round");
		round++;
	}

	public void SetLockWindows (bool val) {
		lockWindows = val;
	}

	//called by FolderIcon.cs and DesktopIcon.cs
	public bool GetLockWindows () {
		return lockWindows;
	}

	public void LockWindows (bool val) {
		GameObject.Find("Power Icon").GetComponent<DesktopIcon>().SetUnopened(!val);
		lockWindows = val;
		arbeauSpawner.SetLockWindow(val);

	}

	public void SetLoadingCursor (int val) {
		StartCoroutine(LoadingCursor(val));
	}

	IEnumerator LoadingCursor (int val) {
		for (int i = 0; i < val; i++) {
			foreach (Texture2D cursor in loadingCursor) {
				Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
				yield return new WaitForSeconds (0.1f);
			}
		}

		Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);

	}
}
