using System.Collections;
using UnityEngine;

namespace Player.PlayerMovement {
    [RequireComponent(typeof (Rigidbody))]
    public class GravityBody : MonoBehaviour {
        private GameObject planet;
        private Player _player;
        private GravityAttractor _attractor;
        private Rigidbody _rigidbody;
        private bool _isAttractorNotNull;

        void Start() {
            _isAttractorNotNull = _attractor != null;
            _player = gameObject.GetComponentInParent<Player>();
            _rigidbody = transform.GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            
            StartCoroutine(FindPlanet());
        }
    
        IEnumerator FindPlanet() {
            yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("Planet") != null);
            planet = GameObject.FindGameObjectWithTag("Planet");
            _attractor = planet.GetComponent<GravityAttractor>();
            _isAttractorNotNull = true;

            _player.currentCelestialBody = planet;
        }

        private void FixedUpdate() {
            if(_isAttractorNotNull) {
                _attractor.Attract(transform);
            }
        }
    }
}