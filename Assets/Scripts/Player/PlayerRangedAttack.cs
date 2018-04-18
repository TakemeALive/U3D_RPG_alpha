using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour {

	public int damagePerShot = 20;
	public float timeBetweenBullets = 0.15f;
	public float range = 100;

	float timer;
	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;

	ParticleSystem gunParticles;
	ParticleSystem spellParticleOnEnemy;
	AudioSource gunAudio;
	Light gunLight;
	float effectsDisplayTime = 0.2f;

	bool isAttackEnabled = true;

	void Awake()
	{
		shootableMask = LayerMask.GetMask("Shootable");
		gunParticles = GetComponent<ParticleSystem>();
		gunAudio = GetComponent<AudioSource>();
		gunLight = GetComponent<Light>();
	}

	void Update()
	{
		timer += Time.deltaTime;

		if(Input.GetButton("Fire1") && timer >= timeBetweenBullets && isAttackEnabled)
		{
			Shoot();
		}

		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects();
		}
	}

	public void DisableEffects()
	{
		gunLight.enabled = false;
	}
	
	void Shoot()
	{
		timer = 0f;

		gunAudio.Play();
		
		gunLight.enabled = true;

		gunParticles.Stop();
		gunParticles.Play();

		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

		if(Physics.Raycast(shootRay, out shootHit, range, shootableMask))
		{
			EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
			if(enemyHealth != null)
			{
				enemyHealth.TakeDamage(damagePerShot, shootHit.point);
			}
		}
	}

	public void SetAttackEnable(bool b){
		isAttackEnabled = b;
	}
}
