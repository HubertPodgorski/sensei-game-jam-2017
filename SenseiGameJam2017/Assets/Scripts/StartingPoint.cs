using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPoint : MonoBehaviour {
	public Player player;
	
	void Awake() {
		player = GameObject.FindObjectOfType<Player>();
	}

	void OnMouseDown() {
		//player.GetComponent<Player>().setPlayerPosition(transform.position);
	}
}
