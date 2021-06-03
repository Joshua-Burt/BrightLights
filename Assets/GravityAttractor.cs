using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour {
    public float gravity = -9.81f;
    public void Attract(Transform bodyTransform) {
        Vector3 targetDirection = (bodyTransform.position - transform.position).normalized;
        Vector3 bodyUp = bodyTransform.up;
        
        Debug.DrawLine(bodyTransform.position, targetDirection, Color.red, 0, false);

        bodyTransform.rotation = Quaternion.FromToRotation(bodyUp, targetDirection) * bodyTransform.rotation;
        
        Debug.Log(targetDirection);
        
        bodyTransform.GetComponent<Rigidbody>().AddForce(targetDirection * gravity);
    }
}
