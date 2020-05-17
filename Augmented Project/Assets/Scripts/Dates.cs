using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains the dates from our dataset that pertains to the Covid-19 pandemic
/// </summary>
public class Dates : MonoBehaviour
{
	// From Dataset
	public static DateTime StartDate = new DateTime(2020, 1, 22);
	public static DateTime EndDate = new DateTime(2020, 5, 15);

	// Dates are inclusive for both start and end, so extra day must be added
	public static int TotalDays = (EndDate - StartDate).Days + 1;
	public static DateTime[] dates = new DateTime[TotalDays];

	/// <summary>
	/// Generate the datetimes for the dates in our dataset.
	/// This is used for the GetDate function below.
	/// </summary>
	public static void PopulateDates()
	{
		for(int i = 0; i < TotalDays; i++)
		{
			dates[i] = StartDate + new TimeSpan(24 * i, 0, 0);
		}
	}

	/// <summary>
	/// Retrieves the last date
	/// </summary>
	/// <returns></returns>
	public static DateTime LastDate()
	{
		return dates[TotalDays - 1];
	}

	/// <summary>
	/// Retrieves date object from this class given number of days since the beginning
	/// Returns a formatted string used on the UI to support the user through the animation.
	/// </summary>
	/// <param name="daysFromStart"></param>
	/// <returns></returns>
	public static string GetDate(int daysFromStart)
	{
		DateTime thisDate = dates[daysFromStart];
		string[] dateString = thisDate.ToString("F").Split(' ');
		string formatString = dateString[1] + " " + dateString[2] + " " + dateString[3];
		return formatString;
	}
}
