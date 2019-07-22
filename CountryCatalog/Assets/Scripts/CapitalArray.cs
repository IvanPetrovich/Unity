using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalArray : MonoBehaviour//, IEnumerable
{

	public List<Capital> capitalArray;
	
	void Awake(){
		capitalArray = new List<Capital>();
	}
	
	public void Add(Capital cap){
		capitalArray.Add(cap);
	}
	
	public void Clear(){
		capitalArray.Clear();
	}
	
	public List<Capital> GetList(){
		return capitalArray;
	}
	public void RemoveCap(Capital cap){
		capitalArray.Remove(cap);
	}
	
	public int Count(){
		return capitalArray.Count;
	}

	public void SortByPopulationAsc(){
		capitalArray.Sort(delegate(Capital x, Capital y)
        {
            if (x.GetPopulation() == y.GetPopulation()) return 0;
            else if (x.GetPopulation() > y.GetPopulation()) return 1;
			return -1;
        });
	}

	public void SortByPopulationDesc(){
		capitalArray.Sort(delegate(Capital x, Capital y)
        {
            if (x.GetPopulation() == y.GetPopulation()) return 0;
            else if (x.GetPopulation() > y.GetPopulation()) return -1;
			return 1;
        });
	}

	public void SortByAreaAsc(){
		
        capitalArray.Sort(delegate(Capital x, Capital y)
        {
            if (x.GetArea() == y.GetArea()) return 0;
            else if (x.GetArea() > y.GetArea()) return 1;
			return -1;
        });
	}
	
	public void SortByAreaDesc(){
		
        capitalArray.Sort(delegate(Capital x, Capital y)
        {
            if (x.GetArea() == y.GetArea()) return 0;
            else if (x.GetArea() > y.GetArea()) return -1;
			return 1;
        });
	}

	public void SortByGDPAsc(){
		
        capitalArray.Sort(delegate(Capital x, Capital y)
        {
            if (x.GetGDP() == y.GetGDP()) return 0;
            else if (x.GetGDP() > y.GetGDP()) return 1;
			return -1;
        });
	}

	public void SortByGDPDesc(){
		
        capitalArray.Sort(delegate(Capital x, Capital y)
        {
            if (x.GetGDP() == y.GetGDP()) return 0;
            else if (x.GetGDP() > y.GetGDP()) return -1;
			return 1;
        });
	}
	
	public IEnumerator GetEnumerator()
    {
        return capitalArray.GetEnumerator();
    }
}
