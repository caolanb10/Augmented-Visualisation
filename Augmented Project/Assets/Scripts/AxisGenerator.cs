using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Axis;

public class AxisGenerator : MonoBehaviour
{
	// Prefab objects to be instantiated
	public GameObject AxisTopPrefab;
	public GameObject AxisPiecePrefab;
	public GameObject AxisHalfPiecePrefab;

	// Game objects actually existing in this prefab
	public GameObject AxisTop;
	public GameObject[] AxisPieces;

	public AxisDirection Direction;
	public float AxisPieceScale = 0.5f;

	int size;
	public float[] AxisPoints;

	int AmountToMove = 2;

	public void Draw(float[] vals, AxisDirection direction)
	{
		this.Direction = direction;
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
		MoveTopPiece();
		AddStartPiece();

		for (int i = 0; i < size; i++)
		{
			Vector3 position = new Vector3((i * AmountToMove) + 2, 0, 0);

			AxisPieces[i] = GameObject.Instantiate(AxisPiecePrefab, gameObject.transform, false);
			AxisPieces[i].transform.Translate(position);
			AxisPieces[i].GetComponentInChildren<AxisSetup>().AssignAxisLabel(AxisPoints[i]);

			//Rotate text for Y axis
			if (Direction == AxisDirection.Y)
			{
				AxisPieces[i].GetComponentInChildren<TextMeshPro>().gameObject.transform.Rotate(new Vector3(0, 0, -90.0f));
			}
		}
	}

	// Moves top piece (cone) to its correct distance from the origin
	public void MoveTopPiece()
	{
		Vector3 TopTransform = new Vector3((AmountToMove * size) + 2, 0, 0);
		AxisTop = GameObject.Instantiate(AxisTopPrefab, gameObject.transform, false);
		AxisTop.transform.Translate(TopTransform);
		TopTransform.y += size * AmountToMove;
	}

	// Places a half piece (a unit) after the origin
	public void AddStartPiece()
	{
		GameObject AxisHalfPiece = GameObject.Instantiate(AxisHalfPiecePrefab, gameObject.transform, false);
		AxisHalfPiece.transform.Translate(new Vector3(0.5F, 0, 0));
	}
}
