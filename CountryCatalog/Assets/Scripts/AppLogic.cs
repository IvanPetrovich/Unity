using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppLogic : MonoBehaviour
{
	private CapitalArray capArray;
	private MoveController moveController;
	private bool isControllersEnabled = true;
	
	void Start(){
		capArray		 = GetComponent<CapitalArray>();
		moveController	 = FindObjectOfType<MoveController>();
	}
	
	public CapitalArray GetCapArray(){
		return capArray;
	}
	
	public void AddCap(Capital capital){
		capArray.Add((Capital)capital.gameObject.GetComponent<Capital>() as Capital);
	}
	
	public void RemoveCap(Capital capital){
		capArray.RemoveCap(capital);
	}
	
	public void ClearCountryList(){
		foreach (Capital cap in capArray.GetList()){
			Mark mark = cap.gameObject.GetComponentInChildren<Mark>();
			mark.Switch();
			GameObject sight;
			sight = cap.GetSightObject();
			if(sight!=null){
				sight.SetActive(false);
			}
		}
		capArray.Clear();
	}
	
	public void SetControllersEnabled(bool val){
		isControllersEnabled = val;
	}
	public bool GetControllersEnabled(){
		return isControllersEnabled;
	}
	
}

