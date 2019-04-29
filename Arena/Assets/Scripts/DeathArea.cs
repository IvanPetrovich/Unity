using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
	void OnTriggerStay2D(Collider2D other){
		if(other.CompareTag("Enemy") || other.CompareTag("Player"))
		{
			IDamageable hit = other.GetComponent<IDamageable>();
			if(hit!=null){
				hit.Damage(this.gameObject);
			}
		}		
	}

}
