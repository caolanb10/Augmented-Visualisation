using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedControlSettings : MonoBehaviour
{
	Dropdown dropdown;
	
	public void Awake()
	{
		dropdown = gameObject.GetComponent<Dropdown>();
	}

	/// <summary>
	/// Handles input from user from the speed control settings dropdown, where index is the index of
	/// the selected option from top (0 (ignored)) to bottom (2).
	/// 
	/// These values will affect the rate at which the animation in the visualisation will progress.
	/// 
	/// </summary>
	/// <param name="index"></param>
	public void HandleValueChanged(int index)
	{
		switch (index)
		{
			case 1:
				Debug.Log(dropdown.options[1].text);
				break;
			case 2:
				Debug.Log(dropdown.options[2].text);
				break;
			case 3:
				Debug.Log(dropdown.options[3].text);
				break;
		}
		ResetDropdown();
	}

	/// <summary>
	/// After action is called, change value of dropdown to the 
	/// first value which is the title of the dropdown
	/// </summary>
	public void ResetDropdown()
	{
		dropdown.value = 0;
	}
}
