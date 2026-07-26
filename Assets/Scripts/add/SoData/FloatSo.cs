using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatSo : ScriptableObject
{
    [SerializeField]
    private float _value = 9;

    public float Value
    {
        get { return _value; }
        set { _value = value; }
    }
}
