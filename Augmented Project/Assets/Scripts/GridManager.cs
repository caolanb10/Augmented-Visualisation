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

	void DrawGridLinesABAC(Vector3 positionab, Vector3 positionac, Quaternion rotationab, Quaternion rotationac)
	{
		DrawGridLine(positionab, rotationab);
		DrawGridLine(positionac, rotationac);
	}

	void DrawGridLine(Vector3 position, Quaternion rotation)
	{
		GameObject gridline = GameObject.Instantiate(GridPrefab, position, rotation, GridRoot.transform);
		gridline.transform.localScale = GridScale;
	}

	public void ChangeGridEnabled()
	{
		GridRoot.SetActive(!GridRoot.activeSelf);
	}
}
