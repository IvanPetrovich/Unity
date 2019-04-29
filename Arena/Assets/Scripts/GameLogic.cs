using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
	[SerializeField]
	private int _score;
	[SerializeField]
	private GameMenuLogic _gameML;
	[SerializeField]
	private Camera _mainCamera;
	[SerializeField]
	private Network _network;
	[SerializeField]
	private int _maxEnemies;
	[SerializeField]
	private int _enemiesCount;
	
	
	void Start(){
		_score = 0;
		_gameML.SetTextScore(_score);
		_network = FindObjectOfType<Network>();
		if(_network.isHost){
			_network.StartServer();
		}else{
			_network.JoinServer();
		}
	}
	
	public bool CanSpawnEnemy(){
		return _enemiesCount<_maxEnemies?true:false;
	}
	
	public float GetEnemiesCount(){
		return _enemiesCount;
	}

	public void AddEnemy(){
		_enemiesCount += 1;
	}
	
	public void RemoveEnemy(){
		_enemiesCount -= 1;
	}
	
	public void AddScore(int score){
		_score += score;
		_gameML.SetTextScore(_score);
	}
	
	public Camera GetCamera(){
		return _mainCamera;
	}
 
}
