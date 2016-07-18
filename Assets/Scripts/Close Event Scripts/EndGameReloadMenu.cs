using UnityEngine;
using System.Collections;

public class EndGameReloadMenu : CloseEvent {

	public override void StartEvent () {
		Application.LoadLevel(0);
	}
}
