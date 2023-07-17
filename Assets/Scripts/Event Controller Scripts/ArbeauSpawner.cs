using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class ArbeauSpawner : MonoBehaviour
{
    public bool useSpawner,
        fastSpawn;
    public GameObject[] arbeauWin;
    Camera camera;
    bool lockWindow = false;

    GameManager gameMan;

    Dictionary<int, float> spawnMap = new Dictionary<int, float>();
    public float[] spawnTimes = new float[10];

    GameObject currWin;

    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameMan = GetComponent<GameManager>();

        for (int ii = 0; ii < spawnTimes.Length; ii++)
        {
            int roundNum = ii + 1;
            spawnMap.Add(roundNum, spawnTimes[ii]);
        }

        if (useSpawner)
            StartCoroutine("SpawnRandomArbeau");
    }

    // Update is called once per frame
    void Update() { }

    //called by GameManager.cs
    public void SetLockWindow(bool val)
    {
        lockWindow = val;
    }

    IEnumerator SpawnRandomArbeau()
    {
        while (true)
        {
            //KEEP THIS
            //float time = UnityEngine.Random.Range(3f, 40f);
            //yield return new WaitForSeconds(time);

            //first you need the RectTransform component of your canvas

            //use this only for testing
            //yield return new WaitForSeconds(0.5f);
            yield return new WaitForSeconds(DetermineWait(gameMan.GetRound()));
            if (!lockWindow)
                StartCoroutine("SpawnWindow");
        }
    }

    float DetermineWait(int round)
    {
        float t = 15f;
        if (fastSpawn)
            return 0.5f;

        spawnMap.TryGetValue(round, out t);

        float result = UnityEngine.Random.Range(
            t - UnityEngine.Random.Range(t - (t - 1), (t / 2)),
            t
        );
        return result;
    }

    void SpawnWindow()
    {
        if (gameMan.GetArbeauOff())
            return;

        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();

        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen,
        //whereas WorldToViewPortPoint treats the lower left corner as 0,0.
        //Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

        CanvasScaler scaler = GameObject.Find("Canvas").GetComponent<CanvasScaler>();

        int index = 0;

        GameObject win;

        // Demo version
        win = (GameObject)Instantiate(
            arbeauWin[UnityEngine.Random.Range(0, 5)],
            new Vector2(0f, 0f),
            transform.rotation
        );

        win.name = "Arbeau Pop-up Window";
        win.transform.SetParent(GameObject.Find("Canvas").transform, false);

        currWin = win;

        if (GameManager.Instance.usingController)
        {
            win.transform.position = GameObject.Find("Joystick Cursor").transform.position;
            return;
        }

        Rect winRect = win.transform.GetChild(0).gameObject.GetComponent<RectTransform>().rect;
        RectTransform winRectTrans = win.transform
            .GetChild(0)
            .gameObject.GetComponent<RectTransform>();

        float winX =
            Input.mousePosition.x * scaler.referenceResolution.x / Screen.width
            - (scaler.referenceResolution.x / 2f);
        float winY =
            Input.mousePosition.y * scaler.referenceResolution.y / Screen.height
            - (scaler.referenceResolution.y / 2f);

        float winHeight,
            winWidth,
            left,
            right,
            top,
            bottom;

        winWidth = winRect.width;
        winHeight = winRect.height;

        if (winHeight == 0f)
            winHeight = win.transform
                .GetChild(0)
                .GetChild(0)
                .gameObject.GetComponent<Text>()
                .preferredHeight;
        if (winHeight > 1000f)
            winHeight /= 10f;

        if (Screen.width >= 754f)
        {
            left = (-1) * (Screen.width - (winWidth / DeriveLeftDivisor()));
        }
        else
        {
            left = (-1) * (Screen.width - (winWidth / 10f));
        }

        if (Screen.width >= 754f)
        {
            right = (Screen.width - (winWidth / DeriveRightDivisor()));
        }
        else
        {
            right = (Screen.width - (winWidth / 10f));
        }

        if (Screen.height >= 337f && Screen.height <= 650f)
        {
            top = Screen.height - (winHeight / DeriveTopDivisor());
        }
        else if (Screen.height > 650f)
        {
            top = (Screen.height - (winHeight / 0.7f));
        }
        else
        {
            top = (Screen.height - (winHeight / 10f));
        }

        if (Screen.height >= 337f && Screen.height <= 650f)
        {
            bottom = (-1) * (Screen.height - (winHeight / DeriveBottomDivisor()));
        }
        else if (Screen.height > 650f)
        {
            bottom = (-1) * (Screen.height - (winHeight / 0.5f));
        }
        else
        {
            bottom = (-1) * (Screen.height - (winHeight / 10f));
        }

        //Left side
        if (winX < left)
        {
            print("winX (" + winX + ") < left (" + left + ")");
            winX = left;
        }
        //right side
        else if (winX > right)
        {
            winX = right;
        }

        //bottom
        if (winY < bottom)
        {
            winY = bottom;
        }
        //top
        else if (winY > top)
        {
            winY = top;
        }

        Vector2 newPos = new Vector2(winX, winY);
        win.GetComponent<RectTransform>().anchoredPosition = newPos;
    }

    float DeriveLeftDivisor()
    {
        float divisor = 2.5f - ((Screen.width - 754f) * 0.0028f);

        return divisor;
    }

    float DeriveRightDivisor()
    {
        float divisor = 2.45f - ((Screen.width - 754f) * 0.0028f);

        return divisor;
    }

    float DeriveTopDivisor()
    {
        float divisor = 2.55f - ((Screen.height - 337f) * 0.0062f);

        return divisor;
    }

    float DeriveBottomDivisor()
    {
        float divisor = 2f - ((Screen.height - 337f) * 0.0062f);

        return divisor;
    }
}
