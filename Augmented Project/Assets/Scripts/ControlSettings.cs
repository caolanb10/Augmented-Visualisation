using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSettings : MonoBehaviour
{
	Dropdown Dropdown;

	public GridManager GridManager;
	public Countries Countries;

	public GameObject DataPointRoot;
	public GameObject[] Datapoints;

	bool DataPointsEnabled = false;
	int NumberOfDataPoints;

	public void Awake()
	{
		Dropdown = gameObject.GetComponent<Dropdown>();
	}

	/// <summary>
	/// Handles input from user from the control settings dropdown, where index is the index of
	/// the selected option from top (0 (ignored)) to bottom (2).
	/// </summary>
	/// <param name="index"></param>
	public void HandleValueChanged(int index)
	{
		switch (index)
		{
			case 1:
				GridManager.ChangeGridEnabled();
				break;
			case 2:
				EnableOrDisableLabels();
				break;
			default:
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
		Dropdown.value = 0;
	}

	/// <summary>
	/// For each data point in the visualisation, either enable or disable the textual
	/// annotation for that data point (visual preference/additional encoding)
	/// 
	/// On first call of this function we must find and store a reference for all these data points.
	/// 
	/// For subsequent calls, we will use the saved references from the previous call to save time. 
	/// </summary>
	public void EnableOrDisableLabels()
	{
		DataPointsEnabled = !DataPointsEnabled;
		Countries.ChangeLabelsEnabled();
	}
}
