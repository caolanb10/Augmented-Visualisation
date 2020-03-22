using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	public GameObject GridPrefab;

	Vector3 GridScale;

	public GameObject GridRoot;

	int NumberOfAxisPoints;

	public void DrawGrid(int points)
	{
		NumberOfAxisPoints = points;

		GridScale = new Vector3(GridPrefab.transform.localScale.x, NumberOfAxisPoints, GridPrefab.transform.localScale.z);

		for (int i = 0; i < NumberOfAxisPoints; i++)
		{
			int offset = (i * 2) + 2;

			Vector3 positionXY = new Vector3(offset, NumberOfAxisPoints, 0);
			Vector3 positionXZ = new Vector3(offset, 0, NumberOfAxisPoints);
			Vector3 positionYX = new Vector3(NumberOfAxisPoints, offset, 0);
			Vector3 positionYZ = new Vector3(0, offset, NumberOfAxisPoints);
			Vector3 positionZX = new Vector3(NumberOfAxisPoints, 0, offset);
			Vector3 positionZY = new Vector3(0, NumberOfAxisPoints, offset);

			DrawGridLine(positionXY, positionXZ, Quaternion.identity, Quaternion.Euler(0, 90, 90));
			DrawGridLine(positionYX, positionYZ, Quaternion.Euler(0, 0, 90), Quaternion.Euler(0, 90, 90));
			DrawGridLine(positionZX, positionZY, Quaternion.Euler(0, 0, 90), Quaternion.identity);

			for(int j = 0; j <= NumberOfAxisPoints; j++)
			{
				int offset2 = (j * 2);

				DrawGridLine(
					new Vector3(positionXZ.x, offset2, positionXZ.z), 
					new Vector3(positionZX.x, offset2, positionZX.z), 
					Quaternion.Euler(0, 90, 90),
					Quaternion.Euler(0, 0, 90)
					);

				GameObject line = GameObject.Instantiate(GridPrefab, new Vector3(positionXY.x, positionXY.y, offset2), Quaternion.identity, GridRoot.transform);

				line.transform.localScale = GridScale;
			}
		}
	}

	void DrawGridLine(Vector3 positionab, Vector3 positionac, Quaternion rotationab, Quaternion rotationac)
	{
		GameObject gridlineab = GameObject.Instantiate(GridPrefab, positionab, rotationab, GridRoot.transform);
		GameObject gridlineac = GameObject.Instantiate(GridPrefab, positionac, rotationac, GridRoot.transform);

		gridlineab.transform.localScale = GridScale;
		gridlineac.transform.localScale = GridScale;
	}

	public void ChangeGridEnabled()
	{
		GridRoot.SetActive(!GridRoot.activeSelf);
	}
}
