using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public float sinkSpeed = 2.5f;
	public int scoreValue = 10;
	public AudioClip deathClip;
	public GameObject spell;

	Animator anim;
	AudioSource enemyAudio;
	ParticleSystem hitParticles;
	
	CapsuleCollider capsuleCollider;
	
	bool isDead;
	bool isSinking;

	float spellPlayPeriod = 3f;
    float timer;

	void Awake()
	{
		anim = GetComponent<Animator>();
		enemyAudio = GetComponent<AudioSource>();
		hitParticles = GetComponentInChildren<ParticleSystem>();
		capsuleCollider = GetComponent<CapsuleCollider>();

		currentHealth = startingHealth; 

		timer = 0f;
	}

	void Update()
	{
		if(isSinking) { transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime); }
		
		timer += Time.deltaTime;
		if(timer > spellPlayPeriod){
			spell.SetActive(false);
			timer = 0f;
		}
	}

	public void TakeDamage(int amount, Vector3 hitPoint)
	{
		if(isDead) 
		{ 
			return; 
		}
		
		enemyAudio.Play();

		currentHealth -= amount;

		hitParticles.transform.position = hitPoint;
		hitParticles.Play();

		// TODO: Effect Audio will not be played in the second time(Bug).
		// TODO: Effect might be too long for one shot.
		timer = 0f;
		spell.SetActive(false);
		spell.SetActive(true);

		if(currentHealth <= 0)
		{ 
			Death();
		}

	}

	void Death()
	{
		isDead = true;

		capsuleCollider.isTrigger = true;

		anim.SetTrigger("Dead");

		enemyAudio.clip = deathClip;
		enemyAudio.Play();
	}

	public void StartSinking()
	{
		GetComponent<NavMeshAgent>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;

		isSinking = true;

		ScoreManager.score += scoreValue;

		Destroy(gameObject, 2f);
	}
}
