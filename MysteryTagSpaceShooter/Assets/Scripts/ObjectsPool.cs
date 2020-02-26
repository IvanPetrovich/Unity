using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool
{
	private List<GameObject> _objects = new List<GameObject>();
	private GameObject prefab;
	private GameObject parent;
	
	public ObjectsPool(int startCount, GameObject gObject, GameObject gParent)
	{
		prefab = gObject;
		parent = gParent;
		for(int i=0; i<startCount; i++)
		{
			AddObject();
		}
	}
	
	public GameObject GetObject()
	{
		for(int i=0; i<_objects.Count; i++)
		{
			if(_objects[i].gameObject.activeInHierarchy == false)
			{
				return _objects[i];
			}
		}
		
		AddObject();
		return _objects[_objects.Count - 1];
		
	}


	private void AddObject()
	{
		GameObject newObj = GameObject.Instantiate(prefab);
		newObj.transform.SetParent(parent.transform);
		newObj.SetActive(false);
		_objects.Add(newObj);
		
	}

}