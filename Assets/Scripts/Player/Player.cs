using System.Threading;
using Celestials;
using Player.PlayerMovement.Camera;
using PlayerMovement.Camera;
using UnityEngine;

namespace Player {
    public class Player : MonoBehaviour {
        public float speed = 10;
        public float runSpeed = 20;
        public float jumpPower = 10;
        public bool firstPerson = true;
        public GameObject skin;
        public GameObject currentCelestialBody;

        private GameObject _playerModel;
        private Transform _transform;

        void Start() {
            _transform = transform;
            _playerModel = Instantiate(skin, transform, false);
            _playerModel.name = "PlayerModel";
            if(firstPerson) {
                gameObject.AddComponent<FirstPersonPlayerMove>();
                if(Camera.main is { }) {
                    Camera.main.transform.parent = _playerModel.transform;
                    Camera.main.transform.position = _playerModel.transform.position + new Vector3(0, 1f, 1);
                }
            } else {
                gameObject.AddComponent<ThirdPersonCameraMove>();
                gameObject.AddComponent<ThirdPersonPlayerMove>();
            }
        }

        void Update() {
            _transform.position = _playerModel.transform.position;

            if(currentCelestialBody != null) {
                currentCelestialBody.GetComponent<Planet>().Orbit(_transform);
            }
        }
        
        private void OnTriggerEnter(Collider enterCollider) {
            if(enterCollider.CompareTag("Planet")) {
                currentCelestialBody = enterCollider.gameObject;
            }
        }
        
        private void OnTriggerExit(Collider enterCollider) {
            if(enterCollider.CompareTag("Planet")) {
                currentCelestialBody = null;
            }
        }
    }
}
