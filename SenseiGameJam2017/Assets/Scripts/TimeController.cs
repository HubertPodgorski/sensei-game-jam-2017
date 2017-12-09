using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

	private ArrayList Movements = new ArrayList();
	private ArrayList rotation = new ArrayList();
	private ArrayList velocity = new ArrayList();
    public ArrayList shots = new ArrayList();

    private int MovementIndex = 0;
	public static bool rewinding;
	public bool useForce = true;

	public Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
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

		if(Input.GetKey("e")) {
			rewinding = true;
			RewindTime();
		} else {
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

		if (MovementIndex < 0) {
			MovementIndex = 0;
		} 
	}
}