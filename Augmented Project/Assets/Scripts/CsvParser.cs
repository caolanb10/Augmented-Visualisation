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

		string fileContent = Stream.ReadToEnd();

		Lines = fileContent.Split('\n');

		InitialiseHeaders();
	}
}
