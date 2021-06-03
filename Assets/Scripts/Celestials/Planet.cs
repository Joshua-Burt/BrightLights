using UnityEngine;

namespace Celestials {
    public class Planet : CubeSphere {
        public Color baseColor;
        
        void Start() {
            createMesh();
        }

        public void createMesh() {
            transform.position =
                new Vector3(0,0,0);

            int size = Random.Range(500, 1250);
            Initialize(100, size);
        }
    }
}
