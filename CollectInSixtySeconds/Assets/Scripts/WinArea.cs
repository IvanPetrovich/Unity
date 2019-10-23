using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinArea : MonoBehaviour
{
    
	private GameLogic _gameLogic;

    void Start()
    {
		_gameLogic = FindObjectOfType<GameLogic>();
    }
	
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			_gameLogic.WinGame();
		}
	}
	
}
