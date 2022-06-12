using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
	
	public GameObject tutorialUI;
	public GameObject calibrationUI;

	public GameObject skipButton;

	public GameObject distanceWall;
	//private float playerDistance = GameManager.PlayerDistance;
	
	private void Awake() {
		//Instance = this;
		distanceWall.gameObject.SetActive(false);
		GameManager.GameStateChanged += GameManagerOnGameStateChanged;
		//distanceWall = Instantiate(distanceWall, new Vector3(0,0, playerDistance), distanceWall.transform.rotation);
		//distanceWall.gameObject.SetActive(false);
	}

	private void OnDestroy() {
		GameManager.GameStateChanged -= GameManagerOnGameStateChanged;
	}

	private void GameManagerOnGameStateChanged(GameState state) {
		switch ( state ) {
			case GameState.Tutorial:
				Debug.Log("Tutorial Phase");
				tutorialUI.SetActive(true);
				skipButton.SetActive(true);
				//distanceWall.gameObject.SetActive(true);
				break;
			default:
				//distanceWall.gameObject.SetActive(false);
				break;
		}
	}

	public void ShowCalibrationUI() {
		tutorialUI.SetActive(false);
		calibrationUI.SetActive(true);
	}

	public void StartCalibration() {
		calibrationUI.SetActive(false);
		distanceWall.gameObject.SetActive(true);
		skipButton.SetActive(false);
	}

	public void SkipTutorial() {
		tutorialUI.SetActive(false);
		calibrationUI.SetActive(false);
		distanceWall.gameObject.SetActive(false);
		skipButton.SetActive(false);
		GameManager.Instance.EndTutorial();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
