using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Player.PlayerMovement.Camera {
    public class FirstPersonPlayerMove : MonoBehaviour {
        public float mouseXSensitivity = 50f;
        public float mouseYSensitivity = 50f;

        private Rigidbody _rigidbody;
        private Player _player;
        private Transform _transform;
        private UnityEngine.Camera _camera;
        private Collider _collider;

        void Awake() {
            _player = GetComponent<Player>();
            _rigidbody = GetComponent<Rigidbody>();
            _camera = UnityEngine.Camera.main;

            _collider = _player.skin.GetComponent<CapsuleCollider>();

            _transform = transform;
            // if(_camera != null) {
            //     _camera.transform.position = _player.transform.position;
            // }
        }

        void Update() {
            // Player Movement
            float Horizontal = Input.GetAxis("Horizontal") * _player.speed;
            float Vertical = Input.GetAxis("Vertical") * _player.speed;

            Vector3 Movement = _transform.right * Horizontal + _transform.forward * Vertical + _rigidbody.velocity;
            
            if(Input.GetAxis("Jump") != 0) {
                if(IsGrounded()) {
                    Debug.Log("JUMPING");
                    Movement += _transform.up * _player.jumpPower;
                }
            } else {
                Debug.Log("NOT JUMPING");
            }

            _transform.Translate(Movement * Time.deltaTime);
            
            // Player Camera Movement
            
            float mouseX = Input.GetAxisRaw("Mouse X") * mouseXSensitivity;
            float mouseY = Input.GetAxisRaw("Mouse Y") * mouseYSensitivity;


            if(mouseX != 0 || mouseY != 0) {
                var transformRotation = _transform.rotation;
                var cameraRotation = _camera.transform.rotation;
                transformRotation.y += mouseX;
                cameraRotation.x += mouseY;
                
                _transform.rotation.Set(0,transformRotation.y,0,0);
                _camera.transform.rotation.Set(cameraRotation.x,0,0,0);
            }
        }
        
        bool IsGrounded() {
            var bounds = _collider.bounds;
            return Physics.CheckCapsule(bounds.center,new Vector3(bounds.center.x,bounds.min.y-0.1f,bounds.center.z),1);
        }
    }
}
