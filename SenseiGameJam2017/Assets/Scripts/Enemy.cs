using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public EnemyBehaciourType behaviourType;
    public GameObject bulletPrefab;
    public GameObject bulletSource;
    public GameObject Player;
    public float attackCooldown;
    float timer = 0;

	// Update is called once per frame
	void Update () {
		if(behaviourType == EnemyBehaciourType.FoundPlayer) {
            RotateTowardsEnemy();
            Shoot();

            timer -= Time.deltaTime;
        }
        else {
            SearchForEnemy();
        }
	}

    void SearchForEnemy() {
        Vector3 direction = Player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < 50) {
            behaviourType = EnemyBehaciourType.FoundPlayer;
        }
    }

    void RotateTowardsEnemy() {
        transform.LookAt(Player.transform);
    }

    void Shoot() {
        if (timer <= 0) {
            Instantiate(bulletPrefab, bulletSource.transform.position, transform.rotation);
            timer = attackCooldown;
        }
    }
}

public enum EnemyBehaciourType {
    Idle,
    FoundPlayer,
}
