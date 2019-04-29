using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class SnowBall : NetworkBehaviour
{
	[SerializeField]
	private GameObject _owner;
	private Rigidbody2D _rb2D;
	private Vector2 _velocity;
	private float _direction; 
	private Animator _anim;
	private float _speed = 6f;
	private float _playerSpeed;

	void Start(){
		_rb2D = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(_owner == null){
			_owner = this.gameObject;
		}
		
		if((other.CompareTag("Enemy") && _owner.CompareTag("Player")) 
		|| (other.CompareTag("Player") && _owner.CompareTag("Enemy"))
		||	other.CompareTag("Block")
		){
			_anim.SetTrigger("Break");
			_speed = 0f;
			_velocity = Vector2.zero;
			Destroy(gameObject,1f);
			
			IDamageable hit = other.GetComponent<IDamageable>();
			if(hit!=null){
				hit.Damage(_owner);
			}
		}

		if(other.gameObject.layer == 8)
		{
			_anim.SetTrigger("Break");
			_speed = 0f;
			_velocity = Vector2.zero;
			Destroy(gameObject,1f);		
		}
	}
	
	void Update(){
		_rb2D.velocity = new Vector2(_velocity.x + _speed * -_direction,0);//_velocity.y);
	}
	
	public void SetVelocity(Vector2 velocity){
		_velocity = velocity;
	}
	
	public void SetDirection(float direction){
		_direction = direction;
	}
	
	public void SetPlayerSpeed(float playerSpeed){
		_playerSpeed = playerSpeed;
	}
	
	public void SetOwner(GameObject owner){
		_owner = owner;
	}

	[Command]
	public void CmdBreak(){
		RpcBreak();
	}
	
	[ClientRpc]
	void RpcBreak(){
		_anim.SetTrigger("Break");
		_speed = 0f;
		Destroy(gameObject,1f);
	}
	
	[Command]
	public void CmdDestroy(){
		RpcDestroy();
	}
	
	[ClientRpc]
	void RpcDestroy(){
		Destroy(gameObject,1f);
	}

}
