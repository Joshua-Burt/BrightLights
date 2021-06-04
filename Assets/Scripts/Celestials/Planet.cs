using UnityEngine;

namespace Celestials {
    public class Planet : CubeSphere {
        public Color baseColor;
        public float planetRadius = 500;
        public float orbitRadius = 10000;
        public float gravitationalInfluence = 100;

        public Transform center;
        public Vector3 axis = Vector3.up;
        public float radiusSpeed = 0.5f;
        public float rotationSpeed = 80.0f;

        public bool hasParentStar;

        public void createMesh() {
            Initialize(100, planetRadius);
            gameObject.layer = 6;
        }

        void Start() {
            if(GameObject.FindGameObjectWithTag("Star")) {
                hasParentStar = true;

                center = GameObject.FindGameObjectWithTag("Star").transform;

                transform.position = (transform.position - center.position).normalized * orbitRadius + center.position;
            } else {
                hasParentStar = false;
            }


        }

        void Update() {
            Orbit(transform);
        }

        public void Orbit(Transform _transform) {
            if(hasParentStar) {
                _transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
                var desiredPosition =
                    (_transform.position - center.position).normalized * orbitRadius + center.position;
                _transform.position =
                    Vector3.MoveTowards(_transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
            }
        }
    }
}
