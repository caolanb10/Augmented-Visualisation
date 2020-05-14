using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Axis;

/// <summary>
/// 
/// </summary>
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

	// x, y, z axes
	public GameObject[] Axes;

	public bool DebugEnabled;

	public int NumberOfAxisPoints = 20;

	// { maxX, maxY, maxZ }
	public float[] DataValuesMax = new float[Axis.NumberOfDirections] { 0, 0, 0 };
	
	// { {x, y, z}, {x, y, z} } \\
	public float[,] DataValues;

	void Start()
	{
		GridManager = GetComponent<GridManager>();
	}

	/// <summary>
	/// 
	/// </summary>
	public void CreateAxes()
	{
		// Instantiate the origin.
		GameObject.Instantiate(AxisOriginPrefab, Vector3.zero, Quaternion.identity, AxisRoot.transform);

		GetHighestValues();
		Axes = new GameObject[3];

		CreateAxis(AxisDirection.X, Quaternion.identity);
		CreateAxis(AxisDirection.Y, Quaternion.Euler(0, 0, 90.0f));
		CreateAxis(AxisDirection.Z, Quaternion.Euler(0, -90.0f, 0));

		Axes[(int)AxisDirection.X].GetComponent<AxisGenerator>()
			.Draw(CreateAxisPointsFromMax(DataValuesMax[0]), AxisDirection.X);

		Axes[(int)AxisDirection.Y].GetComponent<AxisGenerator>()
			.Draw(CreateAxisPointsFromMax(DataValuesMax[1]), AxisDirection.Y);

		Axes[(int)AxisDirection.Z].GetComponent<AxisGenerator>()
			.Draw(CreateAxisPointsFromMax(DataValuesMax[2]), AxisDirection.Z);

		GridManager.DrawGrid(NumberOfAxisPoints);
	}

	/// <summary>
	/// Instantiate the axis prefab for a given direction
	/// </summary>
	/// <param name="direction"></param>
	/// <param name="rotation"></param>
	void CreateAxis(AxisDirection direction, Quaternion rotation)
	{
		Axes[(int)direction] = GameObject.Instantiate(AxisPrefab, Vector3.zero, rotation, AxisRoot.transform);
	}

	/// <summary>
	/// From our 2D array of data, get the max point for each axis which will serve 
	/// as the max point on the respective axis.
	/// </summary>
	void GetHighestValues()
	{
		for (int i = 0; i < (DataValues.Length/3); i++)
		{
			DataValuesMax[0] = DataValues[i, 0] > DataValuesMax[0] ? DataValues[i, 0] : DataValuesMax[0];
			DataValuesMax[1] = DataValues[i, 1] > DataValuesMax[1] ? DataValues[i, 1] : DataValuesMax[1];
			DataValuesMax[2] = DataValues[i, 2] > DataValuesMax[2] ? DataValues[i, 2] : DataValuesMax[2];
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public void PlotAllPoints()
	{
		for (int i = 0; i < DataValues.Length / 3; i++)
		{
			PlotGameObject(DataValues[i, 0], DataValues[i, 1], DataValues[i, 2]);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="z"></param>
	public void PlotGameObject(float x, float y, float z)
	{
		GameObject point = GameObject.Instantiate(DataPointPrefab, 
			new Vector3(
				(x / DataValuesMax[0]) * NumberOfAxisPoints * 2,
				(y / DataValuesMax[1]) * NumberOfAxisPoints * 2,
				(z / DataValuesMax[2]) * NumberOfAxisPoints * 2
			),
			Quaternion.identity, 
			DataPointRoot.transform);

		// TODO add the data to the game objects script.
		// point.GetComponent<>()
	}
}
