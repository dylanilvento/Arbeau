using UnityEngine;
using System.Collections;

public class FacilityQuit : CloseEvent {

	public override void StartEvent () {
		Application.Quit();
	}

}
