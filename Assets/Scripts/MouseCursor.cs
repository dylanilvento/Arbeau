using UnityEngine;
using System.Collections;

public class MouseCursor : MonoBehaviour {

	// Use this for initialization
	public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter() {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    void OnMouseExit() {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
