﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
   
	void Start () {
        GetComponent<Rigidbody>().AddForce(Vector3.right * 10, ForceMode.Impulse);
	}

}
