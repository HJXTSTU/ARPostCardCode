using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LoadingState{
	Idle,
	Loading
}
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
			curObject = null;
			curPrefab = null;
			Resources.UnloadUnusedAssets ();
		}
	}

	public void ReleaseObject(){
		Unload ();
	}
		
	private LoadingState mState = LoadingState.Idle;
	public LoadingState State{
		get{
			return mState;
		}
	}
	private ResourceRequest req = null;
	private System.Action onLoadComplete = null;
	private Coroutine loadCoroutine = null;
	public void LoadBuildingAsync(int index,System.Action onComplete){
		if (curPrefab != null) {
			Unload ();
		}
		string path = "BuildingPrefabs/Building_" + index;
		UICenter.Instance.MaskAppear ();
		onLoadComplete = onComplete;
		mState = LoadingState.Loading;
		loadCoroutine = StartCoroutine ("LoadResource", path);
	}

	private IEnumerator LoadResource(string path){
		req = Resources.LoadAsync (path);
		yield return req;
	}

	public void StopLoadingBuilding(){
		if (mState == LoadingState.Loading) {
			StopCoroutine (loadCoroutine);
			req = null;
			onLoadComplete = null;
			loadCoroutine = null;
			mState = LoadingState.Idle;
			UICenter.Instance.SetProgress (0.0f);
			UICenter.Instance.MaskDisappear ();
		}
	}

	void Update(){
		if (mState == LoadingState.Loading) {
			UICenter.Instance.SetProgress (req.progress);
			if (req.isDone) {
				curPrefab = req.asset as GameObject;
				GameObject building_obj = GameObject.Instantiate (curPrefab, Vector3.zero, Quaternion.identity, transform);
				Building building = building_obj.GetComponent<Building> ();
				building.Initialize ();
				curObject = building_obj;
				mState = LoadingState.Idle;
				UICenter.Instance.MaskDisappear ();
				StopCoroutine (loadCoroutine);

				onLoadComplete ();
				onLoadComplete = null;
				req = null;
				loadCoroutine = null;

			} else {
				UICenter.Instance.SetProgress (req.progress);
			}
		}
	}
}
