using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private static float playerMovementSpeed = 0.2f;
	private static float playerRotateSpeed = 30f;
	private Camera playerCamera;
	public GameObject bulletPrefab;
	public GameObject bulletSourcePosition;
	public WeaponType weaponType;
	void Start () {
		playerCamera = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		HandlePlayerMovemenet();
		HandleCameraMovement();
		HandlePlayerPointingRotation();
		StartCoroutine(HandleBulletShoot());
	}

	void HandlePlayerMovemenet() {
		transform.Translate(playerMovementSpeed * Input.GetAxis("Horizontal"), 0f, playerMovementSpeed * Input.GetAxis("Vertical"), Space.World);
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
		playerCamera.transform.position = transform.position + new Vector3(0, 10, -10);
	}

	IEnumerator HandleBulletShoot() {
		if (Input.GetMouseButtonDown(0)) {
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
