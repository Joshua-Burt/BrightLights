using System;
using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Player.PlayerMovement.Camera {
    [RequireComponent(typeof(Rigidbody))]
    public class FirstPersonPlayerMove : MonoBehaviour {
        public float mouseXSensitivity = 150f;
        public float mouseYSensitivity = 150f;
        public LayerMask groundedMask;

        private CapsuleCollider _playerCollider;
        private Rigidbody _rigidbody;
        private Player _playerComp;
        private Transform _transform;
        private UnityEngine.Camera _camera;
        private float verticalLookRotation;
        private Vector3 smoothMoveVelocity;
        private Vector3 Movement;
        private bool grounded;

        void Awake() {
            _playerCollider = GetComponentInChildren<CapsuleCollider>();
            _playerComp = GetComponent<Player>();
            _rigidbody = GetComponent<Rigidbody>();
            _camera = UnityEngine.Camera.main;

            _transform = transform;

            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _rigidbody.useGravity = false;

            // Setting the proper layers to the layer mask
            int layer1 = 6;
            int layer2 = 7;

            LayerMask mask1 = 1 << layer1;
            LayerMask mask2 = 1 << layer2;
            
            groundedMask.value = mask1 | mask2;
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
                _playerComp.speed = Player.walkSpeed;
                // If in empty space, rotate the whole model
            } else {
                _transform.Rotate(Vector3.up * mouseX + Vector3.left * mouseY);
                _playerComp.speed = Player.flySpeed;
            }

            Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            Vector3 targetMoveAmount = moveDir * _playerComp.speed;
            Movement = Vector3.SmoothDamp(Movement, targetMoveAmount, ref smoothMoveVelocity, .01f);

            if(Input.GetButtonDown("Jump")) {
                if(grounded) {
                    _rigidbody.AddForce(_transform.up * _playerComp.jumpPower);
                }
            }

            grounded = false;
            Ray ray = new Ray(_transform.position, -_transform.up);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit, _playerCollider.height / 2 + 0.1f,groundedMask)) {
                grounded = true;
            }
            
            
        }

        private void FixedUpdate() {
            _transform.Translate(Movement * Time.deltaTime);
            // _rigidbody.MovePosition(_rigidbody.position + _transform.TransformDirection(Movement) * Time.fixedDeltaTime);
        }
    }
}
