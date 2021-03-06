﻿using UnityEngine;

[RequireComponent (typeof(PickUp))]
public class PowerUp : MonoBehaviour {

	public string powerUpType;
	PickUp pickUp;
	public AudioClip powerUpSFX;

	void Start () {
		pickUp = GetComponent<PickUp>();
	}

	void Update () {
		transform.Rotate (new Vector3 (0, 0, 90) * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D c) {
		string layerName = LayerMask.LayerToName (c.gameObject.layer); // get the layer name
		
		// quit immediately if it wasn't the player that collided with it
		if (!layerName.Equals ("Player"))
			return;
		
		if (powerUpType.Equals ("Score Multiplier")) {
			GameObject.Find ("Game Manager").GetComponent<GameManager> ().score.activateScoreMultiplier (pickUp.effectTime);
		} else if (powerUpType.Equals ("Invincibility")) {
			c.GetComponent<PlayerDeath>().makeInvincible(pickUp.effectTime);
		}

		AudioSource.PlayClipAtPoint (powerUpSFX, transform.position, 1f);
		
		// destroy the pick-up
		Destroy (gameObject);
	}
}