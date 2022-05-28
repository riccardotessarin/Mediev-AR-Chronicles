using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public GameState state;
	bool gameHasEnded = false;

	public PauseManager pauseManager;
	
	public const float TimeEnemyTurn = 2.0f;
	public const float TimePlayerTurn = 2000.0f;
	public const float TimeBeforeSpawn = 2.0f;
	public const float TimeBetweenSpawns = 5.0f;

	public const float MaxTurnDamage = 50.0f;

	public static event Action<GameState> GameStateChanged; 

	private void Awake() {
		Instance = this;
	}

	private void Start() {
		UpdateGameState(GameState.EnemyTurn);
	}

	public void UpdateGameState(GameState newState) {
		state = newState;

		switch ( newState ) {
			case GameState.PlayerTurn:
				break;
			case GameState.EnemyTurn:
				break;
			case GameState.Victory:
				HandleGameWin();
				break;
			case GameState.Lose:
				HandleGameLose();
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		if ( GameStateChanged != null ) GameStateChanged.Invoke(newState);
	}

	private void HandleGameWin() {
		if ( gameHasEnded == false ) {
			gameHasEnded = true;
			pauseManager.Victory();
		}
	}

	private void HandleGameLose() {
		
		if ( gameHasEnded == false ) {
			gameHasEnded = true;
			pauseManager.Defeat();
		}
	}

	// Restart game if player taps on restart button
	public void RestartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}

public enum GameState {
	PlayerTurn,
	EnemyTurn,
	Victory,
	Lose
}