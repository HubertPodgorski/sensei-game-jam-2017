using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedBullet {
    public float time;
    public Vector3 position;
    public Quaternion rotation;
    public GameObject bullet;

    public DestroyedBullet(float t, Vector3 p, Quaternion q, GameObject go) {
        time = t;
        position = p;
        rotation = q;
        bullet = go;
    }
}
