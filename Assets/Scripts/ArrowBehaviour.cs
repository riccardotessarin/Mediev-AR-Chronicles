/*
 *	This class manages the 3D arrow prefab creation and destruction
 *
 *	KEEPING THIS FOR REFERENCE BUT REPLACED WITH 2D CANVAS ARROW
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {
	private void Awake() {
		transform.Translate(Vector3.forward * (float)0.1, Space.Self);
		StartCoroutine(WaitAndDestroy(2.0f));
	}
	
	// This function destroys the arrow after some seconds
	private IEnumerator WaitAndDestroy(float waitTime) {
		while (true) {
			yield return new WaitForSeconds(waitTime);
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(Blink());
	}
	
	// Update is called once per frame
	void Update () {
	}
	
 
	private IEnumerator Blink() {
		while ( true ) {
			yield return new WaitForSeconds(0.2f);
			GetComponentInChildren<Renderer>().enabled = false;
			yield return new WaitForSeconds(0.2f);
			GetComponentInChildren<Renderer>().enabled = true;
		}
	}

}
