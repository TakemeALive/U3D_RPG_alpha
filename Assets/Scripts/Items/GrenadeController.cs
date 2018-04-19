using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour {

	public float delay = 3f;
	public float radius = 10f;
	public float force = 500f;
	public int damage = 1000;
	public GameObject explosionEffect;

	float countdown;
	bool hasExploded = false;

	int shootableMask;
	// Use this for initialization
	void Start () {
		countdown = delay;
		shootableMask = LayerMask.GetMask("Shootable");
	}
	
	// Update is called once per frame
	void Update () {
		countdown -= Time.deltaTime;
		if(countdown <= 0f && !hasExploded){
			Explode();
			hasExploded = true;
		}
	}

	void ExplodeOnItem(){
		Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);
		foreach(var obj in collidersToDestroy){
			Destructible destObj = obj.GetComponent<Destructible>();
			if(destObj != null){
				destObj.Destroy();
			}
		}

		Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);
		foreach(var obj in collidersToMove){
			var rb = obj.GetComponent<Rigidbody>();
			if(rb != null){
				rb.AddExplosionForce(force, transform.position, radius);
			}
		}
	}

	void ExplodeOnEnemy(){
		Collider[] enemyColliders = Physics.OverlapSphere(transform.position, radius, shootableMask);
		foreach(var obj in enemyColliders){
			var rb = obj.GetComponent<Rigidbody>();
			var enemyHealth = obj.GetComponent<EnemyHealth>();
			rb.AddExplosionForce(force, transform.position, radius);
			enemyHealth.TakeDamage(damage, obj.transform.position);
		}
	}

	void Explode(){
		var effect = Instantiate(explosionEffect, transform.position, transform.rotation);

		ExplodeOnItem();
		ExplodeOnEnemy();

		//TODO: 4f is the duration of the effet, may need to get duration from the effect instead.
		Destroy(effect, 4f);
		Destroy(gameObject);
	}
}
