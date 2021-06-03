using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Celestials {
    public class Star : CubeSphere {
        public List<Planet> orbitingPlanets;
        public Color baseColor;
        public string type;

        // Start is called before the first frame update
        void Start() {
            createMesh();
        }

        private void createMesh() {
            transform.position = new Vector3(0,0,0);

            int size = Random.Range(3000, 5000);
            Initialize(100, size);
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
