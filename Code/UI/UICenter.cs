using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;


public class UICenter : MonoBehaviour {
	private static UICenter _instance;
	public static UICenter Instance{
		get{
			return _instance;
		}
	}

	void Awake(){
		_instance = this;
		ResetMusicButtonState ();
	}

	public Button mapToggleButton;
	public void MapToggleButtonAppear(){
		if (!mapToggleButton.gameObject.activeInHierarchy)
			mapToggleButton.gameObject.SetActive (true);
		mapToggleButton.transform.DOLocalMoveY (300.0f, 0.5f);
	}

	public void MapToogleButtonDisappear(){
		mapToggleButton.transform.DOLocalMoveY (400.0f, 0.5f).onComplete = delegate() {
			mapToggleButton.gameObject.SetActive(false);
		};
	}

	public Image map;
	public void MapAppear(){
		if (map.gameObject.activeInHierarchy)
			return;
		map.rectTransform.DOLocalMoveY (0, 1f).OnStart(new TweenCallback(delegate{
			map.gameObject.SetActive(true);
		}));
	}

	public void MapDisappear(){
		if (!map.gameObject.activeInHierarchy)
			return;
		map.rectTransform.DOLocalMoveY(700.0f, 1f).onComplete = delegate {
			map.gameObject.SetActive (false);
		};
	}

	public RectTransform mMusicPanel;
	public void MusicPanelAppear(){
		mMusicPanel.DOLocalRotate (new Vector3 (0, 30, 0), 0.5f);
	}

	public void MusicPanelDisappear(){
		mMusicPanel.DOLocalRotate (new Vector3 (0, 145, 0), 0.5f);
	}


	public Button play;
	public Button pause;
	public Button stop;

	private void DisableButton(Button btn){
		btn.enabled = false;
	}

	private void EnableButton(Button btn){
		btn.enabled = true;
	}

	public void OnPlayButtonClick(){
		DisableButton (play);
		EnableButton (pause);
		EnableButton (stop);
	}

	public void OnPauseButtonClick(){
		DisableButton (pause);
		EnableButton (stop);
		EnableButton  (play);
	}

	public void OnStopButtonClick(){
		DisableButton (stop);
		DisableButton (pause);
		EnableButton (play);

	}
	public void ResetMusicButtonState(){
		DisableButton (pause);
		DisableButton (stop);
		EnableButton  (play);
	}

	public RectTransform specialThanks;
	public RectTransform exitButton;
	public RectTransform content;
	private void ExitButtonAppear(){
		exitButton.DOLocalMoveY (300.0f, 0.5f);
	}

	private void ExitButtonDisappear(){
		exitButton.DOLocalMoveY (400.0f, 0.5f);
	}

	public void OnSpecialThanksAppear(){
		specialThanks.gameObject.SetActive (true);
		if(MusicPlayer.Insatance.HasAudio()){
			MusicPanelDisappear();
			MapToogleButtonDisappear ();
			if (map.gameObject.activeInHierarchy) {
				MapDisappear ();
			}
		}
		ExitButtonDisappear();
		specialThanks.DOScaleY (1, 0.5f).onComplete = delegate(){
				content.DOLocalMoveY (-1.0f, 0);
		};
	}

	public void OnSpecialThanksDisappear(){
		if(MusicPlayer.Insatance.HasAudio()){
			MusicPanelAppear ();
			MapToggleButtonAppear ();
			if (map.gameObject.activeInHierarchy) {
				MapAppear ();
			}
		}
		ExitButtonAppear ();


		specialThanks.DOScaleY (0, 0.5f).onComplete = delegate {
				specialThanks.gameObject.SetActive (false);
		};
	}
}
