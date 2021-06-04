using UnityEngine;

namespace PlayerMovement.Camera { 
/**
 * Script from:
 * https://forum.unity.com/threads/third-person-camera-movement-script.858673/
 *
 * By Alsda_Gamer
 * Edited By Joshua Burt
 */

	public class ThirdPersonPlayerMove : MonoBehaviour {
		public float playerSpeed = 30.0f;
		public float jumpHeight = 15.0f;
		public float gravityValue = -9.81f;

		private CharacterController controller;
		private Rigidbody rb;
		private Vector3 playerVelocity;
		private UnityEngine.Camera _cam;

		void Start() {
			_cam = UnityEngine.Camera.main;
			rb = GetComponent<Rigidbody>();
			controller = GetComponent<CharacterController>();
		}

		void Update() {
			float Horizontal = Input.GetAxis("Horizontal") * playerSpeed;
			float Vertical = Input.GetAxis("Vertical") * playerSpeed;
		
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
					playerVelocity.y += jumpHeight / gravityValue;
				}// else {
				// // Using -0.1f instead of 0 as it prevents an issue where
				// // the character will toggle between isGrounded
				// playerVelocity.y = -0.1f;
				//}
			}// else {
			// 	playerVelocity.y -= gravityValue * Time.deltaTime;
			// }
		
			Movement += playerVelocity;
			rb.AddForce(Movement * Time.deltaTime);
		
			// Rotate the model in whatever direction the model is moving in
			if(Movement.x != 0f || Movement.z != 0f) {
				Quaternion lookRotation = Quaternion.LookRotation(Movement);
				lookRotation.x = 0f;
				lookRotation.z = 0f;
				
				transform.localRotation = Quaternion.Lerp(transform.localRotation, lookRotation, 0.1f);
			}
		}
	}
}
