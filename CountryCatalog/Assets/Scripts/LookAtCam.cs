using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
	private Transform camTransform;
	
    void Start(){
        camTransform = Camera.main.transform;
    }

    void Update(){
		Vector3 target = new Vector3(camTransform.position.x,1f,camTransform.position.z);
        transform.LookAt(target);
    }
}
