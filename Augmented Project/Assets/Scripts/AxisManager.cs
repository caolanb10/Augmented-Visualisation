using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Axis;

public class AxisManager : MonoBehaviour
{
	// Prefabs
	public GameObject AxisPrefab;
	public GameObject AxisOriginPrefab;
	public GameObject GridPrefab;

	// Reference to game object in scene that holds all axis objects
	public GameObject AxisRoot;

	public bool DebugEnabled;

	// x, y, z axes
	public GameObject[] Axes;

	public int NumberOfAxisPoints = 10;

	float[] XValues = { 2, 4, 6, 8, 10, 12, 14, 16, 18 };
	float[] YValues = { 100, 200, 300, 400, 500 };
	float[] ZValues = { 0.4f, 0.6f, 0.8f };


	public void CreateAxes()
	{
		// Origin
		GameObject.Instantiate(AxisOriginPrefab, Vector3.zero, Quaternion.identity, AxisRoot.transform);
		Axes = new GameObject[3];

		CreateAxis(AxisDirection.X, Quaternion.identity);
		CreateAxis(AxisDirection.Y, Quaternion.Euler(0, 0, 90.0f));
		CreateAxis(AxisDirection.Z, Quaternion.Euler(0, -90.0f, 0));

		Axes[(int)AxisDirection.X].GetComponent<AxisGenerator>().Draw(CreateAxisPointsFromMax(HighestValue(XValues)), AxisDirection.X);
		Axes[(int)AxisDirection.Y].GetComponent<AxisGenerator>().Draw(CreateAxisPointsFromMax(HighestValue(YValues)), AxisDirection.Y);
		Axes[(int)AxisDirection.Z].GetComponent<AxisGenerator>().Draw(CreateAxisPointsFromMax(HighestValue(ZValues)), AxisDirection.Z);

		DrawGrid();
	}

	void CreateAxis(AxisDirection direction, Quaternion rotation)
	{
		Axes[(int)direction] = GameObject.Instantiate(AxisPrefab, Vector3.zero, rotation, AxisRoot.transform);
	}

	float HighestValue(float[] vals)
	{
		float max = 0;
		foreach(float val in vals)
		{
			max = val > max ? val : max;
		}
		return max;
	}

	float[] CreateAxisPointsFromMax(float max)
	{
		float[] axisPoints = new float[NumberOfAxisPoints];
		for(int i = 0; i < NumberOfAxisPoints; i++)
		{
			axisPoints[i] = (max / NumberOfAxisPoints) * (i + 1);
			if(DebugEnabled) Debug.Log("Point " + i + " is " + axisPoints[i]);
		}
		return axisPoints;
	}

	void DrawGrid()
	{
		for(int i = 0; i < NumberOfAxisPoints; i ++)
		{
			int offset = (i * 2) + 1;
			Vector3 positionX = new Vector3(offset, NumberOfAxisPoints, 0);
			Vector3 positionY = new Vector3(NumberOfAxisPoints, offset, 0);
			Vector3 positionZ = new Vector3(NumberOfAxisPoints, 0, offset);


			GameObject gridlinex = GameObject.Instantiate(GridPrefab, positionX, Quaternion.identity, AxisRoot.transform);
			GameObject gridliney = GameObject.Instantiate(GridPrefab, positionY, Quaternion.Euler(0,0,90), AxisRoot.transform);
			GameObject gridlinez = GameObject.Instantiate(GridPrefab, positionZ, Quaternion.Euler(0,0,90), AxisRoot.transform);

			// gridlinex.transform.localScale.y = NumberOfAxisPoints;
		}
	}
}
