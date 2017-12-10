using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public int health = 100;

    public AudioClip death, booo;
    public AudioClip[] painSFX;
    private int painSFXid = 0;

    private bool heartSFXready = true;
    public AudioClip[] heartSFX;

    private bool breathSFXready = false;
    public AudioClip[] breathSFX;
    private int breathSFXid = 0;
    private int repeatBreathSFXtimer;

    void Start () {
            Invoke("RepeatBreathSFX", 5);
    }

    void OnTriggerEnter(Collider collider) {
		if (collider.CompareTag("Bullet")) {
			// destroy bullet on hit
            Destroy(collider.transform.parent.gameObject);
			
			var damage = 25;
			health -= damage;
			if (health <= 0) {
				Die();
                Camera.main.GetComponent<AudioSource>().PlayOneShot(death);
                Camera.main.GetComponent<AudioSource>().PlayOneShot(booo);

            }
            painSFXid = Random.Range(0, painSFX.Length);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(painSFX[painSFXid]);
        }
	}

	void Die() {
        Application.LoadLevel("Loose");
    }
	
	public void setPlayerPosition(Vector3 position) {
		transform.position = position;
	}

	void Update() {
        if (heartSFXready)
        {
            heartSFXready = false;

            if (health < 45)
            {
                Camera.main.GetComponent<AudioSource>().PlayOneShot(heartSFX[0]);
                Invoke("RepeatHeartSFX", 0.5f);
            }
            else if (health < 75)
            {
                Camera.main.GetComponent<AudioSource>().PlayOneShot(heartSFX[1]);
                Invoke("RepeatHeartSFX", 0.8f);
            }
            else
            {
                Invoke("RepeatHeartSFX", 2);
            }
        }

        if (breathSFXready) {
            breathSFXready = false;
            if (health < 45)
            {
                repeatBreathSFXtimer = Random.Range(3, 4);

                breathSFXid = Random.Range(0, breathSFX.Length);
                Camera.main.GetComponent<AudioSource>().PlayOneShot(breathSFX[breathSFXid]);
                Invoke("RepeatBreathSFX", repeatBreathSFXtimer);
            }
            else if (health < 75)
            {
                repeatBreathSFXtimer = Random.Range(4, 6);

                breathSFXid = Random.Range(0, breathSFX.Length);
                Camera.main.GetComponent<AudioSource>().PlayOneShot(breathSFX[breathSFXid]);
                Invoke("RepeatBreathSFX", repeatBreathSFXtimer);
            }
            else
            {
                repeatBreathSFXtimer = Random.Range(15, 20);

                breathSFXid = Random.Range(0, breathSFX.Length);
                Camera.main.GetComponent<AudioSource>().PlayOneShot(breathSFX[breathSFXid]);
                Invoke("RepeatBreathSFX", repeatBreathSFXtimer);
            }
        }

    }

    void RepeatHeartSFX()
    {
        heartSFXready = true;
    }

    void RepeatBreathSFX()
    {
        breathSFXready = true;
    }
}
