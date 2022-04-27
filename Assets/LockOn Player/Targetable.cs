using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Targetable : MonoBehaviour
public interface Targetable
{
    //Determines if object is targetable.
    public bool Targetable { get; }
    //What the camera will be tracking when object is targeted
    public Transform TargetTransform { get; }

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
