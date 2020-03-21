﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AxisSetup : MonoBehaviour
{
	public GameObject AxisText;
	public TextMeshPro AxisTextMesh;

	public void AssignAxisLabel(int label)
	{
		AxisTextMesh.text = label.ToString();
	}
}
