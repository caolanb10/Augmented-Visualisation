using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that represents the countries selected for this visualisation.
/// The top 10 countries that had coronavirus cases were selected as well as Ireland.
/// </summary>
public class Countries : MonoBehaviour
{
	public const int NumberOfCountries = 11;

	public AxisManager AxisManager;
	public VisualisationManager VisualisationManager;

	// Parent Object
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

	/// <summary>
	/// Move country game object to its position in the scatter plot, called in VisualisationManager
	/// as the dates change. 
	/// </summary>
	/// <param name="Country"></param>
	/// <param name="ConfirmedFigure"></param>
	/// <param name="RecoveredFigure"></param>
	/// <param name="DeathsFigure"></param>
	public void MoveCountryObject(string Country, float ConfirmedFigure, float RecoveredFigure, float DeathsFigure)
	{
		// Convert United Kingdom to a single word enum value.
		if (Country == "United Kingdom") Country = "UK";
		int index = (int) Enum.Parse(typeof(CountriesEnum), Country);

		// Update relevant country game object annotation
		CountryGameObjects[index].GetComponent<DataPointManager>()
			.SetLabel(ConfirmedFigure, RecoveredFigure, DeathsFigure);

		// Convert data values to points in the visualisation.
		Vector3 newPos = new Vector3(
				(ConfirmedFigure / VisualisationManager.DataValuesMax[0]) * AxisManager.NumberOfAxisPoints * 2,
				(RecoveredFigure / VisualisationManager.DataValuesMax[1]) * AxisManager.NumberOfAxisPoints * 2,
				(DeathsFigure / VisualisationManager.DataValuesMax[2]) * AxisManager.NumberOfAxisPoints * 2
			);

		// Also add position of parent for moving visualisation by the user.
		CountryGameObjects[index].transform.position = (newPos + DataPointRoot.transform.position);
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
}
