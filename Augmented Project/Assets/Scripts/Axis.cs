using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that contains an enumeration between the x, y, z and 0, 1, 2
/// </summary>
public class Axis : MonoBehaviour
{
	public const int NumberOfDirections = 3;

	public enum AxisDirection
	{
		X = 0,
		Y = 1,
		Z = 2,
	}
}
