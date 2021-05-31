using System.Collections.Generic;
using UnityEngine;

namespace Celestials {
    public class StarGenerator : MonoBehaviour {
        private List<Star> stars;
        public Star star;


        // Start is called before the first frame update
        void Start() {
            Instantiate(star);
        }

        // Update is called once per frame
        void Update() {

        }

        
    }
}
