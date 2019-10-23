using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
    private float _speed = 10;
	[SerializeField]
	private float _maxSpeed = 5f;
	private Animator _anim;
    private FixedJoystick _fixedJoystick;
    private Rigidbody _playerRB;
	[SerializeField]
	private GameObject _startPosition;
	private bool _canMove;

	void Start()
	{
		_fixedJoystick = FindObjectOfType<FixedJoystick>();
		_playerRB = GetComponent<Rigidbody>();
		_anim = GetComponent<Animator>();
	}

	public void SetDefaultPosition()
	{
		transform.position = _startPosition.transform.position;
	}

	public void CanMove(bool val){
		_canMove = val;
	}

    public void FixedUpdate()
    {
		if(_canMove)
		{
			Movement();
		}
		_anim.SetFloat("Force",_playerRB.velocity.magnitude/3);
    }
	
	private void Movement()
	{
        Vector3 direction = Vector3.forward * _fixedJoystick.Vertical + Vector3.right * _fixedJoystick.Horizontal;
		if(_playerRB.velocity.magnitude < _maxSpeed)
		{
			_playerRB.AddForce(direction * _speed * Time.fixedDeltaTime, ForceMode.Impulse);
		}	
		if(_playerRB.velocity.magnitude > 0.1f)
		{	
			transform.LookAt(transform.position + _playerRB.velocity);
		}	
	}
	
}
