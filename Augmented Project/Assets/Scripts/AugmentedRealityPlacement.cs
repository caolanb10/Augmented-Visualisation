using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

/// <summary>
/// 
/// Class for implementing augmented reality aspect of this visualisation.
/// 
/// Using: ARFoundation.
/// 
/// ARraycastManager, ARPlaneManager are enabled by default at the start of the scene.
/// 
/// On each update, raycast from the center of the screen toward the planes. 
/// 
/// If it strikes a plane, move the root of the visualisation to the position of where it hit the plane.
/// 
/// Has functionality for scaling the visualisation.
/// 
/// When the visualisation has been placed into the scene and scaled accordingly, 
/// DisableARPlacementManager() is called to stop tracking planes in the scene and the 
/// root of the visualisation will remain in place. 
/// 
/// </summary>
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
	/// Get references to scripts on this game object before first frame.
	/// </summary>
	private void Awake()
	{
		ARRaycastManager = GetComponent<ARRaycastManager>();
		ARPlaneManager = GetComponent<ARPlaneManager>();
	}

	/// <summary>
	/// Called on first update to adjust the scale of the scene to be suitable for an
	/// AR environment.
	/// </summary>
	void SetInitialScale()
	{
		float initialScale = 12.0f;
		gameObject.transform.localScale = new Vector3(initialScale, initialScale, initialScale);
	}

	private void FixedUpdate()
	{
		// Was having issues doing this in start/awake so a slightly hackier way is done here.
		if (firstUpdate)
		{
			SetInitialScale();
			firstUpdate = false;
		}

		if (!Active) return;

		// Vector representing our crosshairs from the UI.
		Vector3 centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2);
		Ray ray = ARCamera.ScreenPointToRay(centerOfScreen);

		// Cast from the crosshairs against the planes found.
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
