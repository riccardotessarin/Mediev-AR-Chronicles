/*
 *	This is the manager that handles the turn times, and enemy and enemy attacks spawn
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SwordSpawnManager : MonoBehaviour {

	//public static SwordSpawnManager Instance;

	[SerializeField]
	private GameObject enemyPrefab;
	[SerializeField]
	private GameObject enemySword;
	[SerializeField]
	private GameObject arrowPrefab;

	//public EnemyBehaviour enemyBehaviour;

	private readonly int[] _xSpawn = { -1, -1, 0, 1, 1 };
	private readonly int[] _ySpawn = { 0, -2, -2, -2, 0 };

	public Image[] arrowList;

	public ParticleSystem circleEffect;
	public ParticleSystem verticalEffect;

	private float _timer = 0.0f;
	private float _turnTimer = 0.0f;

	private float _turnTime;
	private float _timeBetweenSpawns;
	private float _spawnDelay;

	private bool _isPlayerTurn = false;
	private bool _isEnemyTurn = false;

	//private GameObject _medallion;
	//private Vector3 _medallionPosition;
	private Vector3 _corePosition;


	private void Awake() {
		//Instance = this;
		GameManager.GameStateChanged += GameManagerOnGameStateChanged;
	}

	private void OnDestroy() {
		GameManager.GameStateChanged -= GameManagerOnGameStateChanged;
	}

	private void GameManagerOnGameStateChanged(GameState state) {
		switch ( state ) {
			case GameState.EnemyTurn:
				Debug.Log("Enemy Turn");
				_turnTimer = 0.0f;
				_turnTime = GameManager.Instance.gameDifficulty.TimeEnemyTurn;
				_timeBetweenSpawns = GameManager.Instance.gameDifficulty.TimeBetweenSpawns;
				_spawnDelay = GameManager.Instance.gameDifficulty.TimeBeforeSpawn;
				_isEnemyTurn = true;
				_isPlayerTurn = false;
				break;
			case GameState.PlayerTurn:
				Debug.Log("Player Turn");
				//enemyPrefab.GetComponent<MeshRenderer>().enabled = true;
				StartCoroutine(SpawnEnemy());
				//enemyPrefab.SetActive(true);
				//enemyPrefab.GetComponent<EnemyBehaviour>().StartFadeIn();
				_turnTimer = 0.0f;
				_turnTime = GameManager.Instance.gameDifficulty.TimePlayerTurn;
				_isEnemyTurn = false;
				_isPlayerTurn = true;
				break;
			default:
				_isEnemyTurn = false;
				_isPlayerTurn = false;
				break;
		}
	}

	// Use this for initialization
	void Start () {
		//_medallion = GameObject.FindWithTag("Medallion");
		//_medallionPosition = _medallion.transform.position;
		
		/*
		foreach ( var arrow in arrowList ) {
			arrow.gameObject.SetActive(false);
		}
		*/
		_corePosition = new Vector3(0,0,GameManager.PlayerDistance);
		enemyPrefab = Instantiate(enemyPrefab, new Vector3(0, 0, 0.1f), enemyPrefab.transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {

		if ( _isEnemyTurn ) {
			_timer += Time.deltaTime;
			if ( _timer >= _timeBetweenSpawns ) {
				StartCoroutine(SpawnSword(_spawnDelay));
				_timer = 0f;
			}
			_turnTimer += Time.deltaTime;

			if ( _turnTimer >= _turnTime ) {
				Debug.Log("Finished");

				_turnTimer = 0.0f;
				GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
			}
		}
		
		else if ( _isPlayerTurn ) {
			_turnTimer += Time.deltaTime;

			if ( _turnTimer >= _turnTime ) {
				enemyPrefab.GetComponent<EnemyBehaviour>().TurnEnded();
				_turnTimer = 0.0f;
			}
		}
		
		/*
		_timer += Time.deltaTime;
		if ( _timer >= TimeBetweenSpawns ) {
			StartCoroutine(SpawnSword(TimeBeforeSpawn));
			_timer = 0f;
		}
		*/
	}

	private IEnumerator SpawnSword(float timeBeforeSpawn) {
		int i = Random.Range(0, _xSpawn.Length);
		/*
		// For now if it can't find the medallion it will just orient it towards (0,0,1)
		_medallionPosition = GameObject.FindWithTag("Medallion") == null ? new Vector3(0, 0, 1) : _medallion.transform.position;
		Vector3 position = new Vector3(_xSpawn[i], _ySpawn[i], _medallionPosition.z);
		Instantiate(arrowPrefab, _medallionPosition, Quaternion.LookRotation(position - _medallionPosition));
		yield return new WaitForSeconds(timeBeforeSpawn);
		Instantiate(enemySword, position, Quaternion.LookRotation(_medallionPosition - position));
		*/
		Vector3 position = new Vector3(_xSpawn[i], _ySpawn[i], _corePosition.z);
		StartCoroutine(Blink(arrowList[i], timeBeforeSpawn));
		yield return new WaitForSeconds(timeBeforeSpawn);
		Instantiate(enemySword, position, Quaternion.LookRotation(_corePosition - position));


	}
	
	private IEnumerator Blink(Image arrow, float timeBeforeSpawn) {
		int repetitions = (int)(timeBeforeSpawn / 0.4);
		int i = 0;
		while ( i < repetitions ) {
			arrow.gameObject.SetActive(true);
			yield return new WaitForSeconds(0.2f);
			arrow.gameObject.SetActive(false);
			yield return new WaitForSeconds(0.2f);
			i++;
		}
	}

	private IEnumerator SpawnEnemy() {
		verticalEffect.Play();
		circleEffect.Play();
		yield return new WaitForSeconds(0.5f);
		enemyPrefab.SetActive(true);
		enemyPrefab.GetComponent<CapsuleCollider>().enabled = true;

	}
}
