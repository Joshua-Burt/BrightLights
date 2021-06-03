using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
    private GameObject planet;
    private GravityAttractor _attractor;
    private Rigidbody _rigidbody;
    private CharacterController _controller;
    private bool _isAttractorNotNull;

    void Start() {
        _isAttractorNotNull = _attractor != null;
        _rigidbody = transform.GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        
        _controller = GetComponent<CharacterController>();
        StartCoroutine(FindPlanet());
    }
    
    IEnumerator FindPlanet() {
        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("Planet") != null);
        planet = GameObject.FindGameObjectWithTag("Planet");
        _attractor = planet.GetComponent<GravityAttractor>();
        _isAttractorNotNull = _attractor != null;
    }

    private void FixedUpdate() {
        if(_isAttractorNotNull) {
            if(!_controller.isGrounded) {
                _attractor.Attract(transform);
            }
        }
    }
}
