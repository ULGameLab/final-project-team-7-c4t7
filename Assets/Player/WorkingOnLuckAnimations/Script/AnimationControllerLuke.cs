using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerLuke : MonoBehaviour
{
    public Animator LuckAnimatior;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Slashhhhh");
            LuckAnimatior.SetBool("Run", false);
            LuckAnimatior.SetBool("Jump", false);
            LuckAnimatior.SetBool("DashFront", false);
            LuckAnimatior.SetBool("DashLeft", false);
            LuckAnimatior.SetBool("DashRight", false);
            LuckAnimatior.SetBool("Slash", true);
            LuckAnimatior.SetBool("Force", false);
            LuckAnimatior.SetBool("Block", false);
            LuckAnimatior.SetBool("Idel", false);
            LuckAnimatior.SetBool("RunJump", false);
            LuckAnimatior.SetBool("RunForce", false);
        }
        else
        if (Input.GetButton("Fire2"))
        {
            Debug.Log("Blockkkk");
            LuckAnimatior.SetBool("Run", false);
            LuckAnimatior.SetBool("Jump", false);
            LuckAnimatior.SetBool("DashFront", false);
            LuckAnimatior.SetBool("DashLeft", false);
            LuckAnimatior.SetBool("DashRight", false);
            LuckAnimatior.SetBool("Slash", false);
            LuckAnimatior.SetBool("Force", false);
            LuckAnimatior.SetBool("Block", true);
            LuckAnimatior.SetBool("Idel", false);
            LuckAnimatior.SetBool("RunJump", false);
            LuckAnimatior.SetBool("RunForce", false);
        }
        else
        if (Input.GetKey("w"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                LuckAnimatior.SetBool("Run", false);
                LuckAnimatior.SetBool("Jump", true);
                LuckAnimatior.SetBool("DashFront", false);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", false);

            }
            else
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("went  tru");
                LuckAnimatior.SetBool("Run", false);
                LuckAnimatior.SetBool("Jump", false);
                LuckAnimatior.SetBool("DashFront", true);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", false);

            }
            else
            if (Input.GetKeyDown(KeyCode.E))
            {
                LuckAnimatior.SetBool("Run", false);
                LuckAnimatior.SetBool("Jump", false);
                LuckAnimatior.SetBool("DashFront", false);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", true);
            }
            else
            {
                LuckAnimatior.SetBool("Run", true);
                LuckAnimatior.SetBool("Jump", false);
                LuckAnimatior.SetBool("DashFront", false);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", false);
            }
            
        }
        else
        if (Input.GetKey("s"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                LuckAnimatior.SetBool("Run", false);
                LuckAnimatior.SetBool("Jump", true);
                LuckAnimatior.SetBool("DashFront", false);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", false);

            }
            else
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("went  tru");
                LuckAnimatior.SetBool("Run", false);
                LuckAnimatior.SetBool("Jump", false);
                LuckAnimatior.SetBool("DashFront", true);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", false);

            }
            else
            if (Input.GetKeyDown(KeyCode.E))
            {
                LuckAnimatior.SetBool("Run", false);
                LuckAnimatior.SetBool("Jump", false);
                LuckAnimatior.SetBool("DashFront", false);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", true);
            }
            else
            {
                LuckAnimatior.SetBool("Run", true);
                LuckAnimatior.SetBool("Jump", false);
                LuckAnimatior.SetBool("DashFront", false);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", false);
            }
        }
        else
        if (Input.GetKey("a"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                LuckAnimatior.SetBool("Run", false);
                LuckAnimatior.SetBool("Jump", true);
                LuckAnimatior.SetBool("DashFront", false);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", false);

            }
            else
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("went  tru");
                LuckAnimatior.SetBool("Run", false);
                LuckAnimatior.SetBool("Jump", false);
                LuckAnimatior.SetBool("DashFront", false);
                LuckAnimatior.SetBool("DashLeft", true);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", false);

            }
            else
            if (Input.GetKeyDown(KeyCode.E))
            {
                LuckAnimatior.SetBool("Run", false);
                LuckAnimatior.SetBool("Jump", false);
                LuckAnimatior.SetBool("DashFront", false);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", true);
            }
            else
            {
                LuckAnimatior.SetBool("Run", true);
                LuckAnimatior.SetBool("Jump", false);
                LuckAnimatior.SetBool("DashFront", false);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", false);
            }
        }
        else
        if (Input.GetKey("d"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                LuckAnimatior.SetBool("Run", false);
                LuckAnimatior.SetBool("Jump", true);
                LuckAnimatior.SetBool("DashFront", false);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", false);

            }
            else
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("went  tru");
                LuckAnimatior.SetBool("Run", false);
                LuckAnimatior.SetBool("Jump", false);
                LuckAnimatior.SetBool("DashFront", false);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", true);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", false);

            }
            else
            if (Input.GetKeyDown(KeyCode.E))
            {
                LuckAnimatior.SetBool("Run", false);
                LuckAnimatior.SetBool("Jump", false);
                LuckAnimatior.SetBool("DashFront", false);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", true);
            }
            else
            {
                LuckAnimatior.SetBool("Run", true);
                LuckAnimatior.SetBool("Jump", false);
                LuckAnimatior.SetBool("DashFront", false);
                LuckAnimatior.SetBool("DashLeft", false);
                LuckAnimatior.SetBool("DashRight", false);
                LuckAnimatior.SetBool("Slash", false);
                LuckAnimatior.SetBool("Force", false);
                LuckAnimatior.SetBool("Block", false);
                LuckAnimatior.SetBool("Idel", false);
                LuckAnimatior.SetBool("RunJump", false);
                LuckAnimatior.SetBool("RunForce", false);
            }
        }
        else
        if (Input.GetKeyDown("space"))
        {
            LuckAnimatior.SetBool("Run", false);
            LuckAnimatior.SetBool("Jump", true);
            LuckAnimatior.SetBool("DashFront", false);
            LuckAnimatior.SetBool("DashLeft", false);
            LuckAnimatior.SetBool("DashRight", false);
            LuckAnimatior.SetBool("Slash", false);
            LuckAnimatior.SetBool("Force", false);
            LuckAnimatior.SetBool("Block", false);
            LuckAnimatior.SetBool("Idel", false);
            LuckAnimatior.SetBool("RunJump", false);
            LuckAnimatior.SetBool("RunForce", false);
        }
        else
        if (Input.GetKey("e"))
        {
            LuckAnimatior.SetBool("Run", false);
            LuckAnimatior.SetBool("Jump", false);
            LuckAnimatior.SetBool("DashFront", false);
            LuckAnimatior.SetBool("DashLeft", false);
            LuckAnimatior.SetBool("DashRight", false);
            LuckAnimatior.SetBool("Slash", false);
            LuckAnimatior.SetBool("Force", true);
            LuckAnimatior.SetBool("Block", false);
            LuckAnimatior.SetBool("Idel", false);
            LuckAnimatior.SetBool("RunJump", false);
            LuckAnimatior.SetBool("RunForce", false);
        }
        else
        if (Input.GetKeyDown("r"))
        {
            LuckAnimatior.SetBool("Run", false);
            LuckAnimatior.SetBool("Jump", false);
            LuckAnimatior.SetBool("DashFront", false);
            LuckAnimatior.SetBool("DashLeft", false);
            LuckAnimatior.SetBool("DashRight", false);
            LuckAnimatior.SetBool("Slash", false);
            LuckAnimatior.SetBool("Force", true);
            LuckAnimatior.SetBool("Block", false);
            LuckAnimatior.SetBool("Idel", false);
            LuckAnimatior.SetBool("RunJump", false);
            LuckAnimatior.SetBool("RunForce", false);
        }
        else
        if (Input.GetKeyDown("f"))
        {
            LuckAnimatior.SetBool("Run", false);
            LuckAnimatior.SetBool("Jump", false);
            LuckAnimatior.SetBool("DashFront", false);
            LuckAnimatior.SetBool("DashLeft", false);
            LuckAnimatior.SetBool("DashRight", false);
            LuckAnimatior.SetBool("Slash", false);
            LuckAnimatior.SetBool("Force", true);
            LuckAnimatior.SetBool("Block", false);
            LuckAnimatior.SetBool("Idel", false);
            LuckAnimatior.SetBool("RunJump", false);
            LuckAnimatior.SetBool("RunForce", false);
        }
        else
        {
            LuckAnimatior.SetBool("Run", false);
            LuckAnimatior.SetBool("Jump", false);
            LuckAnimatior.SetBool("DashFront", false);
            LuckAnimatior.SetBool("DashLeft", false);
            LuckAnimatior.SetBool("DashRight", false);
            LuckAnimatior.SetBool("Slash", false);
            LuckAnimatior.SetBool("Force", false);
            LuckAnimatior.SetBool("Block", false);
            LuckAnimatior.SetBool("Idel", true);
            LuckAnimatior.SetBool("RunJump", false);
            LuckAnimatior.SetBool("RunForce", false);
        }
    }
}

