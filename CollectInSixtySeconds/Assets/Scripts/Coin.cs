using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	[SerializeField]
	private int _value;
	private GameLogic _gameLogic;

    void Start()
    {
		_gameLogic = FindObjectOfType<GameLogic>();
        _value = 1;
    }

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player")){
			_gameLogic.AddScore(_value);
			Destroy(transform.parent.gameObject);
		}

		if(other.GetComponent<CoinObject>() != null)
		{
			Destroy(transform.parent.gameObject);
		}
	}
	
}
