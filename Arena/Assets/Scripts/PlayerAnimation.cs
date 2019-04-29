using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAnimation : NetworkBehaviour
{
	[SerializeField]
	private Animator _playerAnimation;
	
	public void Move(float move){
		CmdMove(Mathf.Abs(move));
	}
	
	[Command]
	public void CmdMove(float move){
		RpcMove(move);
	}
	
	[ClientRpc]
	void RpcMove(float move){
		_playerAnimation.SetFloat("Move",move);
	}
	
	public void Attack(){
		CmdAttack();		
	}
	
	[Command]
	public void CmdAttack(){
		RpcAttack();
	}
	
	[ClientRpc]
	void RpcAttack(){
		_playerAnimation.SetTrigger("Attack");
	}
	
	public void Fire(){
		CmdFire();
	}
	
	[Command]
	public void CmdFire(){
		RpcFire();
	}
	
	[ClientRpc]
	void RpcFire(){
		_playerAnimation.SetTrigger("Fire");
	}
	
	public void Die(){
		CmdDie();
	}

	[Command]
	public void CmdDie(){
		RpcDie();
	}

	[ClientRpc]
	void RpcDie(){
		_playerAnimation.SetTrigger("Die");
	}
	
}
