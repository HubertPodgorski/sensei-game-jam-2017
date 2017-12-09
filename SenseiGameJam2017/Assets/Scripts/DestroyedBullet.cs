using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedBullet {
    public float time;
    public Transform transform;
    public GameObject bullet;

    public DestroyedBullet(float t, Transform tr, GameObject go) {
        time = t;
        transform = tr;
        bullet = go;
    }
}
