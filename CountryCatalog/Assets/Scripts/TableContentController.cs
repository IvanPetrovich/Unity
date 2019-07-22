using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableContentController : MonoBehaviour
{
	private AppLogic appLogic;
	[SerializeField]
	private GameObject row;
	[SerializeField]
	private GameObject content;
	
	private bool areaAsc	= true;
	private bool popAsc 	= true;
	private bool gdpAsc 	= true;
	
    void Start(){
        appLogic = FindObjectOfType<AppLogic>();
    }
	
	public void ClearContent(){
		foreach (Transform childTransform in content.transform) 
			Destroy(childTransform.gameObject);
	}

	public void SortByArea(){
		CapitalArray capArray = appLogic.GetCapArray();
		if(areaAsc){
			capArray.SortByAreaAsc();
		}else{
			capArray.SortByAreaDesc();
		}
		areaAsc=!areaAsc;
		FillTable();
	}
	
	public void SortByPopulation(){
		CapitalArray capArray = appLogic.GetCapArray();
		if(popAsc){
			capArray.SortByPopulationAsc();
		}else{
			capArray.SortByPopulationDesc();
		}
		popAsc=!popAsc;
		FillTable();
	}
	
	public void SortByGDP(){
		CapitalArray capArray = appLogic.GetCapArray();
		if(gdpAsc){
			capArray.SortByGDPAsc();
		}else{
			capArray.SortByGDPDesc();
		}
		gdpAsc=!gdpAsc;
		FillTable();
	}
	
	public void FillTable(){
		ClearContent();
		CapitalArray capArray = appLogic.GetCapArray();
		
		foreach (Capital cap in capArray.GetList()){
			
			GameObject newRow = Instantiate(row,transform.position,Quaternion.identity);
			RowFiller rf = newRow.GetComponent<RowFiller>();
			rf.FillRow(cap.GetCountryName(), cap.GetAreaS(), cap.GetPopulationS(), cap.GetGDPS());
			newRow.transform.SetParent(gameObject.transform);
			newRow.name = cap.GetCapitalName();
		}
	}
}
