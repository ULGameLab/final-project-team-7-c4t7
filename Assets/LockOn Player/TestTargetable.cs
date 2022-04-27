using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTargetable : MonoBehaviour, Targetable
{
    [Header("Targetable")]
    [SerializeField] private bool targetable = true;
    //[SerializeField] private bool targetTransform;
    [SerializeField] private Transform targetTransform;

    // bool Targetable.Targetable => throw new System.NotImplementedException();
    bool Targetable.Targetable { get => targetable; }
    //Transform Targetable.TargetTransform => throw new System.NotImplementedException();
    Transform Targetable.TargetTransform { get => targetTransform; }




    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
