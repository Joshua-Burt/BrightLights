using System;
using UnityEngine;

namespace Celestials {
    public class Planet : MonoBehaviour {
        [Range(2, 256)] public int resolution = 10;
        public bool autoUpdate = true;
        public bool hasParentStar;
        public float orbitRadius = 1000;
        public float orbitalSpeed = 0.01f;
        public float rotationalSpeed = 0.01f;
        public Vector3 axis = Vector3.up;

        public ShapeSettings shapeSettings;
        public ColourSettings colourSettings;

        [HideInInspector] public bool shapeSettingsFoldout;
        [HideInInspector] public bool colourSettingsFoldout;

        ShapeGenerator shapeGenerator;

        [SerializeField, HideInInspector] MeshFilter[] meshFilters;
        TerrainFace[] terrainFaces;

        private Transform center;

        private void Start() {
            shapeSettings = gameObject.GetComponent<ShapeSettings>();
            colourSettings = gameObject.GetComponent<ColourSettings>();
            
            colourSettings.OnVariableChange += OnColourSettingsUpdated;

            if(meshFilters == null) {
                GenerateMesh();
            }

            SphereCollider planetCollider = gameObject.AddComponent<SphereCollider>();
            SphereCollider triggerCollider = gameObject.AddComponent<SphereCollider>();
            triggerCollider.isTrigger = true;

            planetCollider.radius = shapeSettings.planetRadius;
            triggerCollider.radius = shapeSettings.planetRadius * 5;
        }

        void Initialize() {
            shapeGenerator = new ShapeGenerator(shapeSettings);

            if(meshFilters == null || meshFilters.Length == 0) {
                meshFilters = new MeshFilter[6];
            }

            terrainFaces = new TerrainFace[6];

            Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

            for(int i = 0; i < 6; i++) {
                if(meshFilters[i] == null) {
                    GameObject meshObj = new GameObject("mesh");
                    meshObj.transform.parent = transform;

                    meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                    meshObj.transform.position = transform.position;
                    meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                    meshFilters[i].sharedMesh = new Mesh();
                }

                terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            }

            GameObject star = GameObject.FindGameObjectWithTag("Star");
            hasParentStar = star != null;

            if(hasParentStar) {
                center = star.transform;
                transform.position = new Vector3(1, 0, 0) * orbitRadius + center.position;
            }
        }

        public void GeneratePlanet() {
            Initialize();
            GenerateMesh();
            GenerateColours();
        }

        public void OnShapeSettingsUpdated() {
            if(autoUpdate) {
                Initialize();
                GenerateMesh();
            }
        }

        public void OnColourSettingsUpdated() {
            if(autoUpdate) {
                Initialize();
                GenerateColours();
            }
        }

        void GenerateMesh() {
            foreach(TerrainFace face in terrainFaces) {
                face.ConstructMesh();
            }
        }

        void GenerateColours() {
            foreach(MeshFilter m in meshFilters) {
                m.GetComponent<MeshRenderer>().sharedMaterial.color = colourSettings.planetColour;
            }
        }

        void Update() {
            if(hasParentStar) {
                // Rotate around star's axis (Year type rotation)
                transform.RotateAround(center.position, axis, orbitalSpeed * Time.deltaTime);
            }

            // Rotate around planet's axis (Day/Night type rotation)
            transform.Rotate(axis, rotationalSpeed * Time.deltaTime);
        }
    }
}