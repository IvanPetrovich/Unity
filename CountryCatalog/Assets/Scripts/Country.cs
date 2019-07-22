using UnityEngine;
using System.Collections;
using System;

public abstract class Country : MonoBehaviour{

	[SerializeField]
	protected String countryName;

	[SerializeField]
	protected float population;

	[SerializeField]
	protected float area;

	[SerializeField]
	protected float gdpQ;// trillion
	
	public string GetCountryName(){
		return countryName;
	}
	
	public float GetPopulation(){
		return population;
	}
	public String GetPopulationS(){
		return population.ToString("##,#");
	}

	public float GetArea(){
		return area;
	}
	public String GetAreaS(){
		return area.ToString("##,#");
	}
	
	public float GetGDP(){
		return gdpQ;
	}
	public String GetGDPS(){
		return gdpQ.ToString();
	}
}