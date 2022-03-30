using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleCode : MonoBehaviour
{
    private Renderer rend;
    private void Awake()
    {
        rend = this.GetComponent<Renderer>();
        rend.enabled = false;
    }
}
