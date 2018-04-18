using System.Collections;
using System.Collections.Generic;
using UnityEngine.Internal;
using UnityEngine;

public class ItemManager{

	private GameObject[] _items;
	// Use this for initialization
	public void Setup () 
	{
		
	}

	public void CreateItemPickupCubes()
	{
		_items = new GameObject[DataManager.cubeAmount];
		for(int i = 0; i < DataManager.cubeAmount; i++)
		{
			_items[i] = GameObject.Instantiate(Resources.Load(DataManager.GetPrefabPathByName("PickupCube"))) as GameObject;
			SetRandomPosition(_items[i]);
		}
	}

	public void SetRandomPosition(GameObject item)
	{
		if(item && item.activeSelf)
		{
			// TODO: obj(position) overlaps with walls
			GameObject currentGround = GameObject.FindGameObjectWithTag("Ground");
			Vector3 groundSize = currentGround.GetComponent<MeshCollider>().bounds.size;

			float keepDistanceToWall = 0.9f;
			Vector3 itemPosition = new Vector3(
				Random.Range(-groundSize.x / 2, groundSize.x / 2) * keepDistanceToWall,
				1,
				Random.Range(-groundSize.z / 2, groundSize.z / 2) * keepDistanceToWall);							
			item.transform.position = itemPosition;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
