using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RowFiller : MonoBehaviour
{
	[SerializeField]
	private Text country, area, population, gdp;
	
	public void FillRow(String countrytext, String areatext, String populationtext, String gdptext){
		country.text	 = countrytext;
		area.text		 = areatext;
		population.text	 = populationtext;
		gdp.text		 = gdptext;
	}
}
