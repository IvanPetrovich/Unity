using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
	private ObjectsPool _pool;
	public GameObject prefab;
	public int startCount;
	public GameObject parent;
	public float delay = 1.0f;
	private float timeElapsed = 0;
	private bool canSpawn = true;

	
	void Start()
	{
		_pool = new ObjectsPool(startCount, prefab, parent);
	}

	public void SetSpawn(bool val)
	{
		canSpawn = val;
	}

	void Update()
	{
		timeElapsed+=Time.deltaTime;
		if (timeElapsed > delay && canSpawn)
		{
			GameObject gObj = _pool.GetObject();
			gObj.SetActive(true);

			AsteroidController aController = gObj.GetComponent<AsteroidController>();
			aController.gameObject.SetActive(true);
			aController.ResetPosition();
			aController.SetGameObjectActive(true);
			
			timeElapsed = 0.0f;
		}
	}
}
