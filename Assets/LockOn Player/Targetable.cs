using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Targetable : MonoBehaviour
public interface Targetable
{
    public bool Targetable { get; }  //Determines if object is targetable.
    public Transform TargetTransform { get; }  //What the camera will be tracking when object is targeted

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
