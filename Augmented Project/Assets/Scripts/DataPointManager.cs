using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPointManager : MonoBehaviour
{
	GameObject MainCamera;

	public GameObject DataPopup;

	void Start()
    {
		MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void Update()
	{
		DataPopup.transform.rotation = MainCamera.transform.rotation;
	}

	public void ChangePopupStatus()
	{
		DataPopup.SetActive(!DataPopup.activeSelf);
	}
}
