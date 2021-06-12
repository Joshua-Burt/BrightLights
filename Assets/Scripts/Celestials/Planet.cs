using System;
using UnityEngine;

namespace Celestials {
    [RequireComponent(typeof(GravityAttractor))]
    public class Planet : CubeSphere {
        public Color baseColor;
        public float planetRadius = 500;
        public float orbitRadius;
        public float gravitationalInfluence = 100;

        public Transform center;
        public Vector3 axis = Vector3.up;
        public float radiusSpeed = 0.5f;
        public float rotationSpeed = 0.01f;

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
            transform.rotation = Quaternion.identity;
        }

        public void Orbit(Transform _transform) {
            if(hasParentStar) {
                var centerPosition = center.position;
                _transform.RotateAround(centerPosition, axis, rotationSpeed * Time.deltaTime);
                
                // var position = _transform.position;
                //
                // var desiredPosition = (position - centerPosition).normalized * orbitRadius + centerPosition;
                // position = Vector3.MoveTowards(position, desiredPosition, Time.deltaTime * radiusSpeed);
                // _transform.position = position;
            }
        }
    }
}
