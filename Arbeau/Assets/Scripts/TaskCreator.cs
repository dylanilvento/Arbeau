using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
//using TaskManager;

public class TaskCreator : MonoBehaviour {
	static int round;
	public GameObject arbeau, taskWin;
	//Text[] arbeauText;
	TaskManager taskMan;
	public Task[] tasks;

	public GameObject[] closeWindow;

	//List<List<string>> dialogList = new List<List<string>>();

	Dictionary<int, float> timerMap = new Dictionary<int, float>();
	public float[] roundTimes = new float[10];

	DistrictManager distMan;
	GameManager gameMan;

	int[] taskIndex;
	int[] score;
	string[] district;
	string[] resource;
	string[] goal;
	Vector2[] winPos;
	//int taskNum = 1;
	//bool[] arbeauFinished;
	bool[] isLowTask;

	//int[] speechIndex;

	Timer timer;

	//Task task;
	// Use this for initialization
	void Start () {

		distMan = GetComponent<DistrictManager>();
		gameMan = GetComponent<GameManager>();

		for (int ii = 0; ii < roundTimes.Length; ii++) {
			int roundNum = ii + 1;
			timerMap.Add(roundNum, roundTimes[ii]);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator SpawnArbeauContext () {
		yield return new WaitForSeconds(1f);
		taskMan = GetComponent<TaskManager>(); //I feel like I put this here for a reason

		CreateTasks();

		int[] cnt = LowHighCount();

		if (gameMan.GetArbeauOff()) {
			for (int ii = 0; ii < taskIndex.Length; ii++) {
				GameObject win;

				List<string> contextList = new List<string>();

				win = (GameObject) Instantiate (arbeau, new Vector2(0f, 0f), transform.rotation);
				
				CloseWindow closeButton = win.transform.GetChild(3).GetComponent<CloseWindow>();

				win.transform.SetParent(GameObject.Find("Canvas").transform, false);
				
				win.transform.localPosition = RandomizePosition(ii);

				win.name = "Arbeau Context Window " + ii;
				
				contextList = GetContext(ii);

				closeButton.SetTextList(contextList);

			}
		}
	}

	List<string> GetContext (int index) {
		List<string> contextList = new List<string>();

		string firstLine = tasks[index].GetBeforeDist().Trim() + " " + tasks[index].GetDistrict() + " " + tasks[index].GetAfterDist().Trim();
		
		//if (firstLine.Length <= 33) firstLine += "\n\n";

		contextList.Add(firstLine);

		if (tasks[index].GetExtraContextSize() > 0) {
			for (int ii = 0; ii < tasks[index].GetExtraContextSize(); ii++) {
				string line = tasks[index].GetContext(ii);
				contextList.Add(line);
				//if (line.Length <= 33) line += "\n\n";
			}
		}

		return contextList;
	}

	List<string> GetWin (int index) {
		List<string> contextList = new List<string>();

		for (int ii = 0; ii < tasks[index].GetWinRespSize(); ii++) {
			string line = tasks[index].GetWinResp(ii);
			contextList.Add(line);
			//if (line.Length <= 33) line += "\n\n";
		}

		return contextList;
		
	}

	List<string> GetLose (int index) {
		List<string> contextList = new List<string>();

		for (int ii = 0; ii < tasks[index].GetLoseRespSize(); ii++) {
			string line = tasks[index].GetLoseResp(ii);

			//if (line.Length <= 33) line += "\n\n";
			
			contextList.Add(line);
		}

		return contextList;
	}

	//called from GameManager.cs
	public void SpawnArbeauWinLose (int index, string type) {

		GameObject win;
		win = (GameObject) Instantiate (arbeau, new Vector2(0f, 0f), transform.rotation);
		win.transform.SetParent(GameObject.Find("Canvas").transform, false);
		
		win.transform.localPosition = RandomizePosition(index);

		CloseWindow closeButton = win.transform.GetChild(3).GetComponent<CloseWindow>();
		List<string> contextList = new List<string>();

		win.name = "Arbeau " + type + " Window " + index;

		if (type.Equals("Win")) {

			contextList = GetWin(index);
			closeButton.SetTextList(contextList);

		}

		else if (type.Equals("Lose")) {

			contextList = GetLose(index);
			closeButton.SetTextList(contextList);

		}
			
	}

	//called from GameManager.cs
	//index is index of closedWin array
	public void SpawnArbeauClose (int index) {
		GameObject win;
		win = (GameObject) Instantiate (closeWindow[index], new Vector2(0f, 0f), transform.rotation);
		win.transform.SetParent(GameObject.Find("Canvas").transform, false);
		win.name = "Arbeau Closure Window";
	}

	void CreateTasks () {
		round = gameMan.GetRound();
		//print("Round: " + round);
		int closedCnt = distMan.districts[0].GetClosedCount();
		//print("closed count: " + closedCnt);
		int itemNum = 0;

		if (round == 1) {
			itemNum = 1;

		}

		else if (round >= 2 && round <= 3) {
			itemNum = 2;

			if (closedCnt == 3) {
				itemNum = 1;
			}

		}

		else if (round >= 4) {
			itemNum = 3;

			if (closedCnt == 2) {
				itemNum = 2;
			}

			else if (closedCnt == 3) {
				itemNum = 1;
			}
			
		}
		//print("itemNum: " + itemNum);

		SetItems(itemNum);

		AssignTasks();

		GameObject win;
		string taskList = null;
		win = (GameObject) Instantiate (taskWin, new Vector2(0f, 0f), transform.rotation);
		win.name = "Task Window";
		win.transform.SetParent(GameObject.Find("Canvas").transform, false);
		win.transform.localPosition =  new Vector2(430, 140);

		//need to add text to window itself

		Text taskText = GameObject.Find("Tasks").GetComponent<Text>();

		for (int ii = 0; ii < taskIndex.Length; ii++) {
			//print ("Assigning task to task window: ("  + tasks[ii].GetGoal() + ").");
			

			if (tasks[ii].GetGoal().Trim().Equals("Allocate")) {
				//print("== allocate");
				taskList += "Allocate at least " + tasks[ii].GetScore() + " " + tasks[ii].GetResource() + " to " + tasks[ii].GetDistrict();
			}

			else if (tasks[ii].GetGoal().Trim().Contains("Don't Allocate")) {
				taskList += "Don't allocate more than " + tasks[ii].GetScore() + " " + tasks[ii].GetResource() + " to " + tasks[ii].GetDistrict();
			}

			else if (tasks[ii].GetGoal().Contains("Don't Redirect")) {
				taskList += "Don't redirect less than " + tasks[ii].GetScore() + " " + tasks[ii].GetResource() + " to " + tasks[ii].GetDistrict();
			}

			if (ii < taskIndex.Length - 1) {
				taskList += "\n" + "\n";
			}
		}

		taskText.text = taskList;

		timer = GameObject.Find("Timer").GetComponent<Timer>();
		
		/////////*******************
		if (gameMan.GetArbeauOff()) {
		
			timer.SetWinNum(taskIndex.Length);
		
		}
		else {
		
			timer.SetWinNum(0);
		
		}
		

		timer.SetTimer(CreateTimer());

	}

	void AssignTasks () {
		
		//print("Assigning tasks");
		int[] cnt = LowHighCount();
		//print("lowCnt: " + cnt[0] + ", highCnt: " + cnt[1]);
		bool[] distUsed = new bool[4] {false, false, false, false};


		//print(taskIndex.Length);

		for (int ii = 0; ii < taskIndex.Length; ii++) {
			

			if (cnt[0] > 0) {	
				bool assigned = false;
				
				while (!assigned) {
					taskIndex[ii] = Mathf.FloorToInt(UnityEngine.Random.Range(0f, (float) taskMan.lowTasks.Length - 0.1f));
					assigned = CompareTasks(ii);
					//print(taskIndex[ii]);
				}
				
				tasks[ii] = taskMan.GetLowTask();
				
				if (tasks[ii].GetResource().Contains("Power")) {
					
					tasks[ii].SetScore(Mathf.FloorToInt(UnityEngine.Random.Range(2f, 3.9f)));

				}
				
				else {
					
					tasks[ii].SetScore(Mathf.FloorToInt(UnityEngine.Random.Range(1f, 2.9f)));
					
				}

				cnt[0]--;
			}

			else if (cnt[1] > 0) {
				bool assigned = false;
				
				while (!assigned) {
					taskIndex[ii] = Mathf.FloorToInt(UnityEngine.Random.Range(0f, (float) taskMan.highTasks.Length - 0.1f));	
					assigned = CompareTasks(ii);
				}
				
				tasks[ii] = taskMan.GetHighTask();

				if (tasks[ii].GetResource().Contains("Power")) {
					
					tasks[ii].SetScore(Mathf.FloorToInt(UnityEngine.Random.Range(4f, 5.9f)));
				}
				
				else {
					tasks[ii].SetScore(3);
				}

				cnt[1]--;
			}

			int distChoice = 0;

			while (tasks[ii].GetDistrict() == null) {

				distChoice = Mathf.FloorToInt(UnityEngine.Random.Range(1f, 4.9f));

				if (distChoice == 1 && !distUsed[0]) {
					if (!distMan.districts[0].GetClosed()) {
						tasks[ii].SetDistrict("H1");
					}

					distUsed[0] = true;
				}

				else if (distChoice == 2  && !distUsed[1]) {
					if (!distMan.districts[1].GetClosed()) {
						tasks[ii].SetDistrict("H2");
					}
					distUsed[1] = true;
				}

				else if (distChoice == 3  && !distUsed[2]) {
					if (!distMan.districts[2].GetClosed()) {
						tasks[ii].SetDistrict("I1");
					}
					distUsed[2] = true;
				}

				else if (distChoice == 4  && !distUsed[3]) {
					if (!distMan.districts[3].GetClosed()) {
						tasks[ii].SetDistrict("I2");
					}
					distUsed[3] = true;
				}
			}
		}
	}

	int[] LowHighCount () {
		int highCnt = 0;
		int lowCnt = 0;
		int closedCnt = distMan.districts[0].GetClosedCount();
		
		if (round == 1) {
			lowCnt = 1;

		}

		else if (round >= 2 && round <= 3) {
			lowCnt = 2;

			if (closedCnt == 3) {
				lowCnt--;
			}
		}

		else if (round == 4) {
			lowCnt = 3;

			if (closedCnt == 2) {
				lowCnt--;
			}

			else if (closedCnt == 3) {
				lowCnt -= 2;
			}

		}

		else if (round >= 5 && round <= 6) {
			lowCnt = 2;
			highCnt = 1;

			if (closedCnt == 2) {
				lowCnt--;
				//highCnt--;
			}

			else if (closedCnt == 3) {
				lowCnt -= 2;
			}

		}

		else if (round == 7) {
			lowCnt = 1;
			highCnt = 2;

			if (closedCnt == 2) {
				lowCnt--;
				//highCnt--;
			}

			else if (closedCnt == 3) {
				lowCnt--;
				highCnt--;
			}
		}

		else if (round >= 8) {
			highCnt = 3;

			if (closedCnt == 2) {
				highCnt--;
			}
			else if (closedCnt == 3) {
				highCnt -= 2;
			}
		}

		int[] cnt = {lowCnt, highCnt};
		return cnt;
	}

	void SetItems (int num) {
		tasks = new Task[num];
		//TEST
		//arbeauText = new Text[num];
		taskIndex = new int[num];
		//TEST
		//speechIndex = new int[num];
		winPos = new Vector2[num];

		/*for (int ii = 0; ii < speechIndex.Length; ii++) {
			speechIndex[ii] = 0;
		}*/
	}

	bool CompareTasks (int index) {
		// print ("index: " + index);
		//return true;

		if (index > 0) {
			for (int ii = index - 1; ii >= 0; ii--) {
				//print ("ii: " + ii + " = " + index + " - 1");
				// print("is " + taskIndex[index] + " == " + taskIndex[ii] + "?");
				if (taskIndex[index] == taskIndex[ii]) {
					//print("yes");
					// return true;
					return false;
				}
				//print("no");
			}
			
			//print ("Exit for loop");
			return true;
		}

		else {
			//print("skipping comparison");
			return true;
		}
		
	}

	Vector2 RandomizePosition (int index) {
		float yPos, xPos;
		float range = 100f;

		bool matched = false;

		do {
			matched = false;

			yPos = Random.Range(-150f, 240f);

			if (yPos <= 20f) {
				xPos = Random.Range(-265f, 220f);
			}

			else {
				xPos = Random.Range(-265f, -70f);
			}

			for (int ii = 0; ii < index; ii++) {
				if ((winPos[ii].y <= yPos + range) && (winPos[ii].y) >= yPos - range) {
					matched = true;
				}
				
			}

			range -= 10f;

		} while (matched);

		winPos[index] = new Vector2(xPos, yPos);

		return winPos[index];
	}

	float CreateTimer () {
		float t = 20f;
		
		timerMap.TryGetValue(round, out t);

		return t;
	}
}
