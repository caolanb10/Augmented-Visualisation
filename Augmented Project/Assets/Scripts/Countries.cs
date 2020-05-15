using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countries : MonoBehaviour
{
	public const int NumberOfCountries = 11;

	public AxisManager AxisManager;
	public VisualisationManager VisualisationManager;

	public GameObject DataPointRoot;

	// Reference to Prefabs to be instantiated
	public GameObject[] OrbPrefabs = new GameObject[NumberOfCountries];

	// Actual game objects in the scene that have been instantiated from the prefabs
	GameObject[] CountryGameObjects = new GameObject[NumberOfCountries];

	// Enumerates countries to indices 0 - 10
	public enum CountriesEnum
	{
		Brazil,
		France,
		Germany,
		Iran,
		Ireland,
		Italy,
		Russia,
		Spain,
		Turkey,
		US,
		UK,
	}

	/// <summary>
	/// Instantiate game objects into scene from prefabs
	/// </summary>
	public void InstantiateCountries()
	{
		for(int i = 0; i < NumberOfCountries; i++)
		{
			CountryGameObjects[i] = GameObject.Instantiate(OrbPrefabs[i], new Vector3(0, 0, 0), 
				Quaternion.identity, DataPointRoot.transform);
		}
	}

	public void MoveCountryObject(string Country, float ConfirmedFigure, float RecoveredFigure, float DeathsFigure)
	{
		if (Country == "United Kingdom") Country = "UK";
		int index = (int) Enum.Parse(typeof(CountriesEnum), Country);

		CountryGameObjects[index].GetComponent<DataPointManager>()
			.SetLabel(ConfirmedFigure, RecoveredFigure, DeathsFigure);

		Vector3 countryGameObjectPosition = CountryGameObjects[index].transform.position;

		Vector3 newPos = new Vector3(
				(ConfirmedFigure / VisualisationManager.DataValuesMax[0]) * AxisManager.NumberOfAxisPoints * 2,
				(RecoveredFigure / VisualisationManager.DataValuesMax[1]) * AxisManager.NumberOfAxisPoints * 2,
				(DeathsFigure / VisualisationManager.DataValuesMax[2]) * AxisManager.NumberOfAxisPoints * 2
			);

		// Vector3 newPos = Vector3.MoveTowards(countryGameObjectPosition, new Vector3(x, y, z), 10);

		CountryGameObjects[index].transform.position = newPos;
	}

	/// <summary>
	/// For each country, enable/disable the data annotation for the visualisation
	/// </summary>
	public void ChangeLabelsEnabled()
	{
		foreach(GameObject country in CountryGameObjects)
		{
			country.GetComponentInChildren<DataPointManager>().ChangePopupStatus();
		}
	}

	/// <summary>
	/// Testing enumerations
	/// </summary>
	public void Test()
	{
		MoveCountryObject("Brazil", 0, 0, 0);
		MoveCountryObject("France", 0, 0, 0);
		MoveCountryObject("Germany", 0, 0, 0);
		MoveCountryObject("Iran", 0, 0, 0);
		MoveCountryObject("Ireland", 0, 0, 0);
		MoveCountryObject("Italy", 0, 0, 0);
		MoveCountryObject("Russia", 0, 0, 0);
		MoveCountryObject("Spain", 0, 0, 0);
		MoveCountryObject("Turkey", 0, 0, 0);
		MoveCountryObject("US", 0, 0, 0);
		MoveCountryObject("United Kingdom", 0, 0, 0);
	}
}
