using UnityEngine;

/**
 * Script from:
 * https://forum.unity.com/threads/third-person-camera-movement-script.858673/
 *
 * By Alsda_Gamer
 * Edited By Joshua Burt
 */

public class TPMCameraMove : MonoBehaviour
{
	public Transform lookAt;
	public Transform Player;
	public bool yInverted = false;
	public bool xInverted = false;
	public float distance = 10.0f;
	public float sensivity = 300.0f;
	
	private const float Y_MIN = -50.0f;
	private const float Y_MAX = 50.0f;
	private float currentX = 0.0f;
	private float currentY = 0.0f;


	void LateUpdate()
	{
		currentX += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
		currentY += Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

		currentY = Mathf.Clamp(currentY, Y_MIN, Y_MAX);

		Vector3 Direction = new Vector3(0, 0, -distance);
		Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
		
		if(!yInverted) {
			transform.position = lookAt.position + rotation * Direction * -1;
		} else {
			transform.position = lookAt.position + rotation * Direction;
		}

		transform.LookAt(lookAt.position);
	}
}