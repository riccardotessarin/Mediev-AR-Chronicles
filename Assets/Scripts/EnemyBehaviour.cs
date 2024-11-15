﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	private Animator _animator;
	
	private bool _isTakingDamage = false;

	private float maxHealth = 100.0f;
	private float enemyHealth;
	private float damageReceived = 0.0f;
	
	public HealthBar healthBar;

	private float _maxTurnDamage;

	private void Awake() {
		enemyHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		_maxTurnDamage = GameManager.MaxTurnDamage;
		gameObject.SetActive(false);
		_animator = GetComponent<Animator>();
		//gameObject.GetComponent<MeshRenderer>().enabled = false;
	}

	// Use this for initialization
	void Start () {
		
	}

	private void OnTriggerEnter(Collider other) {
		GameObject go = other.gameObject;

		if ( go.CompareTag("Sword") ) {
			if ( !_isTakingDamage ) {
				_isTakingDamage = true;
				EnemyTakeDamage(10.0f);
				//gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		_isTakingDamage = false;
		//gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
	}

	// Update is called once per frame
	void Update () {
		/*
		 if ( Input.GetKeyDown(KeyCode.D) ) {

		}
		
		if ( Input.GetKeyDown(KeyCode.F) ) {
		}
		*/
	}
	
	public void EnemyTakeDamage(float damage) {
		enemyHealth -= damage;
		damageReceived += damage;
		
		healthBar.SetHealth(enemyHealth);

		//If enemy health has gone to 0 it's player win
		if ( enemyHealth <= 0.0f ) {
			StartCoroutine(Defeated());
		}
		
		_animator.SetTrigger("Damaged");

		// If the enemy has received enough damage it changes turns
		if ( damageReceived >= _maxTurnDamage && enemyHealth > 0.0f) {
			TurnEnded();
		}
	}
	
	// To make it better looking
	public void TurnEnded() {
		damageReceived = 0.0f;
		gameObject.SetActive(false);
		//gameObject.GetComponent<MeshRenderer>().enabled = false;
		GameManager.Instance.UpdateGameState(GameState.EnemyTurn);
	}

	private IEnumerator Defeated() {
		_animator.SetTrigger("Dead");
		yield return new WaitForSeconds(3f);
		//Destroy(transform.parent.gameObject);
		GameManager.Instance.UpdateGameState(GameState.Victory);
	}

}
