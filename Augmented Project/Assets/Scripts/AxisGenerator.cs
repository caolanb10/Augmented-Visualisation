using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Axis;

/// <summary>
/// AxisGenerator class is used to dynamically create the game objects used for a single axis. 
/// </summary>
public class AxisGenerator : MonoBehaviour
{
	// References to prefabs
	public GameObject AxisTopPrefab;
	public GameObject AxisPiecePrefab;
	public GameObject AxisHalfPiecePrefab;

	// Game objects instantiated from prefabs
	public GameObject AxisTop;
	public GameObject[] AxisPieces;

	public AxisDirection Direction;
	public float AxisPieceScale = 0.5f;
	public float[] AxisPoints;

	int size;
	int AmountToMove = 2;

	/// <summary>
	/// Accessor function accessed by AxisManager class to create the individual x, y and z axes.
	/// </summary>
	/// <param name="vals"></param>
	/// <param name="direction"></param>
	public void Draw(float[] vals, AxisDirection direction)
	{
		this.Direction = direction;
		AxisPoints = vals;
		InstantiateGameObjects();
		CreateAxis();
	}

	/// <summary>
	/// Instantiates array of axis pieces to be the same size as the number of desired axis points.
	/// </summary>
	void InstantiateGameObjects()
	{
		size = AxisPoints.Length;
		AxisPieces = new GameObject[size];
	}

	/// <summary>
	/// Creates a single axis (x, y or z) by attaching a top piece, a start piece and individual axis pieces.
	/// </summary>
	void CreateAxis()
	{

		/// <summary>
		/// Moves top piece (cone) to its correct distance from the origin
		/// </summary>
		void MoveTopPiece()
		{
			Vector3 TopTransform = new Vector3((AmountToMove * size) + 2, 0, 0);
			AxisTop = GameObject.Instantiate(AxisTopPrefab, gameObject.transform, false);
			AxisTop.transform.Translate(TopTransform);
			TopTransform.y += size * AmountToMove;
		}

		/// <summary>
		/// Places a half piece (a unit) after the origin
		/// </summary> 
		void AddStartPiece()
		{
			GameObject AxisHalfPiece = GameObject.Instantiate(AxisHalfPiecePrefab, gameObject.transform, false);
			AxisHalfPiece.transform.Translate(new Vector3(0.5F, 0, 0));
		}

		MoveTopPiece();
		AddStartPiece();

		// Places individual axis pieces between the start piece and the top piece.
		for (int i = 0; i < size; i++)
		{
			Vector3 position = new Vector3((i * AmountToMove) + 2, 0, 0);

			AxisPieces[i] = GameObject.Instantiate(AxisPiecePrefab, gameObject.transform, false);
			AxisPieces[i].transform.Translate(position);
			AxisPieces[i].GetComponentInChildren<AxisPieceSetup>().AssignAxisLabel(AxisPoints[i]);

			//Rotate text for Y axis
			if (Direction == AxisDirection.Y)
			{
				AxisPieces[i].GetComponentInChildren<TextMeshPro>().gameObject.transform.Rotate(new Vector3(0, 0, -90.0f));
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="max"></param>
	/// <returns></returns>
	float[] CreateAxisPointsFromMax(float max)
	{
		float[] axisPoints = new float[NumberOfAxisPoints];
		for (int i = 0; i < NumberOfAxisPoints; i++)
		{
			axisPoints[i] = (max / NumberOfAxisPoints) * (i + 1);
			if (DebugEnabled) Debug.Log("Point " + i + " is " + axisPoints[i]);
		}
		return axisPoints;
	}
}
