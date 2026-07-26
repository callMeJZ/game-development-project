using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntSo : ScriptableObject
{
    [SerializeField]
    private int _value = 9;

    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }
}
