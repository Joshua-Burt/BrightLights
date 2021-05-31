using System.Collections.Generic;
using UnityEngine;

namespace Celestials {
    public class Star : MonoBehaviour {
        public List<Planet> orbitingPlanets;
        public Color baseColor;
        
        public Material Material1;
        public Material Material2;
        public Material Material3;

        // Start is called before the first frame update
        void Start() {
            createMesh();
        }
        
        void createMesh() {
            gameObject.AddComponent<MeshRenderer>();
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            Material[] materials = new Material[3];
            
            materials[0] = Material1;
            materials[1] = Material2;
            materials[2] = Material3;

            renderer.materials = materials;
            
            CubeSphere sphere = gameObject.AddComponent<CubeSphere>();
            sphere.Initialize(100,100);
        }

        public bool addPlanet(Planet planet) {
            if(planet != null) {
                orbitingPlanets.Add(planet);
                if(orbitingPlanets.Contains(planet)) {
                    return true;
                }
            }

            return false;
        }
    }
}
