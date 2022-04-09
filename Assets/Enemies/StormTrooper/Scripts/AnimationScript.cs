using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator anim;
    public int animationState = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (animationState) {
            case 0:
                Idle();
                break;
            case 1:
                Walk();
                break;
            case 2:
                Run();
                break;
            case 3:
                Shoot();
                break;
            default:
                Idle();
                break;
        }
        
    }
    
    void Idle()
    {
        ResetAnimation();
        anim.SetBool("isIdle", true);
    }
    void Walk()
    {
        ResetAnimation();
        anim.SetBool("isWalking", true);
    }
    void Run()
    {
        ResetAnimation();
        anim.SetBool("isRunning", true);
    }
    void Shoot()
    {
        ResetAnimation();
        anim.SetBool("isShooting", true);
    }

    void ResetAnimation()
    {
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isShooting", false);
    }
}
