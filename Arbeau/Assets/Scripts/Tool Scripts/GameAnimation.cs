using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameAnimation : MonoBehaviour {

	public static GameAnimation instance;
	//public static GameAnimation;
	void Start() {
		instance = this;
	}

	public void GraphicFadeIn (Image img) {
		//print("got called");
		StartCoroutine(GraphicInFade(img));

	}

	public void GraphicFadeOut (Text text) {
		//print("got called");
		StartCoroutine(GraphicOutFade(text));

	}
		
	IEnumerator GraphicInFade (Image img) {
		float currentAlpha = 0.0f;

		//yield return new WaitForSeconds(0.5f);

		while (currentAlpha < 1.0f) {
			currentAlpha += 0.1f;
			img.color = new Color(img.color.r, img.color.g, img.color.b, currentAlpha);
			yield return new WaitForSeconds(0.05f);
		}
	}

	IEnumerator GraphicOutFade (Text text) {
		float currentAlpha = 1.0f;

		//yield return new WaitForSeconds(0.5f);

		while (currentAlpha > 0.0f) {
			currentAlpha -= 0.1f;
			text.color = new Color(text.color.r, text.color.g, text.color.b, currentAlpha);
			yield return new WaitForSeconds(0.05f);
		}
	}
}