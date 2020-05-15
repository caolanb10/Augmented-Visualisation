using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using TMPro;

/// <summary>
/// 
/// </summary>
public class VisualisationManager : MonoBehaviour
{
	StreamReader Stream;

	public Countries Countries;
	public AxisManager AxisManager;

	public TextMeshProUGUI Notifications_UI;

	public int NumberOfColumns;
	public int NumberOfRows;

	string[] ColumnNames;
	string[] Rows;

	bool HasStarted = false;
	bool IsPlaying = false;

	public float CurrentSpeed = 1.0f;

	// 2D grid/array of all data from the csv file
	public string[,] AllData;

	// All country data for the current day
	public string[,] CurrentDayData;

	// Country data for the last day (most up to date, used for creating axes)
	public string[,] LastDayData;

	float CurrentTime;
	float PreviousTime;

	public int CurrentDay;

	public static string DatasetPath = "Assets/Datasets/Filtered_dataset.csv";

	public float[] DataValuesMax = new float[Axis.NumberOfDirections] { 0, 0, 0 };

	#region PUBLIC_FUNCTIONS

	/// <summary>
	/// Function called by the play button, this begins the visualisation
	/// </summary>
	/// <param name="index"></param>
	public void BeginVisualisation()
	{
		ReadDataFromFile();

		CurrentDay = 0;

		Dates.PopulateDates();
		// Dates.DebugDates();

		// First day
		CurrentDayData = GetCountryDataFromDate(CurrentDay);

		// Last day data
		LastDayData = GetCountryDataFromDate(Dates.TotalDays - 1);

		FindHighestValues();

		AxisManager.CreateAxes(DataValuesMax);

		Countries.InstantiateCountries();
		
		// Start the visualisation
		Visualise();
	}

	/// <summary>
	/// Play the animated visualisation, called by the play UI button
	/// </summary>
	public void Play()
	{
		IsPlaying = true;
		if (!HasStarted)
		{
			HasStarted = true;
			BeginVisualisation();
			PreviousTime = Time.time;
		}
	}

	/// <summary>
	/// Pause the animated visualisation, called by the pause UI button
	/// </summary>
	public void Pause()
	{
		IsPlaying = false;
	}

	/// <summary>
	/// Will progress the animation if it is playing and stop
	/// (not progress) the animation if it is paused.
	/// </summary>
	public void Update()
	{
		if(IsPlaying)
		{
			CurrentTime = Time.time;
			if(CurrentTime >= PreviousTime + 1/CurrentSpeed)
			{
				// Move onto next day
				CurrentDayData = GetCountryDataFromDate(++CurrentDay);

				// Get date for displaying to the UI
				string datetime_string = Dates.GetDate(CurrentDay);
				Notifications_UI.text = datetime_string;

				// Notifications_UI.text
				PreviousTime = CurrentTime;
				// DebugData();
				Visualise();
			}
		}
		// When it reaches the end, stop visualisation and set status to not started
		if(CurrentDay == Dates.TotalDays)
		{
			IsPlaying = false;
			HasStarted = false;
		}
	}
	#endregion

	#region PRIVATE_FUNCTIONS
	/// <summary>
	/// Takes a path to a CSV file, parses it and stores it in memory in a 2-D Array.
	/// </summary>
	/// <param name="path"></param>
	void ReadDataFromFile()
	{
		if (File.Exists(DatasetPath))
		{
			Debug.Log("Found File");
		}
		else
		{
			Debug.Log("Not Found");
		}
		Stream = new StreamReader(DatasetPath);

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
			AllData = new string[NumberOfRows, NumberOfColumns];
		}

		void ReadData()
		{
			for (int i = 0; i < NumberOfRows - 1; i++)
			{
				int index = i + 1;
				string[] currentRow = Rows[index].Split(',');
				for (int j = 0; j < NumberOfColumns; j++)
				{
					AllData[i, j] = currentRow[j];
				}
			}
		}

		ReadHeaders();
		InitDataArray();
		ReadData();
	}

	/// <summary>
	/// Retrieves each country data from a given number of days past the start date.
	/// The last day has the highest amount (cumulative data) and is therefore used
	/// to find the max point for each axes
	/// </summary>
	/// <param name="daysFromBeginning"></param>
	string[,] GetCountryDataFromDate(int daysFromBeginning)
	{
		string [,] data = new string[Countries.NumberOfCountries, NumberOfColumns];
		for (int i = 0; i < Countries.NumberOfCountries; i++)
		{
			int rowOffset = (Dates.TotalDays * i) + daysFromBeginning;
			for (int j = 0; j < NumberOfColumns; j++)
			{
				data[i, j] = AllData[rowOffset, j];
			}
		}
		return data;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="index"></param>
	void Visualise()
	{
		string CountryName;
		float ConfirmedFigure;
		float RecoveredFigure;
		float DeathsFigure;

		// Numerical information is located in columns 2, 3, 4
		for (int i = 0; i < Countries.NumberOfCountries; i++)
		{
			CountryName = CurrentDayData[i, 1];
			ConfirmedFigure = float.Parse(CurrentDayData[i, 2]);
			RecoveredFigure = float.Parse(CurrentDayData[i, 3]);
			DeathsFigure = float.Parse(CurrentDayData[i, 4]);

			Countries.MoveCountryObject(CurrentDayData[i, 1], ConfirmedFigure, RecoveredFigure, DeathsFigure);
		}
	}

	void FindHighestValues()
	{
		float ConfirmedFigure;
		float RecoveredFigure;
		float DeathsFigure;

		for (int i = 0; i < (LastDayData.Length / NumberOfColumns); i++)
		{
			ConfirmedFigure = float.Parse(LastDayData[i, 2]);
			RecoveredFigure = float.Parse(LastDayData[i, 3]);
			DeathsFigure = float.Parse(LastDayData[i, 4]);

			DataValuesMax[0] = ConfirmedFigure > DataValuesMax[0] ? ConfirmedFigure : DataValuesMax[0];
			DataValuesMax[1] = RecoveredFigure > DataValuesMax[1] ? RecoveredFigure : DataValuesMax[1];
			DataValuesMax[2] = DeathsFigure > DataValuesMax[2] ? DeathsFigure : DataValuesMax[2];
		}
	}
	#endregion

	#region DEBUG
	/// <summary>
	/// For debugging purposes
	/// </summary>
	void DebugValuesMax()
	{
		for(int i = 0; i < DataValuesMax.Length; i++)
		{
			Debug.Log(DataValuesMax[i]);
		}
	}

	/// <summary>
	/// Function to print data to the console for debugging.
	/// </summary>
	void DebugData()
	{
		for (int i = 0; i < Countries.NumberOfCountries; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				Debug.Log(CurrentDayData[i, j]);
			}
		}
	}
	#endregion
}
