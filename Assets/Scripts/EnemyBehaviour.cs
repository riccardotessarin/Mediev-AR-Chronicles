using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

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
				gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		_isTakingDamage = false;
		gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
	}

	// Update is called once per frame
	void Update () {
		
	}
	
	public void EnemyTakeDamage(float damage) {
		enemyHealth -= damage;
		damageReceived += damage;
		
		healthBar.SetHealth(enemyHealth);

		//If enemy health has gone to 0 it's player win
		if ( enemyHealth <= 0.0f ) {
			Defeated();
		}
		
		// If the enemy has received enough damage it changes turns
		if ( damageReceived >= _maxTurnDamage ) {
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

	private void Defeated() {
		GameManager.Instance.UpdateGameState(GameState.Victory);
		Destroy(gameObject);
	}

}
