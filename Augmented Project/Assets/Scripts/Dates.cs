using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains the dates from our dataset that pertains to the Covid-19 pandemic
/// </summary>
public class Dates : MonoBehaviour
{
	public static DateTime StartDate = new DateTime(2020, 1, 22);
	public static DateTime EndDate = new DateTime(2020, 5, 12);

	// Dates are inclusive for both start and end, so extra day must be added
	public static int TotalDays = (EndDate - StartDate).Days + 1;
	public static DateTime[] dates = new DateTime[TotalDays];

	public void PopulateDates()
	{
		for(int i = 0; i < TotalDays; i++)
		{
			dates[i] = StartDate + new TimeSpan(i, 0, 0);
		}
	}

	public void DebugDates()
	{
		foreach(DateTime d in dates)
		{
			Debug.Log(d);
		}
	}

	public DateTime LastDate()
	{
		return dates[TotalDays - 1];
	}
}
