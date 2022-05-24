using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
	
	private float maxHealth = 100.0f;
	private float health;

	public HealthBar healthBar;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}
	
	public void TakeDamage(float damage) {
		health -= damage;
		healthBar.SetHealth(health);
		
		if ( health <= 0f ) {
			GameManager.Instance.UpdateGameState(GameState.Lose);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
