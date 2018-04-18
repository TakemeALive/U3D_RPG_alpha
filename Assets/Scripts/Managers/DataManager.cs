using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager{

//--- Path
	private static string _prefabPath = "Prefabs/";

	public static string GetPrefabPathByName(string fileName)
	{
		return _prefabPath + fileName;
	}

//--- Data 
	public static int cubeAmount = 10; // Amount of the Pickup Cubes
}
