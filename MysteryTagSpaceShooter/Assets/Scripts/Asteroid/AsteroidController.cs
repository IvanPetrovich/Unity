using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class AsteroidController : MonoBehaviour
{
	public AsteroidView  asteroidView;
	public AsteroidModel asteroidModel;
	private CircleCollider2D _cCollider2D;


	private void Awake()
	{
		_cCollider2D = GetComponent<CircleCollider2D>();
		asteroidModel = new AsteroidModel();

	}
	
	private void Start()
	{
		asteroidModel.pos
		.ObserveEveryValueChanged(pos => pos.Value)
		.Subscribe(posvar => {
			SetPosition(posvar);
		}).AddTo(this);

		asteroidModel.health
		.ObserveEveryValueChanged(x => x.Value)
		.Subscribe(xs => {
			OnHealthChange(xs);
		}).AddTo(this);

		asteroidView.destroyComplete
		.ObserveEveryValueChanged(desComplete => desComplete.Value)
		.Subscribe(desCompleteVar => {
			OnDestroyComplete(desCompleteVar);
		}).AddTo(this);

	}

	private void OnDestroyComplete(bool val)
	{
		if (val == true)
		{
			gameObject.SetActive(false);
		}
	}
	
	private void OnHealthChange(int val)
	{
		if (val <= 0)
		{
			_cCollider2D.enabled = false;
			asteroidView.DestroyIt();
		}
	}
	
	private void SetPosition(Vector3 val)
	{
		transform.position = val;
		if (val.y < -6)
		{
			gameObject.SetActive(false);
		}
	}
	
	public void SetGameObjectActive(bool val)
	{
		asteroidView.SetVisible(val);
	}
	
	public void ResetPosition()
	{
		_cCollider2D.enabled = true;
		asteroidModel.Initialize();
		asteroidView.Initialize();
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{	
			asteroidModel.Hit();
		}	
	}
	
	
	void Update()
	{
		Vector3 newPos = new Vector3(asteroidModel.pos.Value.x, asteroidModel.pos.Value.y - (Time.deltaTime * asteroidModel.speed), asteroidModel.pos.Value.z);
		asteroidModel.pos.Value = newPos;
	}
	
	
}
