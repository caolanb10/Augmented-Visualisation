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

	public string[,] Data;

	string FilePath;

	static string DatasetDirectoryPath = "Assets/Datasets/";
	public static string[] DatasetPaths = new string[] {
		DatasetDirectoryPath + "attractions.csv",
		DatasetDirectoryPath + "test.csv"
	};

	public int[,] DataSetNumericalDataIndex = new int[2, Axis.NumberOfDirections]
	{ { 0 ,0, 0 }, { 0, 1, 2 } };


	StreamReader Stream;
	
	public void ReadHeaders()
	{
		ColumnNames = Rows[0].Split(',');
		NumberOfColumns = ColumnNames.Length;
	}

	public void ReadData()
	{
		for (int i = 0; i < NumberOfRows - 1; i++)
		{
			int index = i + 1;
			string[] currentRow = Rows[index].Split(',');
			for(int j = 0; j < NumberOfColumns; j++)
			{
				Data[i,j] = currentRow[j];
			}
		}
	}

	public void InitDataArray()
	{
		Data = new string[NumberOfRows, NumberOfColumns];
	}

	public void ChooseDataSet(int index)
	{
		ReadData(DatasetPaths[index]);
		VisualisationManager.Visualise(index);
	}

	public void ReadData(string path)
	{
		FilePath = path;
		if (File.Exists(FilePath))
		{
			Debug.Log("Found File");
		}
		else
		{
			Debug.Log("Not Found");
		}
		Stream = new StreamReader(FilePath);

		string fileContent = Stream.ReadToEnd();

		Rows = fileContent.Split('\n');
		NumberOfRows = Rows.Length - 1;

		ReadHeaders();
		InitDataArray();
		ReadData();
	}
}
