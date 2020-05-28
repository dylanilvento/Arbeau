using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Diagnostics;
using System;

public class QuitGame : MonoBehaviour {
	Button button;
	
	// Use this for initialization
	void Start () {
		button = GetComponent<Button>();
		button.onClick.AddListener(ExitGame);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ExitGame () {
		try {
			Process myProcess = new Process();
	        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
	        myProcess.StartInfo.CreateNoWindow = false;
	        // myProcess.StartInfo.UseShellExecute = false;
	        myProcess.StartInfo.FileName = "C:\\Users\\Dylan\\Desktop\\BnP\\Launcher\\Launcher.exe";
	        myProcess.Start();
	    }
	    catch (Exception e) {
            Console.WriteLine(e.Message);
        }
        
		Application.Quit();
	}

}