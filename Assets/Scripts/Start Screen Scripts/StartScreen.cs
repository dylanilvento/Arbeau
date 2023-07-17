using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class StartScreen : MonoBehaviour
{
    Text wardText;
    Text creditText;
    string[] ward = new string[23];
    string[] credits = new string[12];

    public GameObject startScreen;
    public GameObject arbeauLogo,
        menu;

    public AudioSource hdd,
        typing,
        boot;

    // Use this for initialization
    void Start()
    {
        ward[0] = "   _______________________________________";
        ward[1] = " /                                        \\";
        ward[2] = "|                                         |";
        ward[3] = "|                _________                |";
        ward[4] = "|               |         |               |";
        ward[5] = "|              _/         \\_              |";
        ward[6] = "|             \\    +   +    /             |";
        ward[7] = "|              |  \\+++++/  |              |";
        ward[8] = "|              |  +\\+/+/+  |              |";
        ward[9] = "|              \\  ++v+v++  /              |";
        ward[10] = "|               \\  +++++  /               |";
        ward[11] = "|                \\       /                |";
        ward[12] = "|                 --\\ /--                 |";
        ward[13] = "|                    v                    |";
        ward[14] = "|                         ___    ___      |";
        ward[15] = "|  \\      /     /  /\\    |   \\  |   \\     |";
        ward[16] = "|   \\    /\\    /  /  \\   |   |  |    |    |";
        ward[17] = "|    \\  /  \\  /  /    \\  |--/   |    |    |";
        ward[18] = "|     \\/    \\/  /      \\ |  \\   |___/     |";
        ward[19] = "|                                         |";
        ward[20] = "|                                         |";
        ward[21] = "|                                         |";
        ward[22] = " \\_______________________________________/";

        credits[0] = "\n";
        credits[1] = "[a game by]";
        credits[2] = "\n";
        credits[3] = "[+] sean harrington";
        credits[4] = "\n";
        credits[5] = "[+] kirby martin";
        credits[6] = "\n";
        credits[7] = "[+] mason brown";
        credits[8] = "\n";
        credits[9] = "[+] dylan ilvento";

        wardText = GameObject.Find("Ward Text").GetComponent<Text>();
        creditText = GameObject.Find("Credits").GetComponent<Text>();

        StartCoroutine("StartIntro");
    }

    // Update is called once per frame
    void Update() { }

    IEnumerator StartIntro()
    {
        yield return new WaitForSeconds(1f);

        hdd.PlayScheduled(0f);

        yield return new WaitForSeconds(4f);

        typing.PlayScheduled(0f);
        StartCoroutine(MakeLogo(ward, wardText));

        yield return new WaitForSeconds(4.35f);
        typing.Pause();

        yield return new WaitForSeconds(0.65f);
        typing.PlayScheduled(0f);

        StartCoroutine(MakeLogo(credits, creditText));
        yield return new WaitForSeconds(1.5f);
        typing.Stop();
        yield return new WaitForSeconds(2f);
        GameAnimation.instance.GraphicFadeOut(wardText);
        GameAnimation.instance.GraphicFadeOut(creditText);

        SoundControls.instance.SoundFadeOut(hdd, 20f);

        boot.PlayScheduled(0f);

        yield return new WaitForSeconds(1.5f);

        startScreen.SetActive(true);

        yield return new WaitForSeconds(3f);

        StartCoroutine("MoveLogo");
    }

    IEnumerator MakeLogo(string[] arr, Text uiText)
    {
        for (int ii = 0; ii < arr.Length; ii++)
        {
            arr[ii] += "\n";
        }

        foreach (string line in arr)
        {
            //logo += line + "\n";
            //line += "\n";
            char[] arr2 = line.ToCharArray(0, line.Length);
            //bool skip = false;

            foreach (char c in arr2)
            {
                float pause = 0.0001f;

                if (c.Equals(' '))
                {
                    //print("whitespace");
                    //pause = 0f;
                    uiText.text += c;
                    continue;
                }

                uiText.text += c;
                yield return new WaitForSeconds(pause);
            }
        }
    }

    IEnumerator MoveLogo()
    {
        for (int ii = 0; ii < 40; ii++)
        {
            arbeauLogo.transform.position = new Vector2(
                arbeauLogo.transform.position.x,
                arbeauLogo.transform.position.y + Screen.height / 240f
            );
            yield return new WaitForSeconds(0.01f);
        }

        menu.SetActive(true);
    }
}
