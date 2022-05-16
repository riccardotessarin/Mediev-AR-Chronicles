using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

	private float health;

	// Use this for initialization
	void Start () {
		health = 100.0f;
	}
	
	public void TakeDamage(float damage) {
		health -= damage;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
