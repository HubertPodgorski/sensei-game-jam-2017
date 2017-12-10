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
    MainSystem mainSystem;
    public int health = 100;

    public bool killed = false;
    TimeController timeController;
    GameObject[] players;
    void Awake() {
        timeController = GetComponent<TimeController>();
        mainSystem = GameObject.Find("Main Camera").GetComponent<MainSystem>();
        players = GameObject.FindGameObjectsWithTag("Player");
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

        if(behaviourType == EnemyBehaciourType.Idle) {
            players = GameObject.FindGameObjectsWithTag("Player");
            LookForEnemy();
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
            Vector3 pos  = Player.transform.position - transform.position;
            var newRot = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.05f);
            if (Vector3.Distance(Player.transform.position, transform.position) > DrawFieldOfView.dist_max) {
                behaviourType = EnemyBehaciourType.Idle;
            }
        }
    }

    void LookForEnemy() {
        float dist = float.MaxValue;
        foreach (GameObject go in players) {
            if(Vector3.Distance(go.transform.position, transform.position) < dist) {
                Player = go;
                dist = Vector3.Distance(go.transform.position, transform.position);
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
        transform.Find("Enemy/Volume81.001").GetComponent<SkinnedMeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<DrawFieldOfView>().enabled = false;
        killed = true;

        mainSystem.CheckWinCondition();
        GetComponent<Enemy>().enabled = false;
    }
}

public enum EnemyBehaciourType {
    Idle,
    FoundPlayer,
}
