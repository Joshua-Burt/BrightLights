using System.Collections.Generic;
using UnityEngine;

namespace Celestials {
    public class StarGenerator : MonoBehaviour {
        private List<GameObject> stars;
        public GameObject starObj;
        [Header("Number of Stars")] public int numOfStars = 1;


        // Start is called before the first frame update
        void Start() {
            for(int i = 0; i < numOfStars; i++) {
                GameObject star = Instantiate(starObj);
                star.name = "Star " + i;
            }
        }

        // Update is called once per frame
        void Update() {

        }
    }
}
