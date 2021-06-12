using System.Threading;
using Celestials;
using Player.PlayerMovement.Camera;
using PlayerMovement.Camera;
using UnityEngine;

namespace Player {
    public class Player : MonoBehaviour {
        public float speed = 10;
        public float runSpeed = 20;
        public float jumpPower = 300;
        public bool firstPerson = true;
        public GameObject skin;
        public GameObject currentCelestialBody;
        public GameObject currentShip;

        private GameObject _playerModel;
        private Transform _transform;
        private Camera _cam;
        private Planet _planet;

        void Start() {
            _transform = transform;
            _cam = Camera.main;

            _playerModel = Instantiate(skin, _transform, false);
            _playerModel.name = "PlayerModel";

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
        }

        void Update() {
            // _transform.position = _playerModel.transform.position;

            if(currentCelestialBody != null) {
                currentCelestialBody.GetComponent<Planet>().Orbit(_transform);
            }
        }
        
        private void OnTriggerEnter(Collider enterCollider) {
            if(enterCollider.CompareTag("Planet")) {
                currentCelestialBody = enterCollider.gameObject;
                
            } else if(enterCollider.transform.CompareTag("Ship")) {
                currentShip = enterCollider.gameObject;
                // Make sure the player's rotation is lined up with the ship
                _transform.rotation = Quaternion.FromToRotation(_transform.up, enterCollider.transform.up) * _transform.rotation;
            }
        }
        
        private void OnTriggerExit(Collider exitCollider) {
            if(exitCollider.CompareTag("Planet")) {
                currentCelestialBody = null;
            } else if(exitCollider.transform.CompareTag("Ship")) {
                currentShip = null;
                _cam.transform.forward = _transform.forward;
            }
        }
    }
}
