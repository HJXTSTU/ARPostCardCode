using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackableEventHandler : MonoBehaviour,ITrackableEventHandler {
	
	protected TrackableBehaviour mTrackableBehaviour;

	// Use this for initialization
	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour> ();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
		}
	}


	#region PUBLIC_METHODS

	/// <summary>
	///     Implementation of the ITrackableEventHandler function called when the
	///     tracking state changes.
	/// </summary>
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
			OnTrackingFound();
		}
		else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
			newStatus == TrackableBehaviour.Status.NOT_FOUND)
		{
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
			OnTrackingLost();
		}
		else
		{
			// For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
			// Vuforia is starting, but tracking has not been lost or found yet
			// Call OnTrackingLost() to hide the augmentations
			OnTrackingLost();
		}
	}

	#endregion // PUBLIC_METHODS

	#region PRIVATE_METHODS


	public int buildingIndex;
	public int BuildingIndex{
		set{
			buildingIndex = value;
		}
		get{
			return buildingIndex;
		}
	}
	protected virtual void OnTrackingFound()
	{
//		var rendererComponents = GetComponentsInChildren<Renderer>(true);
//		var colliderComponents = GetComponentsInChildren<Collider>(true);
//		var canvasComponents = GetComponentsInChildren<Canvas>(true);
//
//		// Enable rendering:
//		foreach (var component in rendererComponents)
//			component.enabled = true;
//
//		// Enable colliders:
//		foreach (var component in colliderComponents)
//			component.enabled = true;
//
//		// Enable canvas':
//		foreach (var component in canvasComponents)
//			component.enabled = true;
		Displayer.Instance.OnTrackableFound (transform, buildingIndex);
	}


	protected virtual void OnTrackingLost()
	{
//		var rendererComponents = GetComponentsInChildren<Renderer>(true);
//		var colliderComponents = GetComponentsInChildren<Collider>(true);
//		var canvasComponents = GetComponentsInChildren<Canvas>(true);
//
//		// Disable rendering:
//		foreach (var component in rendererComponents)
//			component.enabled = false;
//
//		// Disable colliders:
//		foreach (var component in colliderComponents)
//			component.enabled = false;
//
//		// Disable canvas':
//		foreach (var component in canvasComponents)
//			component.enabled = false;
		Displayer.Instance.OnTrackableLost(transform);
	}

	#endregion // PRIVATE_METHODS

}
