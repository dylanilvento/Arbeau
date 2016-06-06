using UnityEngine;
using System.Collections;

public class District {

	static int powerRes = 6;
	static int suppRes = 5;
	static int entRes = 5;
	static int closedCnt = 0;

	public string name;
	int power = 0;
	int suppression = 0;
	int entertainment = 0;
	int unrest = 2;

	bool closed = false;

	public District (string name) {
		this.name = name;
	}

	public bool DecrementPower () {
		if (power == 0) {
			return false;
		}
		else if (power == 1 || power == 2) {
			power--;
			IncrementUnrest();
			return true;
		}

		else if (power == 3 || power == 4) {
			power--;
			return true;
		}

		else if (power == 5) {
			power--;
			IncrementUnrest();
			return true;
		}

		else {
			return false;
		}
	}

	public bool IncrementPower () {
		if (power == 0 || power == 1) {
			power++;
			DecrementUnrest();
			return true;
		}
		else if (power == 2 || power == 3) {
			power++;
			//IncrementUnrest();
			return true;
		}

		else if (power == 3 || power == 4) {
			power++;
			DecrementUnrest();
			return true;
		}

		else if (power == 5) {
			IncrementReservePower();
			return true;
		}

		else {
			return false;
		}
	}

	public bool DecrementSuppression () {
		if (suppression == 0) {
			return false;
		}
		else if (suppression == 1) {
			suppression--;
			IncrementUnrest();
			return true;
		}

		else if (suppression == 2) {
			suppression--;
			IncrementUnrest();
			return true;
		}

		else if (suppression == 3 && IncrementPower()) {
			suppression--;
			IncrementUnrest();
			return true;
		}

		else {
			return false;
		}
	}

	public bool IncrementSuppression () {
		if (suppression == 0) {
			suppression++;
			DecrementUnrest();
			return true;
		}
		else if (suppression == 1) {
			suppression++;
			DecrementUnrest();
			//IncrementUnrest();
			return true;
		}

		else if (suppression == 2 && DecrementPower()) {
			suppression++;
			DecrementUnrest();
			//IncrementUnrest();
			return true;
		}

		else if (suppression == 3) {
			IncrementReserveSuppression();
			return true;
		}

		else {
			return false;
		}
	}

	public bool DecrementEntertainment () {
		if (entertainment == 0) {
			return false;
		}
		else if (entertainment == 1 && IncrementPower()) {
			entertainment--;
			IncrementUnrest();
			IncrementUnrest();
			return true;
		}

		else if ((entertainment == 2 || entertainment == 3) && IncrementPower()) {
			suppression++;
			IncrementUnrest();
			IncrementUnrest();
			//IncrementUnrest();
			return true;
		}

		else {
			return false;
		}
	}

	public bool IncrementEntertainment () {
		if (entertainment < 3 && DecrementPower()) {
			entertainment++;
			DecrementUnrest();
			DecrementUnrest();
			return true;
		}
		else {
			return false;
		}
		
	}


	public bool IncrementResource (string resource) {
		if (resource.Equals("Power")) {
			return IncrementPower();
		}
		else if (resource.Equals("Suppression")) {
			return IncrementSuppression();
		}

		else if (resource.Equals("Entertainment")) {
			return IncrementEntertainment();
		}
		else {
			return false;
		}
	}

	public bool DecrementResource (string resource) {
		if (resource.Equals("Power")) {
			return DecrementPower();
		}
		else if (resource.Equals("Suppression")) {
			return DecrementSuppression();
		}

		else if (resource.Equals("Entertainment")) {
			return DecrementEntertainment();
		}
		else {
			return false;
		}
	}

	public void IncrementUnrest() {
		unrest++;
	}

	public void DecrementUnrest() {
		//TEST
		/*if (unrest > 0) {
			unrest--;
		}
		else {
			unrest = 0;
		}*/
		unrest--;
	}

	public void IncrementReservePower() {
		powerRes++;
	}

	public void DecrementReservePower() {
		powerRes--;
	}

	public void IncrementReserveSuppression() {
		suppRes++;
	}

	public void DecrementReserveSuppression() {
		suppRes--;
	}

	public void IncrementReserveEntertainment() {
		entRes++;
	}

	public void DecrementReserveEntertainment() {
		entRes--;
	}

	public int GetReservePower () {
		return powerRes;
	}

	public int GetReserveSuppression () {
		return suppRes;
	}

	public int GetReserveEntertainment () {
		return entRes;
	}

	public int GetResource (string resource) {
		if (resource.Equals("Power")) {
			return power;
		}
		else if (resource.Equals("Suppression")) {
			return suppression;
		}

		else if (resource.Equals("Entertainment")) {
			return entertainment;
		}
		else {
			return 0;
			//print("Resource not found.");
		}
	}

	public int GetUnrest () {
		return unrest;
	}

	public void CloseDistrict () {
		powerRes += power / 2;
		suppRes += suppression / 2;
		entRes += entertainment / 2;

		power = 0;
		suppression = 0;
		entertainment = 0;

		closed = true;
		closedCnt++;
	}

	public bool GetClosed () {
		return closed;
	}

	public int GetClosedCount () {
		return closedCnt;
	}

}
