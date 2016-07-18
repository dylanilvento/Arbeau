using UnityEngine;
using System.Collections;

public class Task {
		string beforeDist, afterDist, resource, goal, difficulty;
		string district = null;
		string[] extraContext;
		string[] winResp;
		string[] loseResp;
		int score;
		
		public Task (string resource) {
			//print("Assigned resource: " + resource);
			this.resource = resource;

		}

		public void SetGoal (string goal) {
			this.goal = goal;
		}

		public void SetBeforeDist(string beforeDist) {
			// print(beforeDist);
			this.beforeDist = beforeDist;
		}

		public void SetAfterDist(string afterDist) {
			this.afterDist = afterDist;
		}

		public void SetExtraContextSize (int size) {
			extraContext = new string[size];
		}

		public void SetContext (int index, string context) {
			extraContext[index] = context;
		}

		public void SetWinRespSize (int size) {
			winResp = new string[size];
		}

		public void SetWinResp (int index, string resp) {
			winResp[index] = resp;
		}

		public void SetLoseRespSize (int size) {
			loseResp = new string[size];
		}

		public void SetLoseResp (int index, string resp) {
			loseResp[index] = resp;
		}

		public void SetDistrict (string dist) {
			district = dist;
		}

		public void SetScore (int score) {
			this.score = score;
		}

		public string GetGoal () {
			//print(goal);
			return goal;
		}

		public string GetBeforeDist() {

			//print(beforeDist);
			return beforeDist;
		}

		public string GetAfterDist() {
			return afterDist;
		}

		public int GetExtraContextSize () {
			return extraContext.Length;
		}

		public string GetContext (int index) {
			return extraContext[index];
		}

		public int GetWinRespSize () {
			return winResp.Length;
		}

		public string GetWinResp (int index) {
			return winResp[index];
		}

		public int GetLoseRespSize () {
			return loseResp.Length;
		}

		public string GetLoseResp (int index) {
			return loseResp[index];
		}

		public string GetResource () {
			//print(resource);
			return resource;
		}

		public string GetDistrict () {
			return district;
		}

		public int GetScore () {
			return score;
		}

}

	
