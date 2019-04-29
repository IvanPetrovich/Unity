using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Enemy : NetworkBehaviour
{
	[SerializeField]
	protected Transform pointA, pointB;
	[SerializeField]
	protected Transform target;
	[SerializeField]
	protected float speedMin, speedMax;
	protected float speed;
	protected GameLogic _gameLogic;
	public int health;
	public Animator _anim;

	public virtual void Init(){
		_anim	 = GetComponentInChildren<Animator>();
		_gameLogic = FindObjectOfType<GameLogic>();
		_gameLogic.AddEnemy();
	}

	public void Start(){
		Init();
		speed = Random.Range(speedMin,speedMax);
	}

	public virtual void DestroyEnemy(float sec){
		_gameLogic.RemoveEnemy();
		Destroy(this.gameObject,sec);
	}
	
	
	public virtual void SetTargetPoints(Transform pA, Transform pB){
		pointA = pA;
		pointB = pB;
		target = pA;
	}
	
	public virtual void Update(){
		if(_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || _anim.GetCurrentAnimatorStateInfo(0).IsName("Die")){// && _anim.GetBool("inCombat") == false){
			return;
		}
		Movement();
	}
	
	public virtual void Movement(){
		Flip();
		if(transform.position == pointA.position){
			target = pointB;
			_anim.SetTrigger("Idle");
		}else if(transform.position == pointB.position){
			target = pointA;
			_anim.SetTrigger("Idle");
		}
		
		if(Vector2.Distance(transform.position, target.position)>0.5f){
			transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
			_anim.SetFloat("Move",speed);
		}else{
			StopChangeTarget();
		}
	}
	
	public virtual void Attack(){
		_anim.SetTrigger("Attack");
	}
	
	IEnumerator SetTarget(Transform trgt){
		yield return new WaitForSeconds(2f);
		target = trgt;
	}
	
	private void Flip(){
		if(transform.position.x < target.transform.position.x){
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
		}else{
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Enemy")){
			Debug.Log("EnemyTrigger " + other.name);
			if(other.gameObject.transform.position.x < transform.position.x*transform.localScale.x ){
				StopChangeTarget(false);
			}	
		}
	}
	
	private void StopChangeTarget(bool isCoroutine = true){
		_anim.SetFloat("Move",0f);
		speed = Random.Range(speedMin,speedMax);
		if(target == pointA){
			if(isCoroutine){
				StartCoroutine(SetTarget(pointB));
			}else{
				target = pointB;
			}	
		}else{
			if(isCoroutine){
				StartCoroutine(SetTarget(pointA));
			}else{
				target = pointA;
			}			
		}
		
	}
}
