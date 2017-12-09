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
    float timer;
	void Awake () {
		playerCamera = FindObjectOfType<Camera>();
        timeController = GetComponent<TimeController>();
	}
	
	// Update is called once per frame
	void Update () {
		HandlePlayerMovemenet();
		HandleCameraMovement();
		HandlePlayerPointingRotation();
		StartCoroutine(HandleBulletShoot());
        timer += Time.deltaTime;

    }

	void LateUpdate() {
		transform.position = new Vector3(transform.position.x, -0.1F, transform.position.z);
		transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
	}

	void HandlePlayerMovemenet() {
		transform.Translate(playerMovementSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, playerMovementSpeed * Input.GetAxis("Vertical") * Time.deltaTime, Space.World);	
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
            timeController.shots.Add(timer);

            if (weaponType == WeaponType.ar) {
				for (var i = 0; i <= 2; i++) {
					Instantiate(bulletPrefab, bulletSourcePosition.transform.position, transform.rotation);
					yield return new WaitForSeconds(0.03f);
				}
			}
			if (weaponType == WeaponType.shotgun) {
				for (var i = -2; i <= 2; i++) {
					Instantiate(bulletPrefab, bulletSourcePosition.transform.position, transform.rotation * Quaternion.Euler(i * Random.Range(-2, 2), i * Random.Range(-2, 2), 0));
				}
			}
			if (weaponType == WeaponType.handgun) {
				Instantiate(bulletPrefab, bulletSourcePosition.transform.position, transform.rotation);
			}
		}
		yield return null;
	}
}

public enum WeaponType {
	ar,
	shotgun,
	handgun

}
