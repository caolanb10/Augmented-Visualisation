using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Axis;

public class VisualisationManager : MonoBehaviour
{
	public AxisManager AxisManager;
	public CsvParser CsvParser;

	public void Visualise(int index)
	{
		AxisManager.DataValues = new float[
			CsvParser.Data.Length/Axis.NumberOfDirections,
			Axis.NumberOfDirections
		];

		int[] indices = {
			CsvParser.DataSetNumericalDataIndex[index, (int) AxisDirection.X],
			CsvParser.DataSetNumericalDataIndex[index, (int) AxisDirection.Y],
			CsvParser.DataSetNumericalDataIndex[index, (int) AxisDirection.Z]
		};

		for (int i = 0; i < CsvParser.NumberOfRows - 1; i++)
		{
			AxisManager.DataValues[i, 0] = float.Parse(CsvParser.Data[indices[i], (int)AxisDirection.X]);
			AxisManager.DataValues[i, 1] = float.Parse(CsvParser.Data[indices[i], (int)AxisDirection.Y]);
			AxisManager.DataValues[i, 2] = float.Parse(CsvParser.Data[indices[i], (int)AxisDirection.Z]);
		}

		AxisManager.CreateAxes();
		AxisManager.PlotAllPoints();
	}
}
