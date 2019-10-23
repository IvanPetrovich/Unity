using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : MonoBehaviour
{
	private bool _isInstantiate;
	private bool _isCollided = false;
	

	void Start()
	{
		_isInstantiate = true;
		StartCoroutine(ResetIsInst());
	}

	public bool IsCollided()
	{
		_isInstantiate = false;
		return _isCollided;
	}

	void OnTriggerEnter(Collider other)
	{
		if(_isInstantiate)
		{
			Destroy(this.gameObject);
		}
	}

	private IEnumerator ResetIsInst()
	{
		yield return new WaitForSeconds(0.1f);
		_isInstantiate = false;
	}
	
}
