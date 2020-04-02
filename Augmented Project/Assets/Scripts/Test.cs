using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public float[,] test = new float[4,10];

	public void TestPrint()
	{
		Debug.Log(test.LongLength);
	}
}
