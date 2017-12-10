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

        if(rewinding)
            RewindTime();
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
                if (GetComponent<Collider>())
                    GetComponent<Collider>().enabled = true;
            }
		}
        else {
            rb.useGravity = wasUsedGravity;
            MovementIndex = 0;
            if (GetComponent<Collider>())
                GetComponent<Collider>().enabled = true;
        }

        if (destroyedBullet != null) {
            if (MainSystem.timer < destroyedBullet.time) {
                destroyedBullet.bullet.GetComponent<TimeController>().enabled = true;
                destroyedBullet.bullet.GetComponent<CapsuleCollider>().enabled = true;
                if(destroyedBullet.bullet.GetComponent<MeshRenderer>())
                    destroyedBullet.bullet.GetComponent<MeshRenderer>().enabled = true;
                destroyedBullet.bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
                if(destroyedBullet.bullet.GetComponent<DrawFieldOfView>())
                    destroyedBullet.bullet.GetComponent<DrawFieldOfView>().enabled = true;
                if (destroyedBullet.bullet.GetComponent<Enemy>()) {
                    destroyedBullet.bullet.GetComponent<Enemy>().enabled = true;
                    destroyedBullet.bullet.GetComponent<Enemy>().killed = false;
                    transform.Find("Enemy/Volume81.001").GetComponent<SkinnedMeshRenderer>().enabled = true;
                }
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

        if(ShotIndex < shots.Count && MainSystem.timer > shots[ShotIndex].time) {
            GetComponent<PlayerMovement>().Shot(shots[ShotIndex]);
            ShotIndex++;
        }
    }
}