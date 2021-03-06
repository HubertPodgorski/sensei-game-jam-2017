﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float bulletSpeed = 30;
    Vector3 startPosition;
    TimeController timeController;


    void Awake() {
        timeController = GetComponent<TimeController>();
    }


    void Start () {
        GetComponent<Rigidbody>().AddForce(transform.up * bulletSpeed, ForceMode.Impulse);
        if (!TimeController.rewinding) {
            Invoke("DestroyBullet", 2f);
            startPosition = transform.position;
        }
	}

    void Update() {
        if (TimeController.rewinding) {
            if(Vector3.Distance(startPosition, transform.position) < 0.1f)
                Destroy(transform.parent.gameObject);
        }
    }

	void DestroyBullet() {
        if (!TimeController.rewinding)
            TurnOff();

    }

    void TurnOff() {
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

	void OnTriggerEnter(Collider collider) {
        if(!TimeController.rewinding) {
            if (collider.tag != "Enemy") {
                timeController.destroyedBullet = new DestroyedBullet(MainSystem.timer, transform.position, transform.rotation, gameObject, null);
                TurnOff();
            }
        }

        if (collider.CompareTag("enemyShield")) {
            timeController.destroyedBullet = new DestroyedBullet(MainSystem.timer, transform.position, transform.rotation, gameObject, null);
            TurnOff();
        }

        if (collider.CompareTag("Enemy")) {
            collider.GetComponent<Enemy>().DMG();
            timeController.destroyedBullet = new DestroyedBullet(MainSystem.timer, transform.position, transform.rotation, gameObject, null);
            TurnOff();
        }
	}
}
