using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerLuke : MonoBehaviour
{
    public Animator LuckAnimatior;
    public AudioClip LuckWalk;
    public AudioSource sounds;
    public float camSpeed = 2.5f;
    public GameObject cameraMain;
    public  Vector3 cameraPos;  //this is the target position
    public Vector3 cameraOldPos; //this is the old position or the current 
    bool down, flag;
    private PlayerInputs inputs;
    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<PlayerInputs>();
        sounds.clip = LuckWalk;
        flag = false;
        down = false;
        
    }
    void playWalk()
    {
        if (LuckAnimatior.GetBool("Run") || LuckAnimatior.GetBool("RunCrouched"))
        {
            if (!sounds.isPlaying)
            {
                Debug.Log("got in");
                sounds.Play();
            }
        }
        else
        {
            if (sounds.isPlaying)
            {
                Debug.Log("got out");
                sounds.Stop();
            }
        }
        
    }
    // in case of hit find script of trigger collision and add this GetComponent<AnimationControllerLuke>().LuckAnimatior.SetBool("Hit", true); 
    // Update is called once per frame
    void Update()
    {
        playWalk();
        if (inputs.Attack.Pressed())
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
        if (UnityEngine.Input.GetButton("Fire2"))
        {
            
            if (UnityEngine.Input.GetKey(KeyCode.A)|| UnityEngine.Input.GetKey(KeyCode.D) || UnityEngine.Input.GetKey(KeyCode.S) || UnityEngine.Input.GetKey(KeyCode.W))
            {
                
                if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift) && UnityEngine.Input.GetKey(KeyCode.W))
                {
                    
                    Debug.Log("Blockkkk walking");
                    LuckAnimatior.SetBool("Run", false);
                    LuckAnimatior.SetBool("Jump", false);
                    LuckAnimatior.SetBool("DashFront", true);
                    LuckAnimatior.SetBool("DashLeft", false);
                    LuckAnimatior.SetBool("DashRight", false);
                    LuckAnimatior.SetBool("Slash", false);
                    LuckAnimatior.SetBool("Force", false);
                    LuckAnimatior.SetBool("Block", true);
                    LuckAnimatior.SetBool("Idel", false);
                    LuckAnimatior.SetBool("RunJump", false);
                    LuckAnimatior.SetBool("RunForce", false);
                    LuckAnimatior.SetBool("RunCrouched", true);
                }
                else
                if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift) && UnityEngine.Input.GetKey(KeyCode.A))
                {
                    Debug.Log("Blockkkk walking");
                    LuckAnimatior.SetBool("Run", false);
                    LuckAnimatior.SetBool("Jump", false);
                    LuckAnimatior.SetBool("DashFront", false);
                    LuckAnimatior.SetBool("DashLeft", true);
                    LuckAnimatior.SetBool("DashRight", false);
                    LuckAnimatior.SetBool("Slash", false);
                    LuckAnimatior.SetBool("Force", false);
                    LuckAnimatior.SetBool("Block", true);
                    LuckAnimatior.SetBool("Idel", false);
                    LuckAnimatior.SetBool("RunJump", false);
                    LuckAnimatior.SetBool("RunForce", false);
                    LuckAnimatior.SetBool("RunCrouched", true);
                }
                else
                if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift) && UnityEngine.Input.GetKey(KeyCode.D))
                {
                    Debug.Log("Blockkkk walking");
                    LuckAnimatior.SetBool("Run", false);
                    LuckAnimatior.SetBool("Jump", false);
                    LuckAnimatior.SetBool("DashFront", false);
                    LuckAnimatior.SetBool("DashLeft", false);
                    LuckAnimatior.SetBool("DashRight", true);
                    LuckAnimatior.SetBool("Slash", false);
                    LuckAnimatior.SetBool("Force", false);
                    LuckAnimatior.SetBool("Block", true);
                    LuckAnimatior.SetBool("Idel", false);
                    LuckAnimatior.SetBool("RunJump", false);
                    LuckAnimatior.SetBool("RunForce", false);
                    LuckAnimatior.SetBool("RunCrouched", true);
                }
                else
                {
                    Debug.Log("Blockkkk walking");
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
                    LuckAnimatior.SetBool("RunCrouched", true);
                }

            }
            else {
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
                LuckAnimatior.SetBool("RunCrouched", false);
            }
                
        }else
        if (UnityEngine.Input.GetButtonUp("Fire2"))
        {
            down = false;
            flag = false;
        }
        else   //&& !(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
        if (UnityEngine.Input.GetKey("w"))
        {
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
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
            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
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
            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
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
               // playWalk();
            }
            
        }
        else
        if (UnityEngine.Input.GetKey("s"))
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
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
            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
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
            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
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
               // playWalk();
            }
        }
        else
        if (UnityEngine.Input.GetKey("a"))
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
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
            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
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
            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
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
              //  playWalk();
            }
        }
        else
        if (UnityEngine.Input.GetKey("d"))
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
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
            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
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
            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
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
               // playWalk();
            }
        }
        else
        if (UnityEngine.Input.GetKeyDown("space"))
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
        if (UnityEngine.Input.GetKey("e"))
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
        if (UnityEngine.Input.GetKeyDown("r"))
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
        if (UnityEngine.Input.GetKeyDown("f"))
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
        }else
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
            LuckAnimatior.SetBool("Hit", false);
            LuckAnimatior.SetBool("Dead", false);
        }
    }
}

