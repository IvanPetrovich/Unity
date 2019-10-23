using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
	private GameObject _player;
	private Vector3 _offset;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player");
		_offset = transform.position - _player.transform.position;
    }
    
    void Update()
    {
        transform.position = _player.transform.position + _offset;
    }
}
