using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private ItemManager itemManager;
	// Use this for initialization
	void Start () {
		itemManager = GetComponent<ItemManager>();
		itemManager.CreateItemPickupCubes();
		// GameObject cube = Instantiate(Resources.Load(DataManager.GetPrefabPathByName("PickupCube"))) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
