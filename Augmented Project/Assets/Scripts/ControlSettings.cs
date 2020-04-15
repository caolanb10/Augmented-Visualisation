using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSettings : MonoBehaviour
{
	public GridManager GridManager;
	public GameObject DataPointRoot;
	public GameObject[] Datapoints;

	public void ShowHideGrid(int index)
	{
		if (index == 1)
		{
			GridManager.ChangeGridEnabled();
			gameObject.GetComponent<Dropdown>().value = 0;
		}
		if (index == 2)
		{
			
		}
	}

	public void GetDataPoints(int size)
	{
		Datapoints = new GameObject[size];
		for (int i = 0; i < size; i++)
		{
			Datapoints[i] = DataPointRoot.transform.GetChild(i).gameObject;
		}
	}
}
