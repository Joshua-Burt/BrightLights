using System;
using UnityEngine;

/**
 * Script from:
 * https://forum.unity.com/threads/third-person-camera-movement-script.858673/
 *
 * By Alsda_Gamer
 * Edited By Joshua Burt
 */

public class TPMPlayerMove : MonoBehaviour {
	public float playerSpeed = 2.0f;
	public float jumpHeight = 1.0f;
	public float gravityValue = 9.81f;
	
	private CharacterController controller;
	private Vector3 playerVelocity;
	private Camera _cam;

	void Start() {
		_cam = Camera.main;
		controller = GetComponent<CharacterController>();
	}

	void Update() {
		float Horizontal = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
		float Vertical = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
		
		var camTransform = _cam.transform;
		
		// Finds the new placement of the player based on the inputted buttons
		Vector3 Movement = camTransform.right * Horizontal + camTransform.forward * Vertical;
		
		// Controls the falling of the player when they are not on the ground
		
		if(controller.isGrounded) {
			Movement.y = 0f;
			
			if(Input.GetAxis("Jump") != 0) {
				Movement.y = jumpHeight * Time.deltaTime;
			}
		} else {
			Movement.y -= Mathf.Sqrt(gravityValue * Time.deltaTime);
		}

		controller.Move(Movement);

		if(Movement.x != 0f || Movement.z != 0f) {
			Quaternion lookRotation = Quaternion.LookRotation(Movement);
			lookRotation.x = 0f;
			lookRotation.z = 0f;
				
			transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
		}
	}
}
