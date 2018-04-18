using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

	Animator anim;
	AudioSource playerAudio;
	PlayerController playerController;
	PlayerRangedAttack playerShooting;

	bool isDead;
	bool isDamaged;

	void Awake()
	{
		anim = GetComponent<Animator>();
		playerAudio = GetComponent<AudioSource>();
		playerController = GetComponent<PlayerController>();
		playerShooting = GetComponent<PlayerRangedAttack>();
		currentHealth = startingHealth;
	}

	void Update()
	{
		if(isDamaged)
		{
			damageImage.color = flashColor;
		}
		else
		{
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		isDamaged = false;
	}

	public void TakeDamage(int amount)
	{
		isDamaged = true;
		
		currentHealth -= amount;

		healthSlider.value = currentHealth;

		playerAudio.Play();

		if(currentHealth <= 0 && !isDead)
		{
			Death();
		}
	}

	void Death()
	{
		isDead = true;

		playerShooting.DisableEffects();

		anim.SetTrigger("Die");

		playerAudio.clip = deathClip;
		playerAudio.Play();

		playerController.enabled = false;
		playerShooting.enabled = false;
	}
}
