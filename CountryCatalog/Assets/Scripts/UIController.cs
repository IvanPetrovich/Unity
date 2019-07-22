using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
	private AppLogic appLogic;
	
	[SerializeField]
	private GameObject buttonClear;
	
	[SerializeField]
	private Text textCountryCount;
	
	[SerializeField]
	private GameObject tablePanel;
	
	[SerializeField]
	private GameObject infoPanel;
	
	[SerializeField]
	private Text textCountry;
	
	[SerializeField]
	private Text textArea;
	
	[SerializeField]
	private Text textGDP;
	
	[SerializeField]
	private Text textPopulation;
	
	[SerializeField]
	private TableContentController tableContentController;
	

    void Start()
    {
        appLogic				 = FindObjectOfType<AppLogic>();
		if(tableContentController==null){
			tableContentController 	 = FindObjectOfType<TableContentController>();
		}
		HideCountryList();
		HideCountryInfo();
    }
	
	public void ClearCountryList(){
		appLogic.ClearCountryList();
		HideButtonClear();
		UpdateTextCountryCount(0);
	}

	public void ShowButtonClear(){
		if(buttonClear!=null){
			buttonClear.SetActive(true);
		}
	}
	
	public void HideButtonClear(){
		if(buttonClear!=null){
			buttonClear.SetActive(false);
		}
	}
	
	public void UpdateTextCountryCount(int count){
		if(count>0){
			textCountryCount.text = "Выбрано " + count + " стран";
		}else{
			textCountryCount.text = "";
		}
	}
	
	public void ShowCountryList(){
		if(tablePanel!=null){
			tablePanel.SetActive(true);
			tableContentController.FillTable();
			appLogic.SetControllersEnabled(false);
		}
	}
	
	public void HideCountryList(){
		if(tablePanel!=null){
			tablePanel.SetActive(false);
			appLogic.SetControllersEnabled(true);
		}
	}
	
	public void FillInfoPanel(String country, String area, String population, String gdp){
		textCountry.text = country;
		textArea.text = "Площадь: " + area + " кв.км.";
		textPopulation.text = "Население: " + population;
		textGDP.text = "ВВП: " + gdp + " трлн.$";
	}
	
	public void ShowCountryInfo(){
		if(infoPanel!=null){
			infoPanel.SetActive(true);
		}
	}
	
	public void HideCountryInfo(){
		if(infoPanel!=null){
			infoPanel.SetActive(false);
		}
	}
}
