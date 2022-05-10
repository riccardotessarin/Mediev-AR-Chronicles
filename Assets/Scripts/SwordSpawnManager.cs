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
		int i = Random.Range(0, _xSpawn.Length);
		if ( _timer >= 5.0f ) {
			// For now if it can't find the medallion it will just orient it towards (0,0,1)
			_medallionPosition = GameObject.FindWithTag("Medallion") == null ? new Vector3(0, 0, 1) : _medallion.transform.position;
			Vector3 position = new Vector3(_xSpawn[i], _ySpawn[i], _medallionPosition.z);
			Instantiate(enemySword, position, Quaternion.LookRotation(_medallionPosition - position));
			Instantiate(arrowPrefab, _medallionPosition, Quaternion.LookRotation(position - _medallionPosition));
			_timer = 0f;
		}
	}
}
