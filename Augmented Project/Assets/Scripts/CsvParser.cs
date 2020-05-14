using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using static Axis;
using static Dates;

/// <summary>
/// 
/// </summary>
public class CsvParser : MonoBehaviour
{
	// public VisualisationManager VisualisationManager;
	public int NumberOfColumns;
	public int NumberOfRows;

	string[] ColumnNames;
	string[] Rows;
	
	// 2D grid/array of all data from the csv file
	public string[,] Data;

	public int NumberOfCountries = 11;

	static string DatasetDirectoryPath = "Assets/Datasets/";

	// Datasets for test dataset (1) and COVID-19 Dataset (0)
	public static string[] DatasetPaths = new string[] {
		DatasetDirectoryPath + "Filtered_dataset.csv",
		DatasetDirectoryPath + "test.csv",
	};

	// Indices for the location of the numerical data within the 2d grid of data extracted from the CSV
	// Used for plotting the relevant numerical data.
	public int[,] DataSetNumericalDataIndex = new int[2, Axis.NumberOfDirections]
	{ { 2, 3, 4 }, { 0, 1, 2 } };

	/// <summary>
	/// Function called by the play button, this begins the visualisation
	/// </summary>
	/// <param name="index"></param>
	public void BeginVisualisation(int index)
	{
		ReadDataFromFile(DatasetPaths[index]);
		GetCountryDataFromDate(0);
		// VisualisationManager.Visualise(index);
	}

	StreamReader Stream;

	/// <summary>
	/// Takes a path to a CSV file, parses it and stores it in memory in a 2-D Array.
	/// </summary>
	/// <param name="path"></param>
	public void ReadDataFromFile(string path)
	{
		if (File.Exists(path))
		{
			Debug.Log("Found File");
		}
		else
		{
			Debug.Log("Not Found");
		}
		Stream = new StreamReader(path);

		string fileContent = Stream.ReadToEnd();

		Rows = fileContent.Split('\n');
		NumberOfRows = Rows.Length - 1;

		// Seperate reading of first line of a CSV (The headers) and the rest of the lines (The data)
		void ReadHeaders()
		{
			ColumnNames = Rows[0].Split(',');
			NumberOfColumns = ColumnNames.Length;
		}

		void InitDataArray()
		{
			Data = new string[NumberOfRows, NumberOfColumns];
		}

		void ReadData()
		{
			for (int i = 0; i < NumberOfRows - 1; i++)
			{
				int index = i + 1;
				string[] currentRow = Rows[index].Split(',');
				for (int j = 0; j < NumberOfColumns; j++)
				{
					Data[i, j] = currentRow[j];
				}
			}
		}

		ReadHeaders();
		InitDataArray();
		ReadData();
	}

	public void DebugData()
	{
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				Debug.Log(Data[i, j]);
			}
		}
	}

	public void GetCountryDataFromDate(int daysFromBeginning)
	{
		for(int i = 0; i < NumberOfCountries; i++)
		{
			int rowOffset = (Dates.TotalDays * i) + daysFromBeginning;
			Debug.Log("Offset" + rowOffset.ToString());

			// Should print all country names
			Debug.Log(Data[rowOffset, 0] + Data[rowOffset, 1]);
		}
	}
}

/* 
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
	}*/
