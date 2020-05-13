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

	public void ResetDropdown()
	{
		gameObject.GetComponent<Dropdown>().value = 0;
	}
}
