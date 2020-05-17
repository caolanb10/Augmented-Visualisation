using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for creating a 3-Dimensional grid for the scatter plot.
/// 
/// (Not the most elegant of implementations but functional)
/// 
/// Used by the controls dropdown to enable/disable the grid.
/// </summary>
public class GridManager : MonoBehaviour
{
	// Reference to prefab object
	public GameObject GridPrefab;
	
	// Parent in hierarchy
	public GameObject GridRoot;

	Vector3 GridScale;

	int NumberOfAxisPoints;

	/// <summary>
	/// Draw the entire grid based on the number of axis pieces/points.
	/// </summary>
	/// <param name="points"></param>
	public void DrawGrid(int points)
	{
		NumberOfAxisPoints = points;

		GridScale = new Vector3(GridPrefab.transform.localScale.x, NumberOfAxisPoints, GridPrefab.transform.localScale.z);

		for (int i = 0; i < NumberOfAxisPoints; i++)
		{
			int offset = (i * 2) + 2;

			// Definition of all positions needed for the grid. (3 Choose 2, 3x2 = 6 combinations)
			// { (XY), (XZ), (YX), (YZ), (ZX), (Zy) }
			Vector3 positionXY = new Vector3(offset, NumberOfAxisPoints, 0);
			Vector3 positionXZ = new Vector3(offset, 0, NumberOfAxisPoints);
			Vector3 positionYX = new Vector3(NumberOfAxisPoints, offset, 0);
			Vector3 positionYZ = new Vector3(0, offset, NumberOfAxisPoints);
			Vector3 positionZX = new Vector3(NumberOfAxisPoints, 0, offset);
			Vector3 positionZY = new Vector3(0, NumberOfAxisPoints, offset);

			DrawGridLinesABAC(positionXY, positionXZ, Quaternion.identity, Quaternion.Euler(0, 90, 90));
			DrawGridLinesABAC(positionYX, positionYZ, Quaternion.Euler(0, 0, 90), Quaternion.Euler(0, 90, 90));
			DrawGridLinesABAC(positionZX, positionZY, Quaternion.Euler(0, 0, 90), Quaternion.identity);

			for(int j = 0; j <= NumberOfAxisPoints; j++)
			{
				int offset2 = (j * 2);

				DrawGridLinesABAC(
					new Vector3(positionXZ.x, offset2, positionXZ.z), 
					new Vector3(positionZX.x, offset2, positionZX.z), 
					Quaternion.Euler(0, 90, 90),
					Quaternion.Euler(0, 0, 90)
					);

				DrawGridLine(new Vector3(positionXY.x, positionXY.y, offset2), Quaternion.identity);
			}
		}
	}

	/// <summary>
	/// Draw grid lines for given combo XY, XZ etc.
	/// </summary>
	/// <param name="positionab"></param>
	/// <param name="positionac"></param>
	/// <param name="rotationab"></param>
	/// <param name="rotationac"></param>
	void DrawGridLinesABAC(Vector3 positionab, Vector3 positionac, Quaternion rotationab, Quaternion rotationac)
	{
		DrawGridLine(positionab, rotationab);
		DrawGridLine(positionac, rotationac);
	}

	/// <summary>
	/// Draw individual gridline with position and rotation
	/// </summary>
	/// <param name="position"></param>
	/// <param name="rotation"></param>
	void DrawGridLine(Vector3 position, Quaternion rotation)
	{
		GameObject gridline = 
			GameObject.Instantiate(GridPrefab, (GridRoot.transform.position + position), rotation, GridRoot.transform);
		gridline.transform.localScale = GridScale;
	}

	/// <summary>
	/// Accessed through the controls dropdown.
	/// Used for enabling and disabling the grid hierarchy for quickly enabling and
	/// disabling the grid during visualisation
	/// </summary>
	public void ChangeGridEnabled()
	{
		GridRoot.SetActive(!GridRoot.activeSelf);
	}
}
