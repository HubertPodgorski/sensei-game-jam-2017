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

    int health = 100;

    public bool killed = false;
    TimeController timeController;

    void Awake() {
        timeController = GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update () {
        if(!TimeController.rewinding) {
            if (behaviourType == EnemyBehaciourType.FoundPlayer) {
                RotateTowardsEnemy();
                Shoot();

                timer -= Time.deltaTime;
            }
            else {
                SearchForEnemy();
            }
        }

        Player = MainSystem.activePlayer;
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
            Vector3 pos  = Player.transform.position - transform.position;
            var newRot = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.05f);
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

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Bullet")) {
            // destroy bullet on hit
            Destroy(collider.transform.parent.gameObject);
        }
        if (behaviourType == EnemyBehaciourType.Idle) {
            if (Player.GetComponent<PlayerMovement>().weaponType == WeaponType.ar) {
                var damage = 20;
                health -= damage;
                if (health <= 0) {
                    Die();
                }
            }
            if (Player.GetComponent<PlayerMovement>().weaponType == WeaponType.handgun) {
                var damage = 15;
                health -= damage;
                if (health <= 0) {
                    Die();
                }
            }
            if (Player.GetComponent<PlayerMovement>().weaponType == WeaponType.shotgun) {
                var damage = 10;
                health -= damage;
                if (health <= 0) {
                    Die();
                }
            }
        }
    }

    void Die() {
        //Destroy(gameObject);
        timeController.destroyedBullet = new DestroyedBullet(MainSystem.timer, transform.position, transform.rotation, gameObject, null);
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<DrawFieldOfView>().enabled = false;
        killed = true;
        GetComponent<Enemy>().enabled = false;
    }
}

public enum EnemyBehaciourType {
    Idle,
    FoundPlayer,
}
