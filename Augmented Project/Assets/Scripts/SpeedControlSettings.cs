using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for changing the speed of the visualsation. 
/// 
/// Sets the "CurrentSpeed" variable in the visualisation manager when the user selects a value.
/// 
/// Resets the dropdown to have the value "Speed Control" after every input. 
/// </summary>
public class SpeedControlSettings : MonoBehaviour
{
	public VisualisationManager VisualisationManager;
	Dropdown Dropdown;
	float CurrentSpeed = 1.0f;
	
	public void Awake()
	{
		Dropdown = gameObject.GetComponent<Dropdown>();
	}

	/// <summary>
	/// These values will affect the rate at which the animation in the visualisation will progress.
	/// </summary>
	/// <param name="index"></param>
	public void HandleValueChanged(int index)
	{
		if (index != 0)
		{
			string selection = Dropdown.options[index].text;
			CurrentSpeed = float.Parse(selection);

			ResetDropdown();
			VisualisationManager.CurrentSpeed = CurrentSpeed;
		}
	}

	/// <summary>
	/// After action is called, change value of dropdown to the 
	/// first value which is the title of the dropdown
	/// </summary>
	public void ResetDropdown()
	{
		Dropdown.value = 0;
	}
}
