using System.Collections.Generic;
using UnityEngine;

namespace Celestials {
    public class PlanetGenerator : MonoBehaviour {
        public GameObject planetObj;
        public float maxSize = 1000;
        public float minSize = 500;
        [Header("Number of Planets")] public int numOfPlanets = 1;


        private List<GameObject> planets;

        // Start is called before the first frame update
        void Start() {
            for(int i = 0; i < numOfPlanets; i++) {
                GameObject planet = Instantiate(planetObj);
                planet.name = "Planet " + i;
                
                Planet component = planet.GetComponent<Planet>();

                if(maxSize < minSize) {
                    maxSize = minSize;
                }
                
                component.planetRadius = Random.Range(minSize, maxSize);
                
                // Setting the sphere of influence of the planet, 9x the radius
                planet.GetComponent<SphereCollider>().radius = component.planetRadius * 9;

                if(GameObject.FindGameObjectWithTag("Star")) {
                    component.hasParentStar = true;
                    component.rotationSpeed = 0.01f;
                    component.orbitRadius = Random.Range(10000, 20000);
                }

                component.createMesh();
            }
        }

        // Update is called once per frame
        void Update() {

        }
    }
}
