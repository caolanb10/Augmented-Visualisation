using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Axis;

public class AxisGenerator : MonoBehaviour
{
	// Prefab objects to be instantiated
	public GameObject AxisTopPrefab;
	public GameObject AxisPiecePrefab;

	// Game objects actually existing in this prefab
	public GameObject AxisTop;
	public GameObject[] AxisPieces;

	public AxisDirection Direction;
	public float AxisPieceScale = 0.5f;

	int size;
	public int[] AxisPoints;

	int AmountToMove = 2;

	public void Draw(int[] vals, AxisDirection direction)
	{
		this.Direction = direction;
		if(direction == AxisDirection.Y)
		{

		}
		if (direction == AxisDirection.Z)
		{

		}
		AxisPoints = vals;
		InstantiateGameObjects();
		CreateAxis();
	}

	public void InstantiateGameObjects()
	{
		size = AxisPoints.Length;
		AxisPieces = new GameObject[size];
	}

	public void CreateAxis()
	{
		Vector3 TopTransform = new Vector3((AmountToMove * size) + 1, 0, 0);

		AxisTop = GameObject.Instantiate(AxisTopPrefab, gameObject.transform, false);
		AxisTop.transform.Translate(TopTransform);

		TopTransform.y += size * AmountToMove;

		for (int i = 0; i < size; i++)
		{
			Vector3 position = new Vector3((i * AmountToMove) + 1, 0, 0);

			AxisPieces[i] = GameObject.Instantiate(AxisPiecePrefab, gameObject.transform, false);
			AxisPieces[i].transform.Translate(position);
			AxisPieces[i].GetComponentInChildren<AxisSetup>().AssignAxisLabel(AxisPoints[i]);
		}
	}
}
