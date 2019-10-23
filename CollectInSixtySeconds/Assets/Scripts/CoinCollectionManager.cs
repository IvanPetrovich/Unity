using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectionManager : MonoBehaviour
{
	
	[SerializeField]
	private GameObject _coin;
	[SerializeField]
	private int _coinCount = 10;
	[SerializeField]
	private GameObject _startPosition;
	[SerializeField]
	private float _spawnRange = 10;
	
	void Start()
	{
		transform.position = _startPosition.transform.position;
	}

	public void DestroyCoins()
	{
		foreach(Transform child in transform)
		{
			Destroy(child.gameObject);
		}
	}

	public void CreateCoins()
	{
		for (int i=0; i<_coinCount;i++)
		{
			CreateOneCoin();
		}
	}

	public void CreateOneCoin()
	{
		float startX = _startPosition.transform.position.x;
		float startZ = _startPosition.transform.position.z;
		float x = Random.Range(startX - _spawnRange,startX + _spawnRange);
		float y = 1.5f;
		float z = Random.Range(startZ - _spawnRange, startZ + _spawnRange);
		
		Vector3 newPosition = new Vector3(x,y,z);
		
		GameObject newCoin = Instantiate(_coin, newPosition, Quaternion.identity, transform);
		CoinObject co = newCoin.GetComponent<CoinObject>();
	}

}
