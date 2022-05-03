using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordBehaviour : MonoBehaviour {
	
	private const float ThrowSpeed = 2f;

	private void Awake() {
		StartCoroutine(WaitAndDestroy(30.0f));
	}
	
	// This function destroys the sword after a certain time if it doesn't hit anything
	private IEnumerator WaitAndDestroy(float waitTime) {
		while (true) {
			yield return new WaitForSecondsRealtime(waitTime);
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}

	private void OnCollisionEnter(Collision collision) {
		GameObject go = collision.gameObject;

		switch ( go.tag ) {
			// If it collides with the player shield then no damage
			case "Shield":
				Destroy(this);
				break;
			case "Sword":
				go.transform.root.GetComponent<PlayerData>().TakeDamage(5f);
				Destroy(this);
				break;
			case "Medallion":
				go.transform.root.GetComponent<PlayerData>().TakeDamage(15f);
				Destroy(this);
				break;
		}
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * (Time.unscaledDeltaTime * ThrowSpeed), Space.Self);
	}
}
