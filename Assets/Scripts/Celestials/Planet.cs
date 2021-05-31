using UnityEngine;

namespace Celestials {
    public class Planet : MonoBehaviour {
        public float radius = 2;
        private GameObject planet;
        // Start is called before the first frame update
        void Start() {
            planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        // Update is called once per frame
        void Update() {
            planet.transform.localScale = new Vector3(radius,radius,radius);
        }
    }
}
