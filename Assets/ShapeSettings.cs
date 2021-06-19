using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShapeSettings : MonoBehaviour{

    public float planetRadius = 1;
    
    private void OnValidate() {
        if(OnVariableChange != null) {
            OnVariableChange.Invoke();
        }
    }

    public delegate void OnVariableChangeDelegate();
    public event OnVariableChangeDelegate OnVariableChange;
}
