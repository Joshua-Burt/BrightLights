using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {
    public float radius = 2;
    // Start is called before the first frame update
    void Start() {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cube.transform.localScale = new Vector3(radius,radius,radius);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
