using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Used for labeling the axis with its numerical information.
/// </summary>
public class AxisPieceSetup : MonoBehaviour
{
	public TextMeshPro AxisTextMesh;

	public void AssignAxisLabel(float label)
	{
		AxisTextMesh.text = label.ToString();
	}
}
