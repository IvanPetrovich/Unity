using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	[SerializeField]
	private GameObject _hitSprite;
	private Collider2D _coll;
	private Animator _hitAnimator;
	private bool _canHit;
	
	void Start(){
		_coll = gameObject.GetComponent<Collider2D>();
		_hitAnimator = _hitSprite.gameObject.GetComponent<Animator>();
		_canHit = true;
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if((other.CompareTag("Enemy") || other.CompareTag("Block")) && _canHit){
			IDamageable hit = other.GetComponent<IDamageable>();
			if(hit!=null){
				hit.Damage(this.gameObject);
			}
			_canHit = false;
			_hitSprite.transform.localPosition = new Vector3(_coll.offset.x,_coll.offset.y,0);
			_hitSprite.SetActive(false);
			_hitSprite.SetActive(true);
			StartCoroutine(CanHitRoutine());
		}
	}

	IEnumerator CanHitRoutine(){
		yield return new WaitForSeconds(0.2f);
		_canHit = true;
	}
	
}
