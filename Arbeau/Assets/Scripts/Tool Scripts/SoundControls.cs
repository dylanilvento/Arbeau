using UnityEngine;
using System.Collections;

public class SoundControls : MonoBehaviour {
	
	public static SoundControls instance;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SoundFadeOut (AudioSource audio, float divisor) {
		StartCoroutine(SoundOutFade(audio, divisor));
	}

	public IEnumerator SoundOutFade (AudioSource audio, float divisor) {
		float currentVolume = audio.volume;
		print("test");

		float decrement = currentVolume / divisor;
		//yield return new WaitForSeconds(0.5f);

		while (currentVolume > 0.01f) {
			currentVolume -= decrement;
			//text.color = new Color(text.color.r, text.color.g, text.color.b, currentAlpha);
			audio.volume = currentVolume;
			yield return new WaitForSeconds(0.05f);
		}

		audio.Stop();
	}
}
