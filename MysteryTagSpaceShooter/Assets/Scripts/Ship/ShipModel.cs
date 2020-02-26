using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class ShipModel
{
	public ReactiveProperty<float> speed { get; private set;}
	public ReactiveProperty<float> fireRate { get; private set;}
	public ReactiveProperty<int> health { get; private set;}
	public ReactiveProperty<Vector3> position { get; set;}

	public void Initialize()
	{
		health = new ReactiveProperty<int>(3);
		position = new ReactiveProperty<Vector3>(Vector3.zero);
		speed = new ReactiveProperty<float>(10.0f);
	}
	
	public float GetSpeed()
	{
		return speed.Value;
	}
	
	public void DealDamage(int val)
	{
		health.Value -= val;
	}
	
}
