using UnityEngine;
using System.Collections;

public class TestMute : MonoBehaviour {

	AudioSource audio;
	bool unmuted = false;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space")) {
			if (unmuted)
				audio.mute = true;

			else
				audio.mute = false;

			unmuted = !unmuted;
		}
	}
}
