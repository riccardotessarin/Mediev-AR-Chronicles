using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpawnManager : MonoBehaviour {

	[SerializeField]
	private GameObject enemySword;
	[SerializeField]
	private GameObject arrowPrefab;

	private readonly int[] _xSpawn = { -1, -1, 0, 1, 1 };
	private readonly int[] _ySpawn = { 0, -2, -2, -2, 0 };

	private const float TimeBeforeSpawn = 2.0f;
	private const float TimeBetweenSpawns = 5.0f;
	private float _timer = 0.0f;

	private GameObject _medallion;
	private Vector3 _medallionPosition;

	// Use this for initialization
	void Start () {
		_medallion = GameObject.FindWithTag("Medallion");
		_medallionPosition = _medallion.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		_timer += Time.deltaTime;
		if ( _timer >= TimeBetweenSpawns ) {
			StartCoroutine(SpawnSword(TimeBeforeSpawn));
			_timer = 0f;
		}
	}
	
	private IEnumerator SpawnSword(float timeBeforeSpawn) {
		int i = Random.Range(0, _xSpawn.Length);
		// For now if it can't find the medallion it will just orient it towards (0,0,1)
		_medallionPosition = GameObject.FindWithTag("Medallion") == null ? new Vector3(0, 0, 1) : _medallion.transform.position;
		Vector3 position = new Vector3(_xSpawn[i], _ySpawn[i], _medallionPosition.z);
		Instantiate(arrowPrefab, _medallionPosition, Quaternion.LookRotation(position - _medallionPosition));
		yield return new WaitForSeconds(timeBeforeSpawn);
		Instantiate(enemySword, position, Quaternion.LookRotation(_medallionPosition - position));
	}
}
