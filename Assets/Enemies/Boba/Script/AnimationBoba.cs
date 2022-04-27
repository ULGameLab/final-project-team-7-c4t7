using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBoba : MonoBehaviour
{
    public Animator anim;
    public int animationState = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (animationState)
        {
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
            case 4:
                Fly();
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
        anim.SetBool("isWalk", true);
    }
    void Run()
    {
        ResetAnimation();
        anim.SetBool("isRun", true);
    }
    void Shoot()
    {
        ResetAnimation();
        anim.SetBool("isShoot", true);
    }

    void Fly()
    {
        ResetAnimation();
        anim.SetBool("isFly", true);
    }
    void ResetAnimation()
    {
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalk", false);
        anim.SetBool("isRun", false);
        anim.SetBool("isShoot", false);
        anim.SetBool("isFly", false);
    }
}
