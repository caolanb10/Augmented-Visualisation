using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using static Axis;

public class CsvParser : MonoBehaviour
{
	public VisualisationManager VisualisationManager;
	public int NumberOfColumns;
	public int NumberOfRows;

	string[] ColumnNames;
	string[] Rows;
	
	// 2D grid/array of all data from the csv file
	public string[,] Data;

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

	// Function Called by the play button, this begins the visualisation
	public void BeginVisualisation(int index)
	{
		ReadDataFromFile(DatasetPaths[index]);
		VisualisationManager.Visualise(index);
	}

	StreamReader Stream;

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
}
