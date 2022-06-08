using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
	
	private float maxHealth = 100.0f;
	private float health;

	public ScreenFlash screenFlash;

	public GameObject core;
	public HealthBar healthBar;
	
	private void Awake() {
		core = Instantiate(core, core.transform.position, core.transform.rotation);
		core.transform.parent = gameObject.transform;
		GameManager.GameStateChanged += GameManagerOnGameStateChanged;
	}

	private void OnDestroy() {
		GameManager.GameStateChanged -= GameManagerOnGameStateChanged;
	}

	private void GameManagerOnGameStateChanged(GameState state) {
		switch ( state ) {
			case GameState.EnemyTurn:
				Debug.Log("Enemy Turn");
				core.SetActive(true);
				break;
			case GameState.PlayerTurn:
				Debug.Log("Player Turn");
				//enemyPrefab.GetComponent<MeshRenderer>().enabled = true;
				core.SetActive(false);
				break;
			default:
				core.SetActive(false);
				break;
		}
	}

	// Use this for initialization
	void Start () {
		health = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}
	
	public void TakeDamage(float damage) {
		screenFlash.StartFlash(0.25f, 0.5f, Color.red);
		
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
