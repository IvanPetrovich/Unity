using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Wizard : Enemy, IDamageable
{
	[SerializeField]
	private GameObject _snowBall;
	[SerializeField]
	private Transform _snowBallPosition;	
	private GameLogic _gameLogic;
	private bool _canHit = true;
	private Rigidbody2D _rigidbody2D;

	public int Health{get; set;}
	
	public void Damage(GameObject author){
		if(_canHit){
			Health--;
			_canHit = false;
			StartCoroutine(CanHitResetRoutine());

			if(Health==0){
				_anim.SetTrigger("Die");
				if(author.CompareTag("Player")){
					_gameLogic.AddScore(1);
				}
				DestroyEnemy(3f);
			}
		}
	}
	
	IEnumerator CanHitResetRoutine(){
		yield return new WaitForSeconds(0.5f);
		_canHit = true;
	}
	
	public void Fire(){
		GameObject snowBall;
		snowBall = (GameObject)Instantiate(_snowBall, _snowBallPosition.transform.position, Quaternion.identity);
		SnowBall sb = snowBall.GetComponent<SnowBall>();
		sb.SetVelocity(_rigidbody2D.velocity);
		sb.SetDirection(transform.localScale.x);
		sb.SetOwner(transform.gameObject);
	}	
	
	public void Start(){
		base.Init();
		Health = base.health;
		_gameLogic		 = FindObjectOfType<GameLogic>();
		SetTargetPoints(transform, transform);
		_rigidbody2D = GetComponent<Rigidbody2D>();
		speedMin = 1;
		speedMax = 2;
	}
	
	public void Update(){
		base.Update();
	}
	
}
