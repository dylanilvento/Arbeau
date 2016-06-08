using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public bool playGame;

	public GameObject retrievalWin, distrWin, introWin; //retrieval and distribution
	
	public Texture2D defaultCursor;
	public Texture2D[] loadingCursor = new Texture2D[7];

	DistrictManager distMan;
	TaskCreator taskCreator;
	ArbeauSpawner arbeauSpawner;

	public GameObject[] securityWin = new GameObject[4];

	int closedCnt = 0;
	public int securityClearance = 1;
	int winNum = -1;
	int round = 1;
	bool lockWindows = false;
	bool arbeauOff = false;

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

		//if (playGame) PlayIntro();
		//if (playGame) StartCoroutine("StartRound");
		if (playGame) StartRound();

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

	public bool GetArbeauOff () {
		return arbeauOff;
	}

	void PlayIntro () {
		GameObject win;
		win = (GameObject) Instantiate (introWin, new Vector2(0f, 200f), transform.rotation);
		
		win.transform.SetParent(GameObject.Find("Canvas").transform, false);
	}

	public void StartRound () {
		if (round == 3 || round == 7) {
			StartCoroutine("LoadSecurityCursor");
			
		}

		else {
			StartCoroutine("ContinueRoundStart");
		}
	}

	public void ContinueStartRound () {
		StartCoroutine("ContinueRoundStart");
	}

	IEnumerator LoadSecurityCursor () {
		SetLoadingCursor(5);
		yield return new WaitForSeconds(2f);
		SpawnSecurityPrompt();
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
			if (closedCnt == 0)
				SpawnWindow(securityWin[0]); //good level 2
			else
				SpawnWindow(securityWin[1]); //bad level 2
		}

		if (round == 7) {
			if (closedCnt == 0)
				SpawnWindow(securityWin[2]); //good level 3
			else
				SpawnWindow(securityWin[3]); //bad level 3
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
