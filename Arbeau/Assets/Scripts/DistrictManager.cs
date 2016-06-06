using UnityEngine;
using System.Collections;

public class DistrictManager : MonoBehaviour {

	public District[] districts;

	// Use this for initialization
	void Start () {
		districts = new District[4];

		districts[0] = new District("h1");
		districts[1] = new District("h2");
		districts[2] = new District("i1");
		districts[3] = new District("i2");

		/*districts[0].IncrementUnrest();
		districts[0].IncrementUnrest();
		districts[0].IncrementUnrest();
		//districts[0].IncrementUnrest();
		districts[1].IncrementUnrest();
		districts[1].IncrementUnrest();
		districts[1].IncrementUnrest();*/

	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
