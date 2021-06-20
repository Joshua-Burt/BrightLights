using System;
using System.Threading;
using Celestials;
using Player.PlayerMovement.Camera;
using PlayerMovement.Camera;
using UnityEngine;

namespace Player {
    public class Player : MonoBehaviour {
        public const float walkSpeed = 10;
        public const float runSpeed = 20;
        public const float flySpeed = 200;
        public float speed = 10;
        public float jumpPower = 300;
        public bool firstPerson = true;
        public GameObject skin;
        public GameObject currentCelestialBody;
        public GameObject currentShip;
        public LayerMask groundedMask;
        
        private Rigidbody _rigidbody;
        private GameObject _playerModel;
        private Transform _transform;
        private Camera _cam;
        private Planet _planet;
        private bool grounded;
        private Vector3 smoothMoveVelocity;
        private Vector3 Movement;
        private CapsuleCollider _collider;

        void Start() {
            _transform = transform;
            _cam = Camera.main;
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _rigidbody.useGravity = false;

            _playerModel = Instantiate(skin, _transform, false);
            _playerModel.name = "PlayerModel";
            
            _collider = GetComponentInChildren<CapsuleCollider>();

            if(firstPerson) {
                gameObject.AddComponent<FirstPersonPlayerMove>();
                if(_cam is { }) {
                    var transform1 = _cam.transform;
                    transform1.parent = _playerModel.transform;
                    transform1.position = _playerModel.transform.position + new Vector3(0, 1.5f, 1);
                }
            } else {
                gameObject.AddComponent<ThirdPersonCameraMove>();
                gameObject.AddComponent<ThirdPersonPlayerMove>();
            }
            
            // Setting the proper layers to the layer mask
            int layer1 = 6;
            int layer2 = 7;

            LayerMask mask1 = 1 << layer1;
            LayerMask mask2 = 1 << layer2;
            
            groundedMask.value = mask1 | mask2;
        }

        public void Jump() {
            if(grounded) {
                _rigidbody.AddForce(_transform.up * jumpPower);
            }
        }

        private void Update() {
            speed = currentCelestialBody != null || currentShip != null ? walkSpeed : flySpeed;

            Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            Vector3 targetMoveAmount = moveDir * speed;
            Movement = Vector3.SmoothDamp(Movement, targetMoveAmount, ref smoothMoveVelocity, .01f);
            
            grounded = false;
            Ray ray = new Ray(_transform.position, -_transform.up);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit, _collider.height / 2 + 0.1f, groundedMask)) {
                grounded = true;
            }
        }

        private void FixedUpdate() {
            _transform.Translate(Movement * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider enterCollider) {
            if(enterCollider.CompareTag("Planet")) {
                currentCelestialBody = enterCollider.gameObject;
                transform.parent = currentCelestialBody.transform;
            } else if(enterCollider.transform.CompareTag("Ship")) {
                currentShip = enterCollider.gameObject;

                // Make sure the player's rotation is lined up with the ship
                _transform.rotation = Quaternion.FromToRotation(_transform.up, enterCollider.transform.up) * _transform.rotation;
                transform.parent = currentShip.transform;
            }
        }
        
        private void OnTriggerExit(Collider exitCollider) {
            if(exitCollider.CompareTag("Planet")) {
                currentCelestialBody = null;

                if(currentShip == null) {
                    transform.parent = null;
                }
            } else if(exitCollider.transform.CompareTag("Ship")) {
                currentShip = null;
                _cam.transform.forward = _transform.forward;
                

                transform.parent = currentCelestialBody == null ? null : currentCelestialBody.transform;
            }
        }
    }
}
