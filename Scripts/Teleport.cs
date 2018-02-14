using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	private RaycastHit lastHit;
	public float range = 1000f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (1)) {
			if (GetPosition () != null) {
				TeleportActive ();
			}
		}
	}

	GameObject GetPosition(){

		Vector3 origin = transform.position;
		Vector3 direction = Camera.main.transform.forward;

		if (Physics.Raycast (origin, direction, out lastHit, range)) {
			return lastHit.collider.gameObject;
		} else
			return null;
	}

	void TeleportActive(){

		transform.position = lastHit.point + lastHit.normal * 2;
	}
}
