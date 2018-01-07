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
	private Tweener mapToggleButtonTweener = null;
	public void MapToggleButtonAppear(){
		if (!mapToggleButton.gameObject.activeInHierarchy)
			mapToggleButton.gameObject.SetActive (true);
		TryKillTweener (mapToggleButtonTweener);
		mapToggleButtonTweener = mapToggleButton.transform.DOLocalMoveY (300.0f, 0.5f);
		mapToggleButtonTweener.onComplete= delegate() {
			mapToggleButtonTweener=null;
		};
	}

	public void MapToogleButtonDisappear(){
		TryKillTweener (mapToggleButtonTweener);
		mapToggleButtonTweener = mapToggleButton.transform.DOLocalMoveY (400.0f, 0.5f);
		mapToggleButtonTweener.onComplete = delegate() {
			mapToggleButton.gameObject.SetActive(false);
			mapToggleButtonTweener = null;
		};
	}

	public Image map;
	private Tweener mapTweener = null;
	public void MapAppear(){
		if (map.gameObject.activeInHierarchy)
			return;
		TryKillTweener (mapTweener);
		mapTweener = map.rectTransform.DOLocalMoveY (0, 1f).OnStart(new TweenCallback(delegate{
			map.gameObject.SetActive(true);
			mapTweener=null;
		}));
	}

	public void MapDisappear(){
		if (!map.gameObject.activeInHierarchy)
			return;
		TryKillTweener (mapTweener);
		mapTweener = map.rectTransform.DOLocalMoveY (700.0f, 1f);
		mapTweener.onComplete = delegate {
			map.gameObject.SetActive (false);
			mapTweener=null;
		};
	}

	public RectTransform mMusicPanel;
	private Tweener musicPanelTweener = null;
	public void MusicPanelAppear(){
		TryKillTweener (musicPanelTweener);
		musicPanelTweener = mMusicPanel.DOLocalRotate (new Vector3 (0, 30, 0), 0.5f);
		musicPanelTweener.onComplete= delegate {
			musicPanelTweener = null;
		};
	}

	public void MusicPanelDisappear(){
		TryKillTweener (musicPanelTweener);
		musicPanelTweener = mMusicPanel.DOLocalRotate (new Vector3 (0, 145, 0), 0.5f);
		musicPanelTweener.onComplete= delegate {
			musicPanelTweener = null;
		};
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
	private Tweener exitButtonTweener = null;
	private void ExitButtonAppear(){
		TryKillTweener (exitButtonTweener);
		exitButtonTweener = exitButton.DOLocalMoveY (300.0f, 0.5f);
		exitButtonTweener.onComplete= delegate {
			exitButtonTweener=null;
		};
	}

	private void ExitButtonDisappear(){
		TryKillTweener (exitButtonTweener);
		exitButtonTweener = exitButton.DOLocalMoveY (400.0f, 0.5f);
		exitButtonTweener.onComplete= delegate {
			exitButtonTweener=null;
		};
	}

	private Tweener specialThanksTweener = null;
	public void OnSpecialThanksAppear(){
		specialThanks.gameObject.SetActive (true);
		if(MusicPlayer.Insatance.HasAudio()){
			MusicPanelDisappear();
			MapToogleButtonDisappear ();
			NaviButtonDisappear ();
			if (map.gameObject.activeInHierarchy) {
				MapDisappear ();
			}
		}
		ExitButtonDisappear();

		TryKillTweener (specialThanksTweener);
		specialThanksTweener = specialThanks.DOScaleY (1, 0.5f);
		specialThanksTweener.onComplete = delegate(){
				content.DOLocalMoveY (-1.0f, 0);
				specialThanksTweener=null;
		};
	}

	public void OnSpecialThanksDisappear(){
		if(MusicPlayer.Insatance.HasAudio()){
			MusicPanelAppear ();
			MapToggleButtonAppear ();
			NaviButtonAppear ();
			if (map.gameObject.activeInHierarchy) {
				MapAppear ();
			}
		}
		ExitButtonAppear ();
	
		TryKillTweener (specialThanksTweener);
		specialThanksTweener = specialThanks.DOScaleY (0, 0.5f);
		specialThanksTweener.onComplete = delegate {
			specialThanks.gameObject.SetActive (false);
			specialThanksTweener = null;
		};
	}

	public Button mNaviButton;
	private Tweener naviButtonTweener = null;
	public void NaviButtonAppear(){
		if (!mNaviButton.gameObject.activeInHierarchy)
			mNaviButton.gameObject.SetActive (true);
		TryKillTweener (naviButtonTweener);
		naviButtonTweener = mNaviButton.transform.DOLocalMoveY (300.0f, 0.5f);
		naviButtonTweener.onComplete= delegate {
			naviButtonTweener = null;
		};
	}

	public void NaviButtonDisappear(){
		TryKillTweener (naviButtonTweener);
		naviButtonTweener = mNaviButton.transform.DOLocalMoveY (400.0f, 0.5f);
		naviButtonTweener.onComplete = delegate() {
			mNaviButton.gameObject.SetActive(false);
			naviButtonTweener = null;
		};
	}

	public void NaviButtonEnable(){
		mNaviButton.enabled = true;
	}

	private void TryKillTweener(Tweener tweener){
		if (tweener != null) {
			tweener.Kill (true);
		}
	}


	public GameObject mask;
	public Text loadProgress;
	public void MaskAppear(){
		mask.SetActive (true);
	}

	public void MaskDisappear(){
		mask.SetActive (false);
	}

	public void SetProgress(float progress){
		if (mask.activeInHierarchy) {
			float prog = Mathf.Floor (progress * 10000.0f)/100.0f;
			loadProgress.text =prog + "%";
		}
	}
}
