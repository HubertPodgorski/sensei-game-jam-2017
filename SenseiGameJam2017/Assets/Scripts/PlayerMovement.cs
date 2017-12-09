using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private static float playerMovementSpeed = 0.2f;
	private Camera playerCamera;
	// Use this for initialization
	void Start () {
		playerCamera = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		HandlePlayerMovemenet();
		HandleCameraMovement();
		HandlePlayerPointingRotation();
	}

	void HandlePlayerMovemenet() {
		transform.Translate(playerMovementSpeed * Input.GetAxis("Horizontal"), 0f, playerMovementSpeed * Input.GetAxis("Vertical"), Space.World);
	}

	void HandlePlayerPointingRotation() {
		var mousePosition = Input.mousePosition;
		mousePosition.z = - playerCamera.transform.position.z;
		Vector3 targetLocation = Camera.main.ScreenToWorldPoint(mousePosition);
		targetLocation.y = transform.position.y;
		transform.LookAt(targetLocation);
	}

	void HandleCameraMovement() {
		var cameraHeight = 10;
		var positionFromBack = 10;
		playerCamera.transform.position = transform.position + new Vector3(0, cameraHeight, -positionFromBack);
	}
}
