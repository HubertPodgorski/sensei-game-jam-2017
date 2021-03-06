﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSystem : MonoBehaviour {
    public static float timer;
    public float TimeOfTurn;
    public GameObject playerPrefab;
    public Transform[] spawnPoint;
    public static GameObject activePlayer;
    public GameObject[] enemies;

    public List<TimeController> spawnedPlayers = new List<TimeController>();
    bool AND = true;

    public Text timerText;
    public AudioClip rewind;
    public AudioClip clock;
    private bool canClock = true;

    void Awake() {
        spawnedPlayers.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<TimeController>());
        activePlayer = spawnedPlayers[0].gameObject;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update() {
        if (!TimeController.rewinding) {
            timer += Time.deltaTime;
        }
        else {
            timer -= Time.deltaTime;
        }
        timerText.text = (TimeOfTurn - timer).ToString("#.00");

        if (timer >= TimeOfTurn) {
            TimeController.rewinding = true;
            GetComponent<AudioSource>().PlayOneShot(rewind);
        }
        
        if(TimeController.rewinding) {
            AND = true;
            foreach (TimeController tc in spawnedPlayers) {
                AND = AND && tc.MovementIndex <= 0;
                if(tc) {
                    tc.ReplayIndex = 0;
                    tc.ShotIndex = 0;
                    tc.GetComponent<Player>().health = 100;
                }
                
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
        
        if (timer > TimeOfTurn - 4 && canClock) {
            canClock = false;
            Camera.main.GetComponent<AudioSource>().PlayOneShot(clock);
            Invoke("RepeatCanClock", 9);
        }
        
    }

    public void CheckWinCondition() {
		if(WinCondition())
            Application.LoadLevel("Win");
    }

    bool WinCondition() {
        bool temp = true;
        foreach (GameObject go in enemies) {
            temp &= go.GetComponent<Enemy>().killed;
        }
        return temp;
    }

    void RepeatCanClock() {
        canClock = true;
    }
}
