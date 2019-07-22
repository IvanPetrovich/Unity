using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
	private Touch touch_0;
	private Touch touch_1;
	
	private Vector2 startPosition;
	private Vector2 currentPosition;
	private Vector2 direction;
	private Vector3 target;
	private bool touched = false;
	private bool autoMove = false;
	private float autoMovingSpeed = 7f;
	
	private float movingRate = 0.001f;
	
	private float minZ = -21.0f;
	private float maxZ =  15.0f;
	private float minX = -40.0f;
	private float maxX =  40.0f;
	private float minH =  1.0f;
	private float maxH =  6.0f;
	
	private AppLogic appLogic;
	
	void Start(){
		appLogic		 = FindObjectOfType<AppLogic>();
	}
	
    void Update()
    {
		if(!appLogic.GetControllersEnabled()){
			return;
		}
        if(Input.touchCount == 1){ // moving
			
			touch_0 = Input.GetTouch(0);
			
			if(touch_0.phase == TouchPhase.Began){
				touched = true;
				startPosition = touch_0.position;
				direction = Vector2.zero;
			}
			else if(touch_0.phase == TouchPhase.Ended){
				touched = false;
			}else if(touch_0.phase == TouchPhase.Moved){
				direction = touch_0.deltaPosition;
			}
			
		}else if(Input.touchCount == 2){// scaling
			Touch touch_0 = Input.GetTouch(0);
			Touch touch_1 = Input.GetTouch(1);
			
			Vector2 touch_0_prevPos = touch_0.position - touch_0.deltaPosition;
			Vector2 touch_1_prevPos = touch_1.position - touch_1.deltaPosition;
			
			float prevTouchMagnitude = (touch_0_prevPos	 	- touch_1_prevPos).magnitude;
			float touchMagnitude	 = (touch_0.position	- touch_1.position).magnitude;
			
			float deltaMagnitude = prevTouchMagnitude - touchMagnitude;
			
			transform.position = transform.position + Vector3.up * deltaMagnitude * 0.003f * transform.position.y;
			transform.position = transform.position + Vector3.forward * deltaMagnitude * 0.003f * - transform.position.y;
			
			touched = true;
			
		}
		
		if(touched){
			transform.position = new Vector3(transform.position.x - direction.x * movingRate * transform.position.y, transform.position.y, transform.position.z - direction.y * movingRate * transform.position.y);
			
			float x = Mathf.Clamp(transform.position.x, minX, maxX);
			float y = Mathf.Clamp(transform.position.y, minH, maxH);
			float z = Mathf.Clamp(transform.position.z, minZ, maxZ);
			
			transform.position = new Vector3(x,y,z);
		}
		
		if(autoMove){
			transform.position = Vector3.MoveTowards(transform.position, target, autoMovingSpeed * Time.deltaTime);
			if(transform.position == target){				
				autoMove = false;				
			}
		}			
    }
	
	public void GoToObject(Vector3 trgt){		
		target = new Vector3(trgt.x, transform.position.y, trgt.z - transform.position.y);
		autoMove = true;
	}	
}
