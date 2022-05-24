using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;
	public GameObject gameMenuUI;
	public GameObject winUI;
	public GameObject loseUI;
	
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
	}
	
	public void Pause() {
		if( !GameIsPaused ) {
			gameMenuUI.SetActive(false);
			pauseMenuUI.SetActive(true);
			Time.timeScale = 0f;
			GameIsPaused = true;
			AudioListener.pause = true;
		}
	}

	public void Resume() {
		if ( GameIsPaused ) {
			pauseMenuUI.SetActive(false);
			gameMenuUI.SetActive(true);
			Time.timeScale = 1f;
			GameIsPaused = false;
			AudioListener.pause = false;
		}
	}

	public void Victory() {
		gameMenuUI.SetActive(false);
		winUI.SetActive(true);
		Time.timeScale = 0f;
	}
	
	public void Defeat() {
		gameMenuUI.SetActive(false);
		loseUI.SetActive(true);
		Time.timeScale = 0f;
	}

	public void QuitGame() {
		Application.Quit();
	}
}
