using System;
using UnityEngine;

namespace Celestials {
    public class Planet : CubeSphere {
        public Color baseColor;
        public float planetRadius = 500;
        public float orbitRadius;
        public float gravitationalInfluence = 100;

        public Transform center;
        public Vector3 axis = Vector3.up;
        public float radiusSpeed = 0.5f;
        public float rotationSpeed = 80.0f;

        public bool hasParentStar;

        public void createMesh() {
            if(hasParentStar) {
                center = GameObject.FindGameObjectWithTag("Star").transform;
                transform.position = new Vector3(1,0,0) * orbitRadius + center.position;
            }
            
            Initialize(100, planetRadius);
            gameObject.layer = 6;
        }

        void Update() {
            Orbit(transform);
        }

        public void Orbit(Transform _transform) {
            if(hasParentStar) {
                _transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
                var desiredPosition = (_transform.position - center.position).normalized * orbitRadius + center.position;
                _transform.position = Vector3.MoveTowards(_transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
            }
        }
    }
}
