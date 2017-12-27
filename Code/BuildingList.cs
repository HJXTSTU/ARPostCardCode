using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingList : MonoBehaviour {
	private static BuildingList _instance;
	public static BuildingList Instance{
		get{
			return _instance;
		}
	}

	public void Awake(){
		_instance = this;
	}

//	public Building GetBuilding(int index){
//		if (index < buildingList.Length && index >= 0)
//			return buildingList [index];
//		else
//			return null;
//	}


	private GameObject curPrefab=null;
	private GameObject curObject=null;
	public Building GetBuilding(int index){
		if (curPrefab != null) {
			Unload ();
		}
		string path = "BuildingPrefabs/Building_" + index;
		GameObject buildingprefab = Resources.Load (path) as GameObject;
		GameObject building_obj = GameObject.Instantiate (buildingprefab,Vector3.zero,Quaternion.identity,transform);
		Building res = building_obj.GetComponent<Building> ();
		res.Initialize ();
		curPrefab = buildingprefab;
		curObject = building_obj;
		return res;
	}

	public void LoadBuilding(int index){
		if (curPrefab != null) {
			Unload ();
		}
		string path = "BuildingPrefabs/Building_" + index;
		GameObject buildingprefab = Resources.Load (path) as GameObject;
		GameObject building_obj = GameObject.Instantiate (buildingprefab,Vector3.zero,Quaternion.identity,transform);
		Building building = building_obj.GetComponent<Building> ();
		building.Initialize ();
		curPrefab = buildingprefab;
		curObject = building_obj;
	}

	public Building GetCurrentBuildingBuilding(){
		return curObject.GetComponent<Building> ();
	}

	private void Unload(){
		if (curPrefab != null && curObject != null) {
			DestroyImmediate (curObject);
			//DestroyImmediate (curPrefab);
			curObject = null;
			curPrefab = null;
			Resources.UnloadUnusedAssets ();
		}
	}

	public void ReleaseObject(){
		Unload ();
	}
}
