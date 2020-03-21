using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Axis;

public class AxisManager : MonoBehaviour
{
	// Prefab for generating x, y and z axes
	public GameObject AxisPrefab;

	// x, y, z axes
	public GameObject[] Axes;

	public int[] XValues = { 2, 4, 6 };

	public int[] YValues = { 8, 10, 12};

	public int[] ZValues = { 14, 16, 18 };

	public void CreateAxes()
	{
		Axes = new GameObject[3];
		Axes[(int)AxisDirection.X] = GameObject.Instantiate(AxisPrefab, Vector3.zero, Quaternion.identity);
		Axes[(int)AxisDirection.X].GetComponent<AxisGenerator>().Draw(XValues, AxisDirection.X);
		
		Axes[(int)AxisDirection.Y] = GameObject.Instantiate(AxisPrefab, Vector3.zero, Quaternion.Euler(0, 0, 90.0f));
		Axes[(int)AxisDirection.Y].GetComponent<AxisGenerator>().Draw(YValues, AxisDirection.Y);

		Axes[(int)AxisDirection.Z] = GameObject.Instantiate(AxisPrefab, Vector3.zero, Quaternion.Euler(0, -90.0f, 0));
		Axes[(int)AxisDirection.Z].GetComponent<AxisGenerator>().Draw(ZValues, AxisDirection.Z);
		
	}
}
