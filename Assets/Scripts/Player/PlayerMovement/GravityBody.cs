using System;
using System.Collections;
using Celestials;
using UnityEngine;

namespace Player.PlayerMovement {
    [RequireComponent(typeof (Rigidbody))]
    public class GravityBody : MonoBehaviour {
        private GameObject planet;
        private Player _player;
        private GravityAttractor _attractor;
        private Rigidbody _rigidbody;

        void Start() {
            _player = gameObject.GetComponent<Player>();
            _rigidbody = transform.GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            StartCoroutine(FindPlanet());
        }
    
        IEnumerator FindPlanet() {
            yield return new WaitUntil(() => _player.currentCelestialBody != null);
            planet = _player.currentCelestialBody;
            _attractor = planet.GetComponent<GravityAttractor>();
            _player.currentCelestialBody = planet;
        }


        private void FixedUpdate() {
            if(_attractor != null) {
                _attractor.Attract(transform);
            }
        }
    }
}
