using UnityEngine;
using System.Collections;

public class EtiquetteEnable : CloseEvent {
	
	public GameObject ettiquetteWindow;

	public override void StartEvent () {
		
		GameObject win;

		win = (GameObject) Instantiate (ettiquetteWindow, new Vector2(0f, 0f), transform.rotation);

		win.name = "Arbeau Pop-up Window";
		win.transform.SetParent(GameObject.Find("Canvas").transform, false);

	}
}