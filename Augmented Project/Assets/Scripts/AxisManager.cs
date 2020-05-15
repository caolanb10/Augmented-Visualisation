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

	// Reference to game objects that have hierarchy for given game object type
	public GameObject AxisRoot;

	// x, y, z axes
	public GameObject[] Axes;

	public bool DebugEnabled;

	public int NumberOfAxisPoints = 20;

	void Start()
	{
		GridManager = GetComponent<GridManager>();
	}

	/// <summary>
	/// Create the x, y and z axes using the max values found in the data set.
	/// </summary>
	public void CreateAxes(float[] maxValues)
	{
		// Instantiate the origin.
		GameObject.Instantiate(AxisOriginPrefab, Vector3.zero, Quaternion.identity, AxisRoot.transform);

		// GetHighestValues();
		Axes = new GameObject[3];

		CreateAxis(AxisDirection.X, Quaternion.identity);
		CreateAxis(AxisDirection.Y, Quaternion.Euler(0, 0, 90.0f));
		CreateAxis(AxisDirection.Z, Quaternion.Euler(0, -90.0f, 0));

		// Draw (Generate game objects and place them in their correct place)
		Axes[(int)AxisDirection.X].GetComponent<AxisGenerator>().Draw(AxisDirection.X, maxValues[0]);
		Axes[(int)AxisDirection.Y].GetComponent<AxisGenerator>().Draw(AxisDirection.Y, maxValues[1]);
		Axes[(int)AxisDirection.Z].GetComponent<AxisGenerator>().Draw(AxisDirection.Z, maxValues[2]);

		// Draw Grid
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
		Axes[(int)direction].GetComponent<AxisGenerator>().AxisManager = this;
	}
}
