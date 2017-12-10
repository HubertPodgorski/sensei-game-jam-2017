using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSystem : MonoBehaviour {
    public static float timer;
    public float TimeOfTurn;
    public GameObject playerPrefab;
    public Transform[] spawnPoint;
    public static GameObject activePlayer;
    public GameObject[] enemies;

    public List<TimeController> spawnedPlayers = new List<TimeController>();
    bool AND = true;

    void Awake() {
        spawnedPlayers.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<TimeController>());
        activePlayer = spawnedPlayers[0].gameObject;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update() {
        if (!TimeController.rewinding)
            timer += Time.deltaTime;
        else
            timer -= Time.deltaTime;

        if (timer >= TimeOfTurn)
            TimeController.rewinding = true;
        
        if(TimeController.rewinding) {
            AND = true;
            foreach (TimeController tc in spawnedPlayers) {
                AND = AND && tc.MovementIndex <= 0;
                tc.ReplayIndex = 0;
                tc.ShotIndex = 0;
                tc.GetComponent<Player>().health = 100;
            }

            if (AND) {
                timer = 0;
                GameObject go = Instantiate(playerPrefab, spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity);
                go.GetComponent<PlayerMovement>().weaponType = (WeaponType)Random.Range(0, 2);
                spawnedPlayers.Add(go.GetComponent<TimeController>());
                activePlayer = spawnedPlayers[spawnedPlayers.Count - 1].gameObject;
                TimeController.rewinding = false;
            }
        }
        

        
    }

    public void CheckWinCondition() {
        bool temp = WinCondition();
    }

    bool WinCondition() {
        bool temp = true;
        foreach (GameObject go in enemies) {
            temp &= go.GetComponent<Enemy>().killed;
        }
        return temp;
    }
}
