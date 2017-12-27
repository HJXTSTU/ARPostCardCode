using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClickEvent : MonoBehaviour {
	public void OnMapBuildingClick(int index){
		Displayer.Instance.OnChangeDisplayBuilding (index);
		UICenter.Instance.MapDisappear ();
	}
}
