using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class AugmentedRealityPlacement : MonoBehaviour
{
	ARPlaneManager ARPlaneManager;
	ARRaycastManager ARRaycastManager;

	static List<ARRaycastHit> RayHits = new List<ARRaycastHit>();

	public Camera ARCamera;
	public GameObject VisualisationRoot;

	float ScaleAmount = 0.5f;
	bool Active = true;
	bool firstUpdate = true;

	/// <summary>
	/// 
	/// </summary>
	private void Awake()
	{
		ARRaycastManager = GetComponent<ARRaycastManager>();
		ARPlaneManager = GetComponent<ARPlaneManager>();
	}

	void SetInitialScale()
	{
		float initialScale = 12.0f;
		gameObject.transform.localScale = new Vector3(initialScale, initialScale, initialScale);
	}

	/// <summary>
	/// 
	/// </summary>
	private void FixedUpdate()
	{
		if (firstUpdate)
		{
			SetInitialScale();
			firstUpdate = false;
		}

		if (!Active) return;
		Vector3 centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2);
		Ray ray = ARCamera.ScreenPointToRay(centerOfScreen);

		if (ARRaycastManager.Raycast(ray, RayHits, TrackableType.PlaneWithinPolygon))
		{
			Pose hitPose = RayHits[0].pose;
			Vector3 positionToPlace = hitPose.position;
			VisualisationRoot.transform.position = positionToPlace;
		}
	}

	/// <summary>
	/// Called when the AR object is in the scene and we can stop tracking plaes and moving the
	/// visualisation.
	/// </summary>
	public void DisableARPlacementManager()
	{
		ARPlaneManager.enabled = false;
		SetAllPlanesActiveOrDeactive(false);
		this.enabled = false;
	}

	/// <summary>
	/// Used to scale the visualisation before it is finished being placed.
	/// </summary>
	public void Scale(bool increase)
	{
		float magnitude = !increase ? ScaleAmount : -ScaleAmount;

		Vector3 scaleFactor = new Vector3(magnitude, magnitude, magnitude);

		gameObject.transform.localScale += scaleFactor;
	}

	/// <summary>
	/// Used for disabling tracking planes when we have placed the visualisation into the real world
	/// </summary>
	/// <param name="value"></param>
	private void SetAllPlanesActiveOrDeactive(bool value)
	{
		foreach(var plane in ARPlaneManager.trackables)
		{
			plane.gameObject.SetActive(value);
		}
	}
}
