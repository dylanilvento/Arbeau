using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour {
	public TextAsset lowDiff, highDiff;
	string[] newLines;
	public Task[] lowTasks;
	public Task[] highTasks;

	List<Task> lowTasksList = new List<Task>();
	List<Task> highTasksList = new List<Task>();
	// Use this for initialization
	void Start () {

		if (lowDiff != null) {
			//print("adding tasks");
			newLines = (lowDiff.text.Split('\n'));
			lowTasks = AddTasks(newLines);
		}

		foreach (Task task in lowTasks) {
			lowTasksList.Add(task);
		}

		if (highDiff != null) {
			newLines = (highDiff.text.Split('\n'));
			highTasks = AddTasks(newLines);
		}

		foreach (Task task in highTasks) {
			highTasksList.Add(task);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Task GetLowTask () {
		int index = Mathf.FloorToInt(UnityEngine.Random.Range(0f, (float) lowTasksList.Count - 0.1f));

		Task task = lowTasksList[index];
		lowTasksList.RemoveAt(index);

		return task;
	}

	public Task GetHighTask () {
		int index = Mathf.FloorToInt(UnityEngine.Random.Range(0f, (float) highTasksList.Count - 0.1f));

		Task task = highTasksList[index];
		highTasksList.RemoveAt(index);

		return task;
	}

	Task[] AddTasks (string[] lines) {
		
		int currLine = 0;

		Task[] tasks = new Task[System.Int32.Parse(lines[currLine])];
		//print("Number of tasks: " + tasks.Length);
		currLine++;

		int taskLine = 0;
		int taskIndex = 0;

		while (currLine < lines.Length) {
			
			if (taskLine == 0){
				// print(currLine);
				//print("Found resource: " + lines[currLine]);
				tasks[taskIndex] = new Task (lines[currLine].Trim());
				currLine++;
				taskLine++;
			}
			
			if (taskLine == 1) {
				// print(currLine);
				//print("Goal: " + lines[currLine]);
				tasks[taskIndex].SetGoal(lines[currLine].Trim());
				//print(tasks[taskIndex].GetGoal());
				currLine++;
				taskLine++;
			}

			if (taskLine == 2) {
				// print(currLine);
				// print("Before: " + lines[currLine]);
				// print(taskIndex);
				tasks[taskIndex].SetBeforeDist(lines[currLine].Trim());
				currLine++;
				taskLine++;
			}
			if (taskLine == 3) {
				// print(currLine);
				// print("After: " + lines[currLine]);
				tasks[taskIndex].SetAfterDist(lines[currLine].Trim());
				currLine++;
				taskLine++;
			}

			int extraLines = System.Int32.Parse(lines[currLine].Trim());

			if (taskLine == 4) {
				
				tasks[taskIndex].SetExtraContextSize(extraLines);
				currLine++;

				if (extraLines > 0) {

					for (int ii = 0; ii < extraLines; ii++) {
						// print(currLine);
						// print("Contex: " + lines[currLine]);
						tasks[taskIndex].SetContext(ii, lines[currLine].Trim());
						currLine++;
						
					}

				}

				else {
					// print(currLine);
				}

				taskLine++;
			}
			// print(currLine);

			extraLines = System.Int32.Parse(lines[currLine].Trim());

			if (taskLine == 5) {

				tasks[taskIndex].SetWinRespSize(extraLines);
				currLine++;

				if (extraLines > 0) {

					for (int ii = 0; ii < extraLines; ii++) {
						// print(currLine);
						// print("Win: " + lines[currLine]);
						tasks[taskIndex].SetWinResp(ii, lines[currLine].Trim());
						currLine++;
						
					}

				}

				else {
					// print(currLine);
				}

				taskLine++;

			}

			extraLines = System.Int32.Parse(lines[currLine].Trim());

			if (taskLine == 6) {

				tasks[taskIndex].SetLoseRespSize(extraLines);
				currLine++;

				if (extraLines > 0) {

					for (int ii = 0; ii < extraLines; ii++) {
						// print(currLine);
						// print("Lose: " + lines[currLine]);

						tasks[taskIndex].SetLoseResp(ii, lines[currLine].Trim());
						currLine++;
						
					}

				}

				else {
					// print(currLine);
				}

				taskLine++;

			}
			taskIndex++;

			taskLine = 0;

		}

		return tasks;

	}
}
