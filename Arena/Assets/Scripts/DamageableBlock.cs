using UnityEngine;
using UnityEngine.Networking;

public class DamageableBlock : NetworkBehaviour, IDamageable
{
	
	public int Health {get; set;}
	[SerializeField]
	private Animator _anim;
	
    void Start(){
		_anim = GetComponent<Animator>();
    }

	public void Damage(GameObject author){
		CmdDamage();
	}
	
	[Command]
	public void CmdDamage(){
		RpcDamage();
	}
	
	[ClientRpc]
	void RpcDamage(){
		_anim.SetTrigger("Hit");
	}
		
}
