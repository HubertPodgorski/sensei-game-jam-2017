using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

	private ArrayList Movements = new ArrayList();
	private ArrayList rotation = new ArrayList();
    public List<DestroyedBullet> shots = new List<DestroyedBullet>();
    public DestroyedBullet destroyedBullet;

    public int MovementIndex = 0;
    public int ReplayIndex = 0;
    public int ShotIndex = 0;
    private int DestroyedIndex = 0;
    public static bool rewinding = false;
	public bool useForce = true;

	public Rigidbody rb;
    bool wasUsedGravity;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        wasUsedGravity = rb.useGravity;
    }

	void LateUpdate () {
        		
		if (!rewinding) {

			Movements.Add (transform.position);
			rotation.Add (transform.rotation);

			MovementIndex++;
		}

		if(MovementIndex > Movements.Count - 1) {
			MovementIndex = Movements.Count;	
		}

		if(Input.GetKeyDown("e")) 
			rewinding = true;
		
        if(rewinding)
            RewindTime();

        if (Input.GetKeyDown("f")) {
			rewinding = false;
		}
	}

	void RewindTime () {
        rb.useGravity = wasUsedGravity;
        MovementIndex--;
        rb.velocity = Vector2.zero;

		if (MovementIndex >= 0) {
			
			transform.position = (Vector3) Movements[MovementIndex];
			transform.rotation = (Quaternion) rotation[MovementIndex];

            if(gameObject.tag != "Player") {
                Movements.RemoveAt(MovementIndex);
                rotation.RemoveAt(MovementIndex);
            }
			

            if(MovementIndex == 0) {
                rb.useGravity = wasUsedGravity;
            }
		}
        else {
            rb.useGravity = wasUsedGravity;
            MovementIndex = 0;
        }

        if (destroyedBullet != null) {
            if (MainSystem.timer < destroyedBullet.time) {
                destroyedBullet.bullet.GetComponent<TimeController>().enabled = true;
                destroyedBullet.bullet.GetComponent<CapsuleCollider>().enabled = true;
                destroyedBullet.bullet.GetComponent<MeshRenderer>().enabled = true;
                destroyedBullet.bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
                destroyedBullet = null;
            }
        }
	}

    public void ReplayTime() {
        rb.useGravity = wasUsedGravity;
        ReplayIndex++;

        if (ReplayIndex < Movements.Count) {
            transform.position = (Vector3)Movements[ReplayIndex];
            transform.rotation = (Quaternion)rotation[ReplayIndex];
        }

        if(MainSystem.timer > shots[ShotIndex].time) {
            GetComponent<PlayerMovement>().Shot(shots[ShotIndex]);
            ShotIndex++;
        }
    }
}