using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DistanceWallBehaviour : MonoBehaviour {

	public TextMeshProUGUI countdown;

	private bool _swordTrigger = false;
	private bool _shieldTrigger = false;

	private bool closing = false;

	private float _timer = 3.0f;

	private void OnTriggerEnter(Collider other) {
		GameObject go = other.gameObject;

		if ( go.CompareTag("Sword") ) {
			_swordTrigger = true;
		} else if ( go.CompareTag("Shield") ) {
			_shieldTrigger = true;
		}
	}

	private void OnTriggerStay(Collider other) {
		if ( _swordTrigger || _shieldTrigger ) {
			countdown.gameObject.SetActive(true);
			_timer -= Time.deltaTime;
			Debug.Log(_timer);
			if ( _timer <= 0.6f ) {
				countdown.text = "GO!";
				if ( closing ) return;
				closing = true;
				StartCoroutine(CountdownEnd());
			} else {
				countdown.text = _timer.ToString("0");
			}
		}
	}

	private IEnumerator CountdownEnd() {
		yield return new WaitForSeconds(1.0f);
		countdown.gameObject.SetActive(false);
		GameManager.Instance.EndTutorial();
		gameObject.SetActive(false);
	}

	private void OnTriggerExit(Collider other) {
		GameObject go = other.gameObject;

		if ( go.CompareTag("Sword") ) {
			_swordTrigger = false;
		} else if ( go.CompareTag("Shield") ) {
			_shieldTrigger = false;
		}

		if ( !_shieldTrigger && !_swordTrigger ) {
			ResetTimer();
		}
	}

	private void ResetTimer() {
		countdown.gameObject.SetActive(false);
		_timer = 3.0f;
		countdown.text = _timer.ToString("0");
	}

	// Use this for initialization
	void Start () {
		countdown.text = _timer.ToString("0");
	}
}
