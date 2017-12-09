using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

	private ArrayList Movements = new ArrayList();
	private ArrayList rotation = new ArrayList();
	private ArrayList velocity = new ArrayList();
    public ArrayList shots = new ArrayList();
    public DestroyedBullet destroyedBullet;

    private int MovementIndex = 0;
    private int DestroyedIndex = 0;
    public static bool rewinding = false;
	public bool useForce = true;

	public Rigidbody rb;
    bool wasUsedGravity;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        wasUsedGravity = rb.useGravity;
    }

	void LateUpdate ()
	{

        if (useForce) {
			if (Input.GetKey("f")) {
				rb.AddForce (Random.Range(-150f,150f), Random.Range(-150f,150f), Random.Range(-150f,150f)); 
			}
		}
			
		if (!rewinding) {

			Movements.Add (transform.position);
			rotation.Add (transform.rotation);
			velocity.Add (rb.velocity);

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
		MovementIndex--;
        rb.velocity = Vector2.zero;

		if (MovementIndex >= 0) {
			
			transform.position = (Vector3) Movements[MovementIndex];
			transform.rotation = (Quaternion) rotation[MovementIndex];
			rb.velocity = (Vector3) velocity [MovementIndex];

			Movements.RemoveAt (MovementIndex);
			rotation.RemoveAt (MovementIndex);
			velocity.RemoveAt (MovementIndex);
		}
        else {
            rb.useGravity = wasUsedGravity;
            MovementIndex = 0;
            if (gameObject.tag == "Player")
                rewinding = false;
        }

        if (destroyedBullet != null) {
            if (UIHelper.timer < destroyedBullet.time) {
                destroyedBullet.bullet.GetComponent<TimeController>().enabled = true;
                destroyedBullet.bullet.GetComponent<CapsuleCollider>().enabled = true;
                destroyedBullet.bullet.GetComponent<MeshRenderer>().enabled = true;
                destroyedBullet.bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
                destroyedBullet = null;
            }
        }
        
	}
}