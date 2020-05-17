using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Script attached to each of the country orbs.
/// 
/// For each frame of the scene, the country orbs will rotate so that they are always facing the camera
/// 
/// The annotation is also moved to face the camera from any angle.
/// 
/// This script is used to enable/disable the data annotations from the controls dropdown. 
/// </summary>
public class DataPointManager : MonoBehaviour
{
	GameObject MainCamera;

	public TextMeshProUGUI TMP;

	public GameObject DataAnnotation;
	public GameObject Flag;

	public float Confirmed;
	public float Recovered;
	public float Deaths;


	/// <summary>
	/// Reference to camera in the scene, used for rotation
	/// Begin with annotations disabled
	/// </summary>
	void Start()
    {
		MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		DataAnnotation.SetActive(false);
	}

	/// <summary>
	/// Rotate the annotation as well as the flag as the camera rotates.
	/// This gives the viewer a clear view of the annotation as well as the flag as they 
	/// traverse the scene
	/// </summary>
	void Update()
	{
		DataAnnotation.transform.rotation = MainCamera.transform.rotation;
		Flag.transform.rotation = MainCamera.transform.rotation;
	}

	/// <summary>
	/// Enable/Disable the annotation
	/// </summary>
	public void ChangePopupStatus()
	{
		DataAnnotation.SetActive(!DataAnnotation.activeSelf);
	}

	/// <summary>
	/// Set the annotation
	/// </summary>
	/// <param name="label"></param>
	public void SetLabel(float ConfirmedFigure, float RecoveredFigure, float DeathsFigure)
	{
		Confirmed = ConfirmedFigure;
		Recovered = RecoveredFigure;
		Deaths = DeathsFigure;

		string newLine = "\n";
		string label =
			"Confirmed: " + ConfirmedFigure.ToString().Split('.')[0] + newLine +
			"Recovered: " + RecoveredFigure.ToString().Split('.')[0] + newLine +
			"Deaths: " + DeathsFigure.ToString().Split('.')[0];

		TMP.text = label;
	}
}
