using UnityEngine;

namespace Celestials {
    public class Planet : CubeSphere {
        public Color baseColor;
        public float planetRadius = 500;
        public void createMesh() {
            transform.position = new Vector3(0,0,0);
            
            Initialize(100, planetRadius);
            gameObject.layer = 6;
        }
    }
}
