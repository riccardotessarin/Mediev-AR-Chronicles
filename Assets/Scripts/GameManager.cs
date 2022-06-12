﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public GameState state;
	bool gameHasEnded = false;
	
	public Image tick, cross;

	public PauseManager pauseManager;
	public GameObject gameUI;
	
	public const float TimeEnemyTurn = 30.0f;
	public const float TimePlayerTurn = 30.0f;
	public const float TimeBeforeSpawn = 2.0f;
	public const float TimeBetweenSpawns = 5.0f;

	public const float MaxTurnDamage = 20.0f;

	public const float PlayerDistance = 0.3f;

	public static event Action<GameState> GameStateChanged; 

	private void Awake() {
		
		//Singleton method
		if ( Instance == null ) {
			//First run, set the instance
			Instance = this;
			Instance.state = GameState.Tutorial;
			DontDestroyOnLoad(gameObject);
 
		} else if ( Instance != this ) {
			//Instance is not the same as the one we have, destroy old one, and reset to newest one
			GameState gs = Instance.state;
			//Debug.Log(gs);
			Destroy(Instance.gameObject);
			Instance = this;
			Instance.state = gs;
			DontDestroyOnLoad(gameObject);
		}
		
		/*
		if ( Instance != null && Instance != this ) {
			Destroy(this.gameObject);
		} else {
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		*/
		//tick.gameObject.SetActive(false);
		//cross.gameObject.SetActive(false);
	}

	private void Start() {
		UpdateGameState(Instance.state);
	}

	public void UpdateGameState(GameState newState) {
		state = newState;

		switch ( newState ) {
			case GameState.MainMenu:
				break;
			case GameState.Tutorial:
				break;
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

	public void EndTutorial() {
		gameUI.SetActive(true);
		UpdateGameState(GameState.EnemyTurn);
	}

	// Restart game if player taps on restart button
	public void RestartGame() {
		gameHasEnded = false;
		UpdateGameState(GameState.Tutorial);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ShowTickOnCanvas() {
		StartCoroutine(ShowFeedback(tick, 1.0f));
	}

	public void ShowCrossOnCanvas() {
		StartCoroutine(ShowFeedback(cross, 1.0f));
	}
	
	private IEnumerator ShowFeedback(Image image, float timeToShow) {
		image.gameObject.SetActive(true);
		yield return new WaitForSeconds(timeToShow);
		image.gameObject.SetActive(false);
	}
}

public enum GameState {
	MainMenu,
	Tutorial,
	PlayerTurn,
	EnemyTurn,
	Victory,
	Lose
}