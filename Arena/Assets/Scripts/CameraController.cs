using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	private Transform _target;
	private Vector3 _offsetVelocity;
	private float _speed = 5f;

	public void SetTarget(Transform target){
		_target = target;
	}

	void Start(){
		SetTarget(transform);
		_offsetVelocity = transform.position;
	}

    void Update()
    {
			Vector3 xzVelocity = new Vector3(_target.position.x,_target.position.y,transform.position.z);
			_offsetVelocity = Vector3.MoveTowards(_offsetVelocity, xzVelocity, _speed);
			transform.position = _offsetVelocity;
	}
		
}
