using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Axis;

public class AxisManager : MonoBehaviour
{
	GridManager GridManager;

	// Prefabs_Axis
	public GameObject AxisPrefab;
	public GameObject AxisOriginPrefab;

	// Prefabs_DataPoints
	public GameObject DataPointPrefab;

	// Reference to game objects that have hierarchy for given game object type
	public GameObject AxisRoot;
	public GameObject DataPointRoot;

	public bool DebugEnabled;

	// x, y, z axes
	public GameObject[] Axes;

	public int NumberOfAxisPoints = 20;


	float HighestValueX;
	float HighestValueY;
	float HighestValueZ;

	float[] XValues = { 2, 4, 6, 8, 10, 12, 14, 16, 18 };
	float[] YValues = { 100, 200, 300, 400, 500 };
	float[] ZValues = { 0.4f, 0.6f, 0.8f };

	void Start()
	{
		GridManager = GetComponent<GridManager>();
	}

	public void CreateAxes()
	{
		HighestValueX = HighestValue(XValues);
		HighestValueY = HighestValue(YValues);
		HighestValueZ = HighestValue(ZValues);

		// Origin
		GameObject.Instantiate(AxisOriginPrefab, Vector3.zero, Quaternion.identity, AxisRoot.transform);
		Axes = new GameObject[3];

		CreateAxis(AxisDirection.X, Quaternion.identity);
		CreateAxis(AxisDirection.Y, Quaternion.Euler(0, 0, 90.0f));
		CreateAxis(AxisDirection.Z, Quaternion.Euler(0, -90.0f, 0));

		Axes[(int)AxisDirection.X].GetComponent<AxisGenerator>().Draw(CreateAxisPointsFromMax(HighestValue(XValues)), AxisDirection.X);
		Axes[(int)AxisDirection.Y].GetComponent<AxisGenerator>().Draw(CreateAxisPointsFromMax(HighestValue(YValues)), AxisDirection.Y);
		Axes[(int)AxisDirection.Z].GetComponent<AxisGenerator>().Draw(CreateAxisPointsFromMax(HighestValue(ZValues)), AxisDirection.Z);

		GridManager.DrawGrid(NumberOfAxisPoints);
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

	public void SamplePlot()
	{
		PlotGameObject(9.0f, 250.0f, 0.4f);
	}

	public void PlotGameObject(float x, float y, float z)
	{
		Debug.Log("highest x" + HighestValueX);
		Debug.Log("highest y" + HighestValueY);
		Debug.Log("highest z" + HighestValueZ);

		float displacementx = (x / HighestValueX) * NumberOfAxisPoints * 2;
		float displacementy = (y / HighestValueY) * NumberOfAxisPoints * 2;
		float displacementz = (z / HighestValueZ) * NumberOfAxisPoints * 2;

		GameObject.Instantiate(DataPointPrefab, new Vector3(displacementx, displacementy, displacementz), 
			Quaternion.identity, DataPointRoot.transform);
	}
}
