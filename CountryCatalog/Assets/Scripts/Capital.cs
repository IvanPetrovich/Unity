using UnityEngine;
using System.Collections;
using System;

public class Capital : Country
{
	[SerializeField]
	string capitalName;
	[SerializeField]
	GameObject sight;
	
	void Start(){
		sight.SetActive(false);
	}
	
	public override string ToString(){
		return this.capitalName + " pop: "+ this.GetPopulation() + " area: " + this.GetArea();
	}
	
	public string GetCapitalName(){
		return capitalName;
	}

	public GameObject GetSightObject(){
		return sight;
	}

	public bool Equals(Capital other){
		return this.area == other.area;
	}
}