using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovemenArea : MonoBehaviour
{
	[SerializeField]
	private Transform _targetA;
	[SerializeField]
	private Transform _targetB;
	[SerializeField]
	private GameObject _enemyPrefab;
	[SerializeField]
	private GameObject[] _bonusPrefabs;
	private float _spawnTime = 5f;
	private float _time = 0f;
	private bool _canSpawn = true;
	private GameLogic _gameLogic;
	private bool _hasBonus = false;
	
	void Start(){
		_gameLogic = FindObjectOfType<GameLogic>();
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Enemy")){
			other.gameObject.transform.parent = this.gameObject.transform;
			Enemy enm = other.GetComponent<Enemy>();
			if(enm!=null){
				enm.SetTargetPoints(_targetA,_targetB);
			}
		}
	}
	
	void Update(){
		
		if(_enemyPrefab != null){
			_canSpawn = true;
			_hasBonus = false;
			foreach (Transform child in transform){
				if(child.CompareTag("Enemy")){
					_canSpawn = false;
				}
				
				if(child.gameObject.name == "Bonus"){
					_hasBonus = true;
				}
				
			}
			if(_canSpawn && _gameLogic.CanSpawnEnemy()){
				_time+=Time.deltaTime;
				if(_time > _spawnTime){
					
					GameObject enemy = Instantiate(_enemyPrefab,new Vector2(this.gameObject.transform.position.x,this.gameObject.transform.position.y + 1f),Quaternion.identity);
					if(!_hasBonus){
						
						int bonusType = Random.Range(0,2);
						
						GameObject bonus = Instantiate(_bonusPrefabs[bonusType],new Vector2(this.gameObject.transform.position.x,this.gameObject.transform.position.y + 1f),Quaternion.identity);
						bonus.name = "Bonus";
						_hasBonus = true;
						
					}
					Debug.Log("Can spawn " + name);
					
					_canSpawn = false;
					_time = 0f;
				}
			}
		}
		
	}
}
