using UnityEngine;
using System.Collections;

public class EttiquetteDisable : CloseEvent {
	
	public GameObject ettiquetteWindow;

	public override void StartEvent () {
		GameObject win;

		win = (GameObject) Instantiate (ettiquetteWindow, new Vector2(0f, 0f), transform.rotation);

		win.name = "Arbeau Pop-up Window";
		win.transform.SetParent(GameObject.Find("Canvas").transform, false);

		EttiquetteSpammer etSpam1 = GameObject.Find("Mark IX Window/Close Button").GetComponent<EttiquetteSpammer>();
		EttiquetteSpammer etSpam2 = GameObject.Find("Mark IX Window/Facility Power Failsafe/Disable Button").GetComponent<EttiquetteSpammer>();
		EttiquetteSpammer etSpam3 = GameObject.Find("Mark IX Window/AI Core/Disable Button").GetComponent<EttiquetteSpammer>();

		etSpam1.SetSpammerOn(true);
		etSpam2.SetSpammerOn(true);
		etSpam3.SetSpammerOn(true);

	}
}
