using System.Collections.Generic;
using UnityEngine;

namespace Celestials {
    public class PlanetGenerator : MonoBehaviour {
        public GameObject planetObj;
        public float maxSize = 1000;
        public float minSize = 500;
        [Header("Number of Planets")] public int numOfPlanets = 1;

        void Start() {
            for(int i = 0; i < numOfPlanets; i++) {
                GameObject planet = Instantiate(planetObj);
                planet.name = "Planet " + i;

                Planet component = planet.GetComponent<Planet>();

                if(maxSize < minSize) {
                    maxSize = minSize;
                }

                if(GameObject.FindGameObjectWithTag("Star")) {
                    component.hasParentStar = true;
                    component.orbitalSpeed = 0.01f;
                    component.orbitRadius = Random.Range(10000, 20000);
                }

                component.colourSettings.planetColour = Random.ColorHSV();
                component.GeneratePlanet();
            }
        }
    }
}