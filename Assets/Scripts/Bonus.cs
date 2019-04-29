using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
	[SerializeField]
	private int _bonusID;

	private void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			Player player = other.GetComponent<Player>();
			if(player != null){
				if(_bonusID == 0){
					player.SetSpeedUp(true);
				}else if(_bonusID == 1){
					player.SetArmor(true);
				}
			}
			Destroy(this.gameObject);
		}
	}

}
