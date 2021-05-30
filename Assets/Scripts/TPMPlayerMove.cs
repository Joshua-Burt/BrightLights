using System;
using UnityEditor.UIElements;
using UnityEngine;

/**
 * Script from:
 * https://forum.unity.com/threads/third-person-camera-movement-script.858673/
 *
 * By Alsda_Gamer
 * Edited By Joshua Burt
 */

public class TPMPlayerMove : MonoBehaviour {
	public float playerSpeed = 30.0f;
	public float jumpHeight = 15.0f;
	public float gravityValue = 9.81f;

	private CharacterController controller;
	private Vector3 playerVelocity;
	private Camera _cam;

	void Start() {
		_cam = Camera.main;
		controller = GetComponent<CharacterController>();
	}

	void Update() {
		float Horizontal = Input.GetAxis("Horizontal") * playerSpeed * 100 * Time.deltaTime;
		float Vertical = Input.GetAxis("Vertical") * playerSpeed * 100 * Time.deltaTime;
		
		var camTransform = _cam.transform;
		
		// This is used so the vertical position of the camera does not move
		// the player up or down while they move laterally
		Vector3 v3 = camTransform.forward;
		v3.y = 0;
		v3.Normalize();
		
		// Finds the new placement of the player based on the inputted buttons
		Vector3 Movement = camTransform.right * Horizontal + v3 * Vertical;
		
		if(controller.isGrounded) {
			if(Input.GetAxis("Jump") != 0) {
				playerVelocity.y += jumpHeight * gravityValue;
			} else {
				// Using -0.1f instead of 0 as it prevents an issue where
				// the character will toggle between 
				playerVelocity.y = -0.1f;
			}
		} else {
			playerVelocity.y -= Mathf.Sqrt(gravityValue * Time.deltaTime);
		}

		Movement += playerVelocity;
		controller.Move(Movement * Time.deltaTime);

		// Rotate the model in whatever direction the model is moving in
		if(Movement.x != 0f || Movement.z != 0f) {
			Quaternion lookRotation = Quaternion.LookRotation(Movement);
			lookRotation.x = 0f;
			lookRotation.z = 0f;
				
			transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
		}
	}
}
