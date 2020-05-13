using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSettings : MonoBehaviour
{
	public GridManager GridManager;
	public GameObject DataPointRoot;
	public GameObject[] Datapoints;

	bool DataPointsEnabled = false;
	int NumberOfDataPoints;

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
		gameObject.GetComponent<Dropdown>().value = 0;
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

		if(Datapoints == null)
		{
			Debug.Log("First call");
			NumberOfDataPoints = DataPointRoot.transform.childCount;
			Debug.Log(NumberOfDataPoints);
			Datapoints = new GameObject[NumberOfDataPoints];
		}
		else
		{
			Debug.Log(Datapoints);
			Debug.Log("Second Call");
		}

		for (int i = 0; i < NumberOfDataPoints; i ++)
		{
			GameObject foundChild = DataPointRoot.transform.GetChild(i).gameObject;
			Debug.Log(foundChild != null);
			Datapoints[i] = foundChild;
		}
	}
}
