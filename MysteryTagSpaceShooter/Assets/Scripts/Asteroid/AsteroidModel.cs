		using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class AsteroidModel
{
	public ReactiveProperty<Vector3> pos {get; set;}
	public float speed;
	public ReactiveProperty<int> health {get; set;}

	private float minX = -6.0f;
	private float maxX =  6.0f;
	private float yPos =  8.0f;

	private float minSpeed = 2.0f;
	private float maxSpeed = 4.0f;


	public AsteroidModel()
	{
		Initialize();
	}
	
	public void Hit()
	{
		health.Value--;
	}
	
	public void Initialize()
	{
		speed = Random.Range(minSpeed,maxSpeed);
		if(health == null)
		{
			health = new ReactiveProperty<int>(1);
		}else{
			health.Value = 1;
		}
		
		float xPos = Random.Range(minX, maxX);
		
		if(pos == null){
			pos = new ReactiveProperty<Vector3>(new Vector3(xPos, yPos, 0));
		}else{
			pos.Value = new Vector3(xPos, yPos, 0);
		}			
	}
}
