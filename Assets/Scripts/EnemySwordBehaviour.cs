using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	private void OnCollisionEnter(Collision collision) {
		GameObject go = collision.gameObject;

		switch ( go.tag ) {
			// If it collides with the player shield then no damage
			case "Shield":
				gameObject.SetActive(false);
				Destroy(this);
				break;
			case "Sword":
				go.transform.root.GetComponent<PlayerData>().TakeDamage(5f);
				gameObject.SetActive(false);
				Destroy(this);
				break;
			case "Medallion":
				go.transform.root.GetComponent<PlayerData>().TakeDamage(15f);
				gameObject.SetActive(false);
				Destroy(this);
				break;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
