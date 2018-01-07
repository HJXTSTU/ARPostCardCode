using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIButtonCommand : MonoBehaviour {
	
	public GameObject map;
	public void OnMapToggleButton(){
		if (!map.activeInHierarchy)
			UICenter.Instance.MapAppear ();
		else
			UICenter.Instance.MapDisappear ();
	}
		
	public void OnMusicPlay(){
		MusicPlayer.Insatance.Play ();
		UICenter.Instance.OnPlayButtonClick ();
	}

	public void OnMusicPause(){
		MusicPlayer.Insatance.Pause ();
		UICenter.Instance.OnPauseButtonClick ();
	}

	public void OnMusicStop(){
		MusicPlayer.Insatance.Stop ();
		UICenter.Instance.OnStopButtonClick ();
	}

	public void OnExitButtonClick(){
		Application.Quit ();
	}


	public void OnNaviButtonCommand(){
		UICenter.Instance.NaviButtonEnable ();
		BuildingInfo info = Displayer.Instance.GetCurrentBuildingBuilding ().GetBuildingInfo();
		MapsUtil.PlanWalkRouteInAndroid (info);
	}
}
