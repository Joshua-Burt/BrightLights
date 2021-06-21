using System;
using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

namespace Player.PlayerMovement.Camera {
    [RequireComponent(typeof(Rigidbody))]
    public class FirstPersonPlayerMove : MonoBehaviour {
        public float mouseXSensitivity = 150f;
        public float mouseYSensitivity = 150f;

        private Player _playerComp;
        private Transform _transform;
        private UnityEngine.Camera _camera;
        private float verticalLookRotation;
        
        

        void Awake() {
            _playerComp = GetComponent<Player>();
            _camera = UnityEngine.Camera.main;
            _transform = transform;
        }

        void Update() {
            // Player Movement
            float mouseX = Input.GetAxisRaw("Mouse X") * mouseXSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxisRaw("Mouse Y") * mouseYSensitivity * Time.deltaTime;

            // Mouse Movement controls
            // If is on a planet or in a ship, rotate the camera
            if(_playerComp.currentCelestialBody != null || _playerComp.currentShip != null) {
                _transform.Rotate(Vector3.up * mouseX);
                verticalLookRotation += mouseY;

                verticalLookRotation = Mathf.Clamp(verticalLookRotation, -80, 80);
                _camera.transform.localEulerAngles = Vector3.left * verticalLookRotation;
                // If in empty space, rotate the whole model
            } else {
                _transform.Rotate(Vector3.up * mouseX + Vector3.left * mouseY);
            }

            if(Input.GetButtonDown("Jump")) {
                _playerComp.Jump();
            }
        }

        private void FixedUpdate() {
            
            // _rigidbody.MovePosition(_rigidbody.position + _transform.TransformDirection(Movement) * Time.fixedDeltaTime);

            var camTransform = _camera.transform;
            Ray ray = new Ray(camTransform.position, camTransform.forward);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 10,LayerMask.GetMask("Selectable"))) {
                if(hit.transform.CompareTag("Planet")) {
                    Debug.Log("Planet!");
                } else if(hit.transform.CompareTag("Ship")) {
                    if(Input.GetMouseButtonDown(0)) {
                        Debug.Log("Ship!");
                    }
                }
            }
        }
    }
}
