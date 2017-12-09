using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private int health = 100;

	void OnTriggerEnter(Collider collider) {
		if (collider.CompareTag("Bullet")) {
			// destroy bullet on hit
            Destroy(collider.transform.parent.gameObject);
        }
		var damage = 10;
		health -= damage;
		if (health <= 0) {
			Die();
		}
	}

	void Die() {
        Destroy(gameObject);
    }
}
