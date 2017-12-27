using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

	private BuildingDisplayAttribute displayAttr;
	// Use this for initialization
	private Transform mBuilding;
	public AudioClip mIntroduceClip;
	public Transform GetBuildingObject(){
		return mBuilding;
	}


	public void Initialize(){
		Transform building = transform.GetChild(0);
		displayAttr = new BuildingDisplayAttribute ();
		// Debug.Log (building.name);
		if (building) {
			// Debug.Log (building.name);
			displayAttr.InitLocalPosition = building.localPosition;
			// Debug.Log (building.name);
			displayAttr.InitLocalRotation = building.localRotation;
			// Debug.Log (building.name);
			displayAttr.InitLocalScale    = building.localScale;
			mBuilding = building;
			building.gameObject.SetActive (false);
		} else {
			Debug.LogError (this.gameObject.name + " has no child");
		}
	}

	public BuildingDisplayAttribute GetDisplayAttr(){
		return displayAttr;
	}
}
