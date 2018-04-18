using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private ItemManager _cubeManager;
	// Use this for initialization
	void Start () {
		_cubeManager = new ItemManager();
		_cubeManager.CreateItemPickupCubes();
		// GameObject cube = Instantiate(Resources.Load(DataManager.GetPrefabPathByName("PickupCube"))) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
