using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displayer : MonoBehaviour {
	private static Displayer _instnace=null;
	public static Displayer Instance{
		get{
			return _instnace;
		}
	}

	void Awake(){
		_instnace = this;
	}


	private Transform trans_imageTarget = null;		//当前被识别到的图片的Transform
	private Transform trans_targetBuilding = null;	//当前正在显示的建筑
	private Building targetBuilding = null;			//当前正在显示的建筑槽
	private int targetIndex=-1;						//当前正在显示的建筑槽下标

	public void OnTrackableFound(Transform trans,int index){
		trans_imageTarget = trans;
		targetIndex = index;
		BuildingList.Instance.LoadBuilding (index);
		targetBuilding =BuildingList.Instance.GetCurrentBuildingBuilding ();

		BuildingDisplayAttribute attr = targetBuilding.GetDisplayAttr ();		// 获取建筑显示参数
		trans_targetBuilding = targetBuilding.GetBuildingObject ();				// 获取建筑模型
		DisplayBuilding(attr,trans_targetBuilding,trans_imageTarget);			// 显示建筑模型
		UICenter.Instance.MapToggleButtonAppear ();	// 显示小地图[显示/隐藏]按钮
		UICenter.Instance.MusicPanelAppear();		// 显示播放器按钮
		MusicPlayer.Insatance.SetAudioClip(targetBuilding.mIntroduceClip);	// 设置音乐
	}

	private void DisplayBuilding(BuildingDisplayAttribute attr,Transform targetBuildingModal,Transform imageTarget){
		if (!targetBuildingModal.gameObject.activeInHierarchy) {
			targetBuildingModal.gameObject.SetActive (true);
			targetBuildingModal.SetParent (imageTarget);
			targetBuildingModal.localPosition = attr.InitLocalPosition;
			targetBuildingModal.localRotation = attr.InitLocalRotation;
			targetBuildingModal.localScale = attr.InitLocalScale;
			var rendererComponents = targetBuildingModal.GetComponentsInChildren<Renderer>(true);
			var colliderComponents = targetBuildingModal.GetComponentsInChildren<Collider>(true);
			var canvasComponents = targetBuildingModal.GetComponentsInChildren<Canvas>(true);

			// Enable rendering:
			foreach (var component in rendererComponents)
				component.enabled = true;

			// Enable colliders:
			foreach (var component in colliderComponents)
				component.enabled = true;

			// Enable canvas':
			foreach (var component in canvasComponents)
				component.enabled = true;
			
		}
	}

	private void HideBuilding(Transform targetBuildingModal,Transform targetBuilding){
		var rendererComponents = targetBuildingModal.GetComponentsInChildren<Renderer> (true);
		var colliderComponents = targetBuildingModal.GetComponentsInChildren<Collider> (true);
		var canvasComponents = targetBuildingModal.GetComponentsInChildren<Canvas> (true);

		// Enable rendering:
		foreach (var component in rendererComponents)
			component.enabled = false;

		// Enable colliders:
		foreach (var component in colliderComponents)
			component.enabled = false;

		// Enable canvas':
		foreach (var component in canvasComponents)
			component.enabled = false;

		targetBuildingModal.gameObject.SetActive (false);
		targetBuildingModal.SetParent (targetBuilding);
	}

	public void OnTrackableLost(Transform trans){
		if (trans_imageTarget != null && targetBuilding != null && trans_targetBuilding!=null) {
			if (trans_targetBuilding.gameObject.activeInHierarchy) {
				HideBuilding (trans_targetBuilding, targetBuilding.transform);
				trans_imageTarget = null;
				targetBuilding = null;
				trans_targetBuilding = null;
				targetIndex = -1;
				MusicPlayer.Insatance.ClearAudioClip();		// 清空播放器
				BuildingList.Instance.ReleaseObject ();		// 释放当前建筑
				UICenter.Instance.MapToogleButtonDisappear ();	//按钮隐藏
				UICenter.Instance.MapDisappear ();	//地图隐藏
				UICenter.Instance.MusicPanelDisappear();	//播放器按钮隐藏

			}
		}
	}


	public void OnChangeDisplayBuilding(int index){
		if (trans_imageTarget != null && targetBuilding != null) {
			trans_targetBuilding.SetParent (targetBuilding.transform);
			trans_targetBuilding.gameObject.SetActive (false);
			BuildingList.Instance.ReleaseObject ();
			BuildingList.Instance.LoadBuilding (index);
			MusicPlayer.Insatance.ClearAudioClip ();
			Building newBuilding = BuildingList.Instance.GetCurrentBuildingBuilding ();
			if (newBuilding) {
				Transform newBuildingModal = newBuilding.GetBuildingObject ();
				BuildingDisplayAttribute newAttr = newBuilding.GetDisplayAttr ();
				targetBuilding = newBuilding;
				trans_targetBuilding = newBuildingModal;
				DisplayBuilding (newAttr, trans_targetBuilding, trans_imageTarget);
				MusicPlayer.Insatance.SetAudioClip (targetBuilding.mIntroduceClip);
				UICenter.Instance.ResetMusicButtonState ();
			} else {
				Debug.LogError ("Here is building on index :" + index);
			}
		}
	}
}
