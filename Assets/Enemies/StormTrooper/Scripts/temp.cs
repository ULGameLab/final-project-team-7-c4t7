using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
        {
            
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isShooting", false);
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.T))
        {
            
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isShooting", false);
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Y))
        {
            
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);
            anim.SetBool("isShooting", false);
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.U))
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isShooting", true);

        }
    }
}
