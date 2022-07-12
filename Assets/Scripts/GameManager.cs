using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
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

	public GameObject mainMenuUI;
	public GameObject settingsUI;
	public TMP_Dropdown difficultyDropdown;
	
	public GameObject quitMenuUI;
	private bool quitMenuOpen = false;
	
	public GameDifficulty gameDifficulty = new GameDifficulty();

	public const float PlayerDistance = 0.3f;

	public static event Action<GameState> GameStateChanged; 

	private void Awake() {
		//Singleton method
		if ( Instance == null ) {
			//First run, set the instance
			Instance = this;
			Instance.state = GameState.MainMenu;
			gameDifficulty.SetOnEasy();
			Debug.Log(gameDifficulty.difficulty);
			//gameDifficulty.SetOnHard();
			//Debug.Log(gameDifficulty.difficulty);
			
			DontDestroyOnLoad(gameObject);
 
		} else if ( Instance != this ) {
			//Instance is not the same as the one we have, destroy old one, and reset to newest one
			GameState gs = Instance.state;
			GameDifficulty gd = Instance.gameDifficulty;
			//Debug.Log(gs);
			Destroy(Instance.gameObject);
			Instance = this;
			Instance.state = gs;
			Instance.gameDifficulty = gd;
			Debug.Log(gameDifficulty.difficulty);
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
		if ( Instance.state == GameState.MainMenu ) {
			difficultyDropdown.value = gameDifficulty.difficulty;
			difficultyDropdown.RefreshShownValue();
		}
		UpdateGameState(Instance.state);
	}

	private void Update() {
		if ( Input.GetKeyUp(KeyCode.Escape) ) {
			if ( quitMenuOpen ) {
				CloseQuitMenu();
			} else {
				OpenQuitMenu();
			}
		}
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
				StartCoroutine(HandleGameWin());
				break;
			case GameState.Lose:
				HandleGameLose();
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		if ( GameStateChanged != null ) GameStateChanged.Invoke(newState);
	}

	private IEnumerator HandleGameWin() {
		if ( gameHasEnded == false ) {
			gameHasEnded = true;
			yield return new WaitForSeconds(5f);
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

	public void StartGame() {
		UpdateGameState(GameState.Tutorial);
		SceneManager.LoadScene("GameScene");
	}

	public void OpenSettings() {
		mainMenuUI.SetActive(false);
		settingsUI.SetActive(true);
	}

	public void ToMainMenu() {
		if ( PauseManager.GameIsPaused ) {
			pauseManager.PauseToMenu();
		}
		gameHasEnded = false;
		UpdateGameState(GameState.MainMenu);
		SceneManager.LoadScene("MainMenuScene");
	}

	// Restart game if player taps on restart button
	public void RestartGame() {
		gameHasEnded = false;
		UpdateGameState(GameState.Tutorial);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	public void OpenQuitMenu() {
		quitMenuOpen = true;
		quitMenuUI.SetActive(true);
	}
	
	public void CloseQuitMenu() {
		quitMenuOpen = false;
		quitMenuUI.SetActive(false);
	}
	
	public void QuitGame() {
		Application.Quit();
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

	public void ChangeDifficulty(int val) {
		if ( gameDifficulty.difficulty != val ) {
			switch ( val ) {
				case 0:
					gameDifficulty.SetOnEasy();
					Debug.Log("Set on Easy");
					break;
				case 1:
					gameDifficulty.SetOnNormal();
					Debug.Log("Set on Normal");
					break;
				case 2:
					gameDifficulty.SetOnHard();
					Debug.Log("Set on Hard");
					break;
			}

		}
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