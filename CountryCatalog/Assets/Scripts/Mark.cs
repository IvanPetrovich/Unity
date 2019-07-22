using UnityEngine;
using System.Collections;

public class Mark : MonoBehaviour{
	
	[SerializeField]
	private bool isChecked;
	
	[SerializeField]
	private GameObject mark;
	
	[SerializeField]
	private GameObject checkedMark;	
	
	public bool IsChecked(){		
		return isChecked;		
	}
	
	public void Switch(){		
		isChecked = !isChecked;		
		SetActiveCheckedMark(isChecked);
		SetActiveMark(!isChecked);		
	}
	
	public void SetActiveMark(bool val){
		mark.SetActive(val);
	}
	
	public void SetActiveCheckedMark(bool val){
		checkedMark.SetActive(val);
	}
}