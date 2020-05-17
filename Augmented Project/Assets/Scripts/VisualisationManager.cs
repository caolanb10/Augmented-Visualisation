using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

/// <summary>
/// Class for managing the visualisation. 
/// 
/// Contains logic for reading the timeseries data from a csv file.
/// 
/// Will update the country game objects in the country class with the data from the CSV.
/// 
/// The date will change at the rate of CurrentSpeed, which is set through SpeedControlSettings class
/// 
/// Has media controls for advancing(fast forwarding) or retreating (rewinding) the date by 5 days. 
/// 
/// Also has pause controls to stop updating the date and subsequently the position of the countries.
/// 
/// Updates the UI with the current date for added contextualisation and information.
/// 
/// Takes data from the most recent day and uses that to create the axes for the visualisation
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

	public static string DatasetPath = "Filtered_dataset.csv";
	public static string DatasetAdjustedPopulationPath = "Filtered_dataset_population.csv";

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
	/// Rewind the animation by 5 days
	/// </summary>
	public void Rewind()
	{
		CurrentDay -= 5;
		if (CurrentDay < 0) CurrentDay = 0;

		CurrentDayData = GetCountryDataFromDate(CurrentDay);
		UpdateUI();
		Visualise();
	}

	/// <summary>
	/// Progress the animation by 5 days
	/// </summary>
	public void FastForward()
	{
		CurrentDay += 5;
		if (CurrentDay >= Dates.TotalDays) CurrentDay = Dates.TotalDays - 1;

		CurrentDayData = GetCountryDataFromDate(CurrentDay);
		UpdateUI();
		Visualise();
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
				CurrentDay++;

				// When it reaches the end, stop visualisation and set status to not started
				if (CurrentDay == Dates.TotalDays)
				{
					IsPlaying = false;
					HasStarted = false;
					return;
				}

				CurrentDayData = GetCountryDataFromDate(CurrentDay);
				UpdateUI();
				PreviousTime = CurrentTime;
				Visualise();
			}
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
		BetterStreamingAssets.Initialize();

		if (BetterStreamingAssets.FileExists(DatasetAdjustedPopulationPath))
		{
			Debug.Log("Found File");
			
			// Better streaming assets is a library for accessing streaming assets for android builds
			// developed using unity.  (Can't access the filesystem directly)
			byte[] allData = BetterStreamingAssets.ReadAllBytes(DatasetAdjustedPopulationPath);
			string fileContent = System.Text.Encoding.UTF8.GetString(allData, 0, allData.Length);

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
		else
		{
			Debug.Log("File Not Found");
		}
	}

	/// <summary>
	/// Updates the notification bar with a formatted string of the current date
	/// to support the user through the animated visualisation
	/// </summary>
	void UpdateUI()
	{
		// Get date for displaying to the UI
		string datetime_string = Dates.GetDate(CurrentDay);
		Notifications_UI.text = datetime_string;
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
			ConfirmedFigure = float.Parse(CurrentDayData[i, 5]);
			RecoveredFigure = float.Parse(CurrentDayData[i, 6]);
			DeathsFigure = float.Parse(CurrentDayData[i, 7]);

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
			ConfirmedFigure = float.Parse(LastDayData[i, 5]);
			RecoveredFigure = float.Parse(LastDayData[i, 6]);
			DeathsFigure = float.Parse(LastDayData[i, 7]);

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
