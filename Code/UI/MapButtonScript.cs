using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class MapButtonScript : MonoBehaviour {

	public int TargetIndex;
	public MapClickEvent clickCommand;
	//public FlagFloat mFlagFloat;
	private float initScale;
	private float endScale;
	// Use this for initialization
	void Start () {
		Debug.Log (transform.localScale);
		initScale = transform.localScale.x;
		endScale = transform.localScale.x*2;
	}


	void OnMouseOver(){
		Debug.Log ("Mouse Hover");
		this.transform.DOScale (endScale, 0.5f);
	}
		
	void OnMouseDown(){
		Debug.Log ("Mouse Down");
		this.transform.DOScale (initScale, 0.2f);
		clickCommand.OnMapBuildingClick (TargetIndex);
	}

	void OnMouseExit(){
		Debug.Log ("Mouse Exit");
		this.transform.DOScale (initScale, 0.5f);
	}
}
