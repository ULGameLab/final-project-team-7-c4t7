using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnAnimations : MonoBehaviour
{
    public AudioClip LuckWalk;
    public AudioSource sounds;
    public Animator LuckAnimatior;
    private PlayerInputs inputs;
 
    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<PlayerInputs>();
        sounds.clip = LuckWalk;
    }

    void playWalk()
    {
        if (LuckAnimatior.GetBool("run"))
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
    // Update is called once per frame
    void Update()
    {
        playWalk();

      //  strafing = camControl.LockedOn;



        if (inputs.Dash.PressedDown() )
        {
           //dash animation 
        }else
        if (inputs.Attack.PressedDown())
        {
            LuckAnimatior.SetBool("force", false);
            LuckAnimatior.SetBool("run", false);
            LuckAnimatior.SetBool("blockA", false);
            LuckAnimatior.SetBool("slash", true);

        }else
        if (inputs.Attack.PressedUp())
        {
           
            LuckAnimatior.SetBool("slash", false);
        }
        else
        if (inputs.Block.PressedDown())
        {
            LuckAnimatior.SetBool("force", false);
            LuckAnimatior.SetBool("run", false);
            LuckAnimatior.SetBool("blockA", true);
            LuckAnimatior.SetBool("slash", false);
        }
        else
        if(inputs.Forward.Pressed() || inputs.Back.Pressed()|| inputs.Right.Pressed()|| inputs.Left.Pressed() )
        {
            LuckAnimatior.SetBool("force", false);
            LuckAnimatior.SetBool("run", true);
            LuckAnimatior.SetBool("blockA", false);
            LuckAnimatior.SetBool("slash", false);
        }
        else
        if(inputs.Grab.Pressed() || inputs.Push.Pressed() || inputs.Pull.Pressed())
        {
            LuckAnimatior.SetBool("force", true);
            LuckAnimatior.SetBool("run", false);
            LuckAnimatior.SetBool("blockA", false);
            LuckAnimatior.SetBool("slash", false);
        }
        else
        {
            LuckAnimatior.SetBool("force", false);
            LuckAnimatior.SetBool("run", false);
            LuckAnimatior.SetBool("blockA", false);
            LuckAnimatior.SetBool("slash", false);
        }
        /*     if (strafing)
             {
                 strafeParameter = Mathf.Clamp01(strafeParameter + Time.deltaTime * 4);
                 strafeParametersXZ = Vector3.Lerp(strafeParametersXZ, moveInput * newSpeed, moveSharpness * Time.deltaTime);
             }
             else
             {
                 strafeParameter = Mathf.Clamp01(strafeParameter - Time.deltaTime * 4);
                 strafeParametersXZ = Vector3.Lerp(strafeParametersXZ, Vector3.forward * newSpeed, moveSharpness * Time.deltaTime);
             }
             //animator.SetFloat("Forward", newSpeed);
             animator.SetFloat("Strafing", strafeParameter);
             animator.SetFloat("StrafingX", Mathf.Round(strafeParametersXZ.x * 100f) / 100f);
             animator.SetFloat("StrafingZ", Mathf.Round(strafeParametersXZ.z * 100f) / 100f);

             //Request Lock-On
        //     if (inputs.LockOn.PressedDown())
        //         camControl.ToggleLockOn(!camControl.LockedOn);
        */
        //Lightsaber

    }
}
