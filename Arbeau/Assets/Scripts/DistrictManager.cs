using UnityEngine;
using System.Collections;

public class DistrictManager : MonoBehaviour {

	public District[] districts;

	[Range(0, 6)]
	public int h1InitialUnrest, h2InitialUnrest, i1InitialUnrest, i2InitialUnrest;

	// Use this for initialization
	void Start () {
		districts = new District[4];

		districts[0] = new District("h1");
		districts[1] = new District("h2");
		districts[2] = new District("i1");
		districts[3] = new District("i2");

		for (int ii = 0; ii < h1InitialUnrest; ii++) {
			districts[0].IncrementUnrest();
		}

		for (int ii = 0; ii < h2InitialUnrest; ii++) {
			districts[1].IncrementUnrest();
		}

		for (int ii = 0; ii < i1InitialUnrest; ii++) {
			districts[2].IncrementUnrest();
		}

		for (int ii = 0; ii < i2InitialUnrest; ii++) {
			districts[3].IncrementUnrest();
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
