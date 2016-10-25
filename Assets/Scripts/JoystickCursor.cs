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
	}

	void FixedUpdate () {
		transform.SetAsLastSibling();
	}

	void RaycastWorldUI(){
	    if(XCI.GetButtonDown(XboxButton.A) ){
	    	print(transform.position);
	        PointerEventData pointerData = new PointerEventData(EventSystem.current);

	        pointerData.position = transform.position;//camera.WorldToScreenPoint(transform.position);//Input.mousePosition;

	        List<RaycastResult> results = new List<RaycastResult>();
	        EventSystem.current.RaycastAll(pointerData, results);

	        print(results.Count);
	        if (results.Count > 0) {
	        	print("true");

	        	foreach (RaycastResult result in results) {
	        		ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerDownHandler);
	        		ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.beginDragHandler);
	        	} 
	        }
        }

        else if(XCI.GetButtonUp(XboxButton.A)) {
        	print(transform.position);
	        PointerEventData pointerData = new PointerEventData(EventSystem.current);

	        pointerData.position = transform.position;//camera.WorldToScreenPoint(transform.position);//Input.mousePosition;

	        List<RaycastResult> results = new List<RaycastResult>();
	        EventSystem.current.RaycastAll(pointerData, results);

	        print(results.Count);
	        if (results.Count > 0) {
	        	// print("true");

	        	foreach (RaycastResult result in results) {
	        		ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerUpHandler);
	        		ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);
	        		ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.endDragHandler);
	        	} 
	        }
        }
    }
}
