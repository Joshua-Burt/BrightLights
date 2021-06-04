using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour {
    public float gravity = -9.81f;
    public void Attract(Transform bodyTransform) {
        Vector3 targetDirection = (bodyTransform.position - transform.position).normalized;
        Vector3 bodyUp = bodyTransform.up;
        
        bodyTransform.rotation = Quaternion.FromToRotation(bodyUp, targetDirection) * bodyTransform.rotation;
        bodyTransform.GetComponent<Rigidbody>().AddForce(targetDirection * gravity);
    }
}
