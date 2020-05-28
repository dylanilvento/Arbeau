using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class FacilityQuit : CloseEvent {

	public override void StartEvent () {
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
