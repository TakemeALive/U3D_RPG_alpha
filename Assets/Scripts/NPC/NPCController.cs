using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour {

	public enum State{
		IDLE,
		PATROL,
		CHASE
	}
	public State state;

	NavMeshAgent agent;
	public float interactableRadius = 3f;
	bool isPlayerInRange = false;
	bool isInteracting = false;
	GameObject player;
	GameObject npc;
	public Transform[] paths;
	int currentPathIndex = 0;
	 

	void Start()
	{
		// This Controller set PATROL as the default state for Lu(The NPC) to patrol on preset path
		// May need to adjust after we have other NPCs

		// BUG(Animation): Lu's accessories animation bug when moving;
		state = State.PATROL;

		agent = GetComponent<NavMeshAgent>();

		player = GameObject.FindGameObjectWithTag("Player");
		npc = this.gameObject;
	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject == player) 
		{
			PlayerInRangeForInteraction(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject == player) 
		{ 
			PlayerInRangeForInteraction(false);
		}
		
	}

	void Update()
	{
		// Get Positions and ignore the vertical distance
		Vector3 npcVec = npc.transform.position;
		Vector3 pathVec = paths[currentPathIndex].position;
		npcVec.y = 0;
		pathVec.y = 0;
		if(Vector3.Distance(npcVec, pathVec) <= 0){
			currentPathIndex += 1;
			if(currentPathIndex >= paths.Length) currentPathIndex = 0;
		}
		if(!isPlayerInRange){
			agent.SetDestination(paths[currentPathIndex].position);
		}else{
			// Set NPC's direction to player
			Vector3 direction = (player.transform.position - npc.transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
			npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, lookRotation, Time.deltaTime * 10f);
		}

		// Dialog interaction with player
		// Press E to control
		if(Input.GetKeyDown(KeyCode.E) && !isInteracting){
			InteractWithPlayer(true);
		}else if(Input.GetKeyDown(KeyCode.E) && isInteracting){
			InteractWithPlayer(false);
		}
		

	}

	void PlayerInRangeForInteraction(bool isInRange){
		if(isInRange){
			isPlayerInRange = true;
			agent.enabled = false;
			GetComponent<Animator>().enabled = false;

			TextManager.instance.OpenInteractionHelp();
		}else{
			isPlayerInRange = false;
			agent.enabled = true;
			GetComponent<Animator>().enabled = true;
			TextManager.instance.CloseInteractionHelp();
		}
	}

	void InteractWithPlayer(bool isDoing){
		PlayerRangedAttack playerAttackController = player.GetComponentInChildren<PlayerRangedAttack>();
		isInteracting = isDoing;
		if(isInteracting){
			playerAttackController.SetAttackEnable(false);
			TextManager.instance.OpenDialog();
		}else{
			playerAttackController.SetAttackEnable(true);
			TextManager.instance.CloseDialog();
		}
	}

	// void Idle()
	// {

	// }
	
	// void Patrol(){

	// }

	// void Chase(){

	// } 
}
