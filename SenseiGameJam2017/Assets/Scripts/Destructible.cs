using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    void OnTriggerEnter(Collider col) {
        if(col.tag == "Bullet") {
            for(int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).GetComponent<Rigidbody>().AddExplosionForce(150, col.transform.position, 5);
                transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
