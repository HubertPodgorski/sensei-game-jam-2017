using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float playerMovementSpeed = 0.2f;
	private static float playerRotateSpeed = 30f;
	private Camera playerCamera;
	public GameObject bulletPrefab;
	public GameObject bulletSourcePosition;
	public WeaponType weaponType;
	public Vector3 cameraTransform = new Vector3(-10, 15, -10);
    TimeController timeController;

    public AudioClip soundShotgun;
    public AudioClip soundHandgun;

    private bool footstepSFXready = true;
    public AudioClip[] footstepSFX;
    private int footstepSFXid = 0;
    private int repeatFootstepSFXtimer;

    private bool canShotgun = true;

    void Awake () {
		playerCamera = FindObjectOfType<Camera>();
        timeController = GetComponent<TimeController>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!TimeController.rewinding && MainSystem.activePlayer == gameObject) {
            HandlePlayerMovemenet();
            HandleCameraMovement();
            HandlePlayerPointingRotation();
            StartCoroutine(HandleBulletShoot());
        }
        else if (!TimeController.rewinding && MainSystem.activePlayer != gameObject) {
            timeController.ReplayTime();
            
        }
    }

	void LateUpdate() {
		transform.position = new Vector3(transform.position.x, -0.1F, transform.position.z);
		transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

	void HandlePlayerMovemenet() {
		transform.Translate(playerMovementSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, playerMovementSpeed * Input.GetAxis("Vertical") * Time.deltaTime, Space.World);

        if(playerMovementSpeed * Input.GetAxis("Horizontal") != 0 || playerMovementSpeed * Input.GetAxis("Vertical") != 0) {
            GetComponent<Animator>().SetBool("Run", true);
        }
        else GetComponent<Animator>().SetBool("Run", false);

        if (footstepSFXready && ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D)))) {
            footstepSFXready = false;
            Invoke("RepeatFootstepSFX", 0.5f);

            footstepSFXid = Random.Range(0, footstepSFX.Length);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(footstepSFX[footstepSFXid]);
        }

    }

	void HandlePlayerPointingRotation() {
		
    	Plane playerPlane = new Plane(Vector3.up, transform.position);
		
    	Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
    	float hitdist = 0.0f;

    	if (playerPlane.Raycast (ray, out hitdist)) {
			
        	Vector3 targetPoint = ray.GetPoint(hitdist);
			
        	Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			
        	transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerRotateSpeed * Time.deltaTime);
		}
	}

	void HandleCameraMovement() {
		playerCamera.transform.position = transform.position + cameraTransform;
	}

    
	IEnumerator HandleBulletShoot() {
		if (Input.GetMouseButtonDown(0)) {

            if (weaponType == WeaponType.ar) {
				for (var i = 0; i <= 2; i++) {
					Instantiate(bulletPrefab, bulletSourcePosition.transform.position, transform.rotation);
                    timeController.shots.Add(new DestroyedBullet(MainSystem.timer, bulletSourcePosition.transform.position, transform.rotation, bulletPrefab, null));
                    yield return new WaitForSeconds(0.03f);
				}
			}

            if (weaponType == WeaponType.shotgun && canShotgun) {
                canShotgun = false;
                Invoke("RepeatCanShotgun", 1.2f);
                for (var i = -2; i <= 2; i++) {
                    Quaternion q = transform.rotation * Quaternion.Euler(i * Random.Range(-2, 2), i * Random.Range(-2, 2), 0);
                    Instantiate(bulletPrefab, bulletSourcePosition.transform.position, q);
                    timeController.shots.Add(new DestroyedBullet(MainSystem.timer, bulletSourcePosition.transform.position, q, bulletPrefab, soundShotgun));
                    Camera.main.GetComponent<AudioSource>().PlayOneShot(soundShotgun);
                }
			}

			if (weaponType == WeaponType.handgun) {
				Instantiate(bulletPrefab, bulletSourcePosition.transform.position, transform.rotation);
                timeController.shots.Add(new DestroyedBullet(MainSystem.timer, bulletSourcePosition.transform.position, transform.rotation, bulletPrefab, soundHandgun));
                Camera.main.GetComponent<AudioSource>().PlayOneShot(soundHandgun);
            }
		}
		yield return null;
	}

    public void Shot(DestroyedBullet db) {
        Instantiate(db.bullet, db.position, db.rotation);
    }

    void RepeatFootstepSFX()
    {
        footstepSFXready = true;
    }

    void RepeatCanShotgun() {
        canShotgun = true;
    }
}

public enum WeaponType {
	ar,
	shotgun,
	handgun

}

