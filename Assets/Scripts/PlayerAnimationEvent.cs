using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{

	private Player player;
	private Wizard wizard;
	
	void Start(){
		player = transform.parent.GetComponent<Player>();
		wizard = transform.parent.GetComponent<Wizard>();
	}
	
	public void Fire(){
		if(player != null){
			player.Fire();
		}
		if(wizard != null){
			wizard.Fire();
		}
	}
	
}
