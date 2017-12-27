using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG;
using DG.Tweening;
public class ImageFade : MonoBehaviour {
	private Image mImage;
	public float mTime;
	public float mDelay;
	void Awake(){
		mImage = GetComponent<Image> ();
		mImage.DOFade (1, mTime).onComplete = delegate {
			mImage.DOFade (0, mTime).SetDelay (mDelay).onComplete=delegate {
				SceneManager.LoadScene(1);
			};
		};
	}
}
