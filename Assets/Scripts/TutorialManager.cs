using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

	public GameObject tutorialUI;
	
	public GameObject distanceWall;
	private float playerDistance = GameManager.PlayerDistance;
	
	private void Awake() {
		//Instance = this;
		GameManager.GameStateChanged += GameManagerOnGameStateChanged;
		tutorialUI.SetActive(true);
		distanceWall = Instantiate(distanceWall, new Vector3(0,0, playerDistance), distanceWall.transform.rotation);
		distanceWall.gameObject.SetActive(false);
	}

	private void OnDestroy() {
		GameManager.GameStateChanged -= GameManagerOnGameStateChanged;
	}

	private void GameManagerOnGameStateChanged(GameState state) {
		switch ( state ) {
			case GameState.Tutorial:
				Debug.Log("Tutorial Phase");
				distanceWall.gameObject.SetActive(true);
				break;
			default:
				distanceWall.gameObject.SetActive(false);
				break;
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
