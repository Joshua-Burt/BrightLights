using System;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Celestials {
    public class Star : MonoBehaviour {
        public List<Planet> orbitingPlanets;
        public Color baseColor;
        public Material Material;
        public string type;
        private bool _loaded = false;
        private GameObject sphere;
        private Material sphereMaterial;
        private float yOffset;

        // Start is called before the first frame update
        void Start() {
            transform.position = new Vector3(0, 0, 0);

            int size = Random.Range(3000, 5000);

            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = new Vector3(size, size, size);
            sphere.transform.parent = gameObject.transform;

            sphere.GetComponent<Renderer>().material = Material;
            
            sphereMaterial = sphere.GetComponent<Renderer>().material;
            
            sphereMaterial.name = "_MainTex";
            sphereMaterial.color = baseColor;
            sphereMaterial.mainTextureScale = new Vector2(10, 10);

            _loaded = true;
        }

        private void OnValidate() {
            if(_loaded) {
                sphereMaterial.color = baseColor;
            }
        }

        private void Update() {
            var offset = sphereMaterial.mainTextureOffset;
            float movementSpeed = 0.0001f;
            if(Math.Abs(offset.x + movementSpeed - 1) < 0.001) {
                offset.x = 0;
            }

            if(Math.Abs(yOffset + movementSpeed - Math.PI * 2) < 0.001) {
                yOffset = 0;
            }

            offset.x += movementSpeed;
            yOffset += movementSpeed;
            
            sphereMaterial.mainTextureOffset = new Vector2(offset.x, (float) Math.Sin(yOffset));
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