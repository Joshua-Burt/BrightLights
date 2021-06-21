using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSettings : MonoBehaviour{
    public Color planetColour;
    
    private void OnValidate() {
        if(OnVariableChange != null) {
            OnVariableChange.Invoke();
        }
    }

    public delegate void OnVariableChangeDelegate();
    public event OnVariableChangeDelegate OnVariableChange;
}