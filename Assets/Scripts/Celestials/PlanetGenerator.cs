using System.Collections.Generic;
using UnityEngine;

namespace Celestials {
    public class PlanetGenerator : MonoBehaviour {
        private List<GameObject> planets;
        public GameObject planetObj;
        [Header("Number of Planets")] public int numOfPlanets = 1;


        // Start is called before the first frame update
        void Start() {
            for(int i = 0; i < numOfPlanets; i++) {
                GameObject planet = Instantiate(planetObj);
                planet.name = "Planet " + i;
            }
        }

        // Update is called once per frame
        void Update() {

        }
    }
}
