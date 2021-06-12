using System.Collections;
using System.Collections.Generic;
using Player.PlayerMovement;
using UnityEngine;

public class Ship : MonoBehaviour {
    public bool isBeingFlown;
    private Transform _transform;
    public GameObject currentCelestialBody;
    private GravityAttractor _attractor;

    // Start is called before the first frame update
    void Start() {
        isBeingFlown = false;
        _transform = transform;
    }

    private void OnTriggerEnter(Collider enterCollider) {
        if(enterCollider.CompareTag("Planet")) {
            currentCelestialBody = enterCollider.gameObject;
            _attractor = currentCelestialBody.GetComponent<GravityAttractor>();
        }
    }

    private void OnTriggerExit(Collider exitCollider) {
        if(exitCollider.CompareTag("Planet")) {
            currentCelestialBody = null;
            _attractor = null;
        }
    }
    
    private void FixedUpdate() {
        if(currentCelestialBody != null) {
            _attractor.Attract(_transform);
        }
    }
}
