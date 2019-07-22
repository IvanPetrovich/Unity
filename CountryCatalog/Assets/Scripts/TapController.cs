using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : MonoBehaviour
{
	private float delay = 0;
	private float maxDelay = 0.5f;
	private bool isTapped = false;

	private AppLogic appLogic;
	
	private UIController uiController;
	private MoveController moveController;
	
	private bool isEnabled = true;
	
	void Start(){
		appLogic		 = FindObjectOfType<AppLogic>();
		uiController	 = FindObjectOfType<UIController>();
		moveController	 = FindObjectOfType<MoveController>();
	}
	
	public void SetEnabled(bool val){
		isEnabled = val;
	}
	
	void OnMouseDown(){
		if(!appLogic.GetControllersEnabled()){
			TapOff();
			return;
		}
		isTapped = true;
		delay = 0f;
	}

	public void TapOff(){
		isTapped = false;
	}

	void Update(){
		if(isTapped){
			delay += Time.deltaTime;			
			if(delay > maxDelay){
				Mark mrk = gameObject.GetComponentInParent<Mark>();			
				mrk.Switch();
				isTapped = false;
				Capital cap;
				cap = gameObject.GetComponentInParent<Capital>() as Capital;
				if(mrk.IsChecked()){
					if(cap!=null){
						appLogic.AddCap(cap);
						uiController.HideCountryInfo();
					}
				}else{
					GameObject sight;
					sight = cap.GetSightObject();
					if(sight!=null){
						sight.SetActive(false);
					}
					appLogic.RemoveCap(cap);
				}
				int countryCount = appLogic.GetCapArray().Count();
				if(appLogic.GetCapArray().Count()>0){
					uiController.ShowButtonClear();
				}else{
					uiController.HideButtonClear();
				}
				uiController.UpdateTextCountryCount(countryCount);
			}			
		}		
	}
	
	void OnMouseUp(){
		
		if(delay < maxDelay && isTapped){
			Capital cap;
			GameObject sight;
			cap = gameObject.GetComponentInParent<Capital>() as Capital;
			if(cap!=null){
				sight = cap.GetSightObject();
				if(sight!=null){
					sight.SetActive(!sight.activeInHierarchy);
					Mark mrk = gameObject.GetComponentInParent<Mark>();
					if(sight.activeInHierarchy){
						mrk.SetActiveMark(false);
						uiController.FillInfoPanel(cap.GetCountryName(),cap.GetAreaS(), cap.GetPopulationS(), cap.GetGDPS());
						uiController.ShowCountryInfo();						
						//go to sight
						Vector3 target = cap.gameObject.transform.position;
						moveController.GoToObject(target);
					}else{
						if(!mrk.IsChecked()){
							mrk.SetActiveMark(true);
						}						
						uiController.HideCountryInfo();
					}
				}
			}
		}
		isTapped = false;
	}
}
