using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float bulletSpeed = 30;

	void Start () {
        	GetComponent<Rigidbody>().AddForce(transform.up * bulletSpeed, ForceMode.Impulse);
		Invoke("DestroyBullet", 2f);
	}

	void DestroyBullet() {
		Destroy(transform.parent.gameObject);
	}
}
