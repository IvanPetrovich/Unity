using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AttackArea : NetworkBehaviour
{
	[SerializeField]
	private Animator _anim;
	[SerializeField]
	private GameObject _startRay;
	[SerializeField]
	private float _length;
	private float _direction;
	private Enemy _thisEnemy;
	
	void Start(){
		_anim	 = transform.parent.GetComponentInChildren<Animator>();
		_thisEnemy = transform.parent.gameObject.GetComponent<Enemy>();
	}

	void Update(){
		_direction = transform.parent.transform.localScale.x;
		
		if(IsPlayerNear()){
			_thisEnemy.Attack();
		}
	}
	private void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			_thisEnemy.Attack();
		}
	}

	bool IsPlayerNear(){
		Debug.DrawRay(_startRay.transform.position, Vector2.left*_direction* _length, Color.green);
		RaycastHit2D[] hit = Physics2D.RaycastAll(_startRay.transform.position, Vector2.left*_direction, _length);
		if (hit.Length > 0){
			foreach (RaycastHit2D rch2D in hit){
				if(rch2D.collider.gameObject.CompareTag("Player")){
					return true;
				}
			}	
		}
		return false;
	}
}
