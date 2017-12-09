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
        else{
            SearchForEnemy();
        }
	}

    void SearchForEnemy() {
        if(Player && Vector3.Distance(Player.transform.position, transform.position) <= DrawFieldOfView.dist_max) {
            Vector3 direction = Player.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < 50) {
                behaviourType = EnemyBehaciourType.FoundPlayer;
            }
        }
    }

    void RotateTowardsEnemy() {
        if (Player) {
            transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));
            if (Vector3.Distance(Player.transform.position, transform.position) > DrawFieldOfView.dist_max) {
                behaviourType = EnemyBehaciourType.Idle;
            }
        }
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
