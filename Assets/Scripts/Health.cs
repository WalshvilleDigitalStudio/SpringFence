using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float hitPoints = 100f; 
	float currentHitPoints;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// TakeDamage
	public void TakeDamage (float amt) {
		currentHitPoints -= amt;

		if (currentHitPoints <= 0) {
			Die();
		}

	}

	// Die
	void Die () {
		Destroy (gameObject);
	}
}
