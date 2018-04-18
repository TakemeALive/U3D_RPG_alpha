using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public PlayerHealth playerHealth;
	public float spawnTime = 3f;
	public GameObject[] enemyArray;
	public Transform[] spawnPoints;

	bool isEnemyOnSpawning = false;
	void Start()
	{	
		// if(isEnemyOnSpawning){
			// InvokeRepeating("Spawn", spawnTime, spawnTime);
		// 	isEnemyOnSpawning = false;
		// }
		
	}

	void Update()
	{
		if(isEnemyOnSpawning){
			InvokeRepeating("Spawn", spawnTime, spawnTime);
			isEnemyOnSpawning = false;
		}
	}

	public void SetEnemySpawnActive(bool isActive){
		isEnemyOnSpawning = isActive;
	}

	void Spawn()
	{
		if(playerHealth.currentHealth <= 0f)
		{
			return;
		}

		int spawnPointIndex = Random.Range(0, spawnPoints.Length);
		int enemyIndex = Random.Range(0, enemyArray.Length);
		Instantiate(enemyArray[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
}
