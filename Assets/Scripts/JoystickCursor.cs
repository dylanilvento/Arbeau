using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class JoystickCursor : MonoBehaviour {

	// Use this for initialization
	public XboxController playerNumber = XboxController.First;
	public float walkingSpeed = 7.0f;

	public GameObject mainCamera;
	Camera camera;

	void Start () {
		camera = mainCamera.GetComponent<Camera>();

		if (XCI.GetNumPluggedCtrlrs() > 0) {
			Cursor.visible = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 newPosition = transform.position;

		float axisX = Input.GetAxis("Horizontal");
		float axisY = Input.GetAxis("Vertical");
		
        // float axisX = XCI.GetAxis(XboxCtrlrInput.XboxAxis.LeftStickX);
        // float axisY = XCI.GetAxis(XboxCtrlrInput.XboxAxis.LeftStickY);

        float newPosX = newPosition.x + (axisX * walkingSpeed * Time.deltaTime);
        float newPosY = newPosition.y + (axisY * walkingSpeed * Time.deltaTime);

        newPosition = new Vector2(newPosX, newPosY);
        transform.position = newPosition;

        // print (axisX);

        RaycastWorldUI();
        RaycastMoveHandler();
	}

	void FixedUpdate () {
		transform.SetAsLastSibling();
	}

	void RaycastMoveHandler() {
		PointerEventData pointerData = new PointerEventData(EventSystem.current);
		AxisEventData axisData = new AxisEventData(EventSystem.current);

		pointerData.position = transform.position;
        axisData.moveVector = transform.position;//camera.WorldToScreenPoint(transform.position);//Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        // print(results.Count);
        if (results.Count > 1) {
        	// print("true");

        	for (int ii = 1; ii < 4 && ii < results.Count; ii++) {
        		ExecuteEvents.Execute(results[ii].gameObject, pointerData, ExecuteEvents.pointerEnterHandler);
        	}



        	//only need to hit object at index 1 cause that's the highest up object
        	// print(results[1].gameObject.name + ", " + results[1].index);
        	// ExecuteEvents.Execute(results[1].gameObject, axisData, ExecuteEvents.moveHandler);
        	// ExecuteEvents.Execute(results[1].gameObject, pointerData, ExecuteEvents.pointerEnterHandler);
        	// ExecuteEvents.Execute(results[1].gameObject, pointerData, ExecuteEvents.pointerExitHandler);
        		// ExecuteEvents.Execute(results[1].gameObject, pointerData, ExecuteEvents.beginDragHandler);
        		// ExecuteEvents.Execute(results[1].gameObject, pointerData, ExecuteEvents.initializePotentialDrag);
        		
        	// } 
        }
	}

	void RaycastWorldUI(){
	    if(XCI.GetButtonDown(XboxButton.A) ){
	    	print(transform.position);
	        PointerEventData pointerData = new PointerEventData(EventSystem.current);

	        pointerData.position = transform.position;//camera.WorldToScreenPoint(transform.position);//Input.mousePosition;

	        List<RaycastResult> results = new List<RaycastResult>();
	        EventSystem.current.RaycastAll(pointerData, results);

	        print(results.Count);
	        if (results.Count > 1) {
	        	// print("true");

	        	// foreach (RaycastResult result in results) {
	        	for (int ii = 1; ii < 4 && ii < results.Count; ii++) {
	        	//only need to hit object at index 1 cause that's the highest up object
	        		// print(results[1].gameObject.name + ", " + results[1].index);
	        		ExecuteEvents.Execute(results[ii].gameObject, pointerData, ExecuteEvents.pointerDownHandler);
	        		// ExecuteEvents.Execute(results[ii].gameObject, pointerData, ExecuteEvents.pointerClickHandler);
	        		ExecuteEvents.Execute(results[ii].gameObject, pointerData, ExecuteEvents.beginDragHandler);
	        		ExecuteEvents.Execute(results[ii].gameObject, pointerData, ExecuteEvents.initializePotentialDrag);
	        		
	        	} 
	        }
        }

        else if(XCI.GetButtonUp(XboxButton.A)) {
        	print(transform.position);
	        PointerEventData pointerData = new PointerEventData(EventSystem.current);

	        pointerData.position = transform.position;//camera.WorldToScreenPoint(transform.position);//Input.mousePosition;

	        List<RaycastResult> results = new List<RaycastResult>();
	        EventSystem.current.RaycastAll(pointerData, results);

	        // print(results.Count);
	        if (results.Count > 1) {
	        	// print("true");

	        	// foreach (RaycastResult result in results) {
	        	for (int ii = 1; ii < 4 && ii < results.Count; ii++) {
	        		ExecuteEvents.Execute(results[ii].gameObject, pointerData, ExecuteEvents.pointerUpHandler);
	        		ExecuteEvents.Execute(results[ii].gameObject, pointerData, ExecuteEvents.pointerClickHandler);
	        		ExecuteEvents.Execute(results[ii].gameObject, pointerData, ExecuteEvents.endDragHandler);
	        	}
	        	// } 
	        }
        }
    }
}
