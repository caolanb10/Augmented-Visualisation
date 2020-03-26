using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class CsvParser : MonoBehaviour
{
	int Columns;

	string[] Headers;

	string[] Lines;

	string FilePath;

	static string DatasetDirectoryPath = "Assets/Datasets";
	static string[] DatasetNames = new string[] { "attractions.csv", "test.csv" };
	string[] DatasetPaths = new string[] {
		DatasetDirectoryPath + DatasetNames[0],
		DatasetDirectoryPath + DatasetNames[1]
	};

	StreamReader Stream;
	
	public void InitialiseHeaders()
	{
		Headers = Lines[0].Split(',');
		Columns = Headers.Length;
	}

	public void TestFile()
	{
		ReadData("Assets/Datasets/attractions.csv");
	}

	public void ReadData(string path)
	{
		Debug.Log(Directory.GetCurrentDirectory());

		FilePath = path;
		if (File.Exists(FilePath))
		{
			Debug.Log("Found File");
		}
		else
		{
			Debug.Log("Not Found");
		}
		Stream = new StreamReader(path);

		InitialiseHeaders();

		string fileContent = Stream.ReadToEnd();

		Lines = fileContent.Split('\n');

	}
}
