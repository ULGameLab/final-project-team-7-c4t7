using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float dashSpeed = 16f;
    [SerializeField] private float sprintSpeed = 10f;

    [Header("Sharpness")]
    [SerializeField] private float rotationSharpness = 25f;
    [SerializeField] private float moveSharpness = 25f;

    [Header("Dash")]
    [SerializeField] private float DashTime = 3f;
    [SerializeField] private float DashRecharge = 1.5f;
    float dashTime;

    [Header("Lightsaber")]
    public AudioSource sounds;
    public AudioClip swish;
    //public GameObject BaseLightsaber;
    //float Damage;
    public GameObject StrikeZone;
    bool AttackReady;
    public GameObject DeflectZone;
    bool DeflectReady;
    public float DeflectWait;
    public GameObject blockZone;



    private Animator animator;
    private PlayerInputs inputs;
    private CameraController camControl;

    private float targetSpeed;
    private Quaternion targetRotation;

    private float newSpeed;
    private Vector3 newVelocity;
    private Quaternion newRotation;

    private bool strafing;
    private bool sprinting;
    private bool dashing;
    private float strafeParameter;
    private Vector3 strafeParametersXZ;

    private bool DashReady;
    
    public ForceLevelStatus status;
    private int forceReduction = 5;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inputs = GetComponent<PlayerInputs>();
        camControl = GetComponent<CameraController>();

        animator.applyRootMotion = false;

        DashReady = true;

        AttackReady = true;
        DeflectReady = true;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(inputs.MoveAxisRight, 0, inputs.MoveAxisForward);
        Vector3 camPlanarDirection = camControl.CameraPlanarDirection;
        Quaternion camPlanarRotation = Quaternion.LookRotation(camPlanarDirection);

        Debug.DrawLine(transform.position, transform.position + moveInput, Color.green);  //Consider re-enable
        Vector3 moveInputOriented = camPlanarRotation * moveInput.normalized;
        Debug.DrawLine(transform.position, transform.position + moveInput, Color.magenta);  //Consider re-enable

        strafing = camControl.LockedOn;
        /*
        if (strafing)
        {
            sprinting = inputs.Sprint.PressedDown() && (moveInput != Vector3.zero);
        }
        else
        {
            sprinting = inputs.Sprint.Pressed() && (moveInput != Vector3.zero);
        }
        if (sprinting)
            camControl.ToggleLockOn(false);
        */

        /*
        if (inputs.Dash.PressedDown())
            dashing = true;
        */

        
        if (DashReady && inputs.Dash.PressedDown() && status.getForce() >= forceReduction)
        {
            status.AddForce(-forceReduction);
            StartCoroutine(Dash());
            StartCoroutine(DashReadying());
        }
        



        /*
        if (dashing)
        {
            sprinting = inputs.Sprint.PressedDown() && (moveInput != Vector3.zero);
        }
        else
        {
            sprinting = inputs.Sprint.Pressed() && (moveInput != Vector3.zero);
        }

        */


        /*
        if (strafing)
        {
            sprinting = inputs.Sprint.PressedDown() && (moveInput != Vector3.zero);
            strafing = inputs.LockOn.Pressed() && !sprinting;
        }
        else
        {
            sprinting = inputs.Sprint.Pressed() && (moveInput != Vector3.zero);
            strafing = inputs.LockOn.PressedDown();
        }
        */

        /*
        targetSpeed = moveInput != Vector3.zero ? runSpeed : 0;

        newVelocity = moveInput * targetSpeed;
        transform.Translate(newVelocity * Time.deltaTime, Space.World);
        
        if(targetSpeed != 0)
        {
            targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = targetRotation;
        } */

        //Movement speed
        //if (sprinting ) { targetSpeed = moveInput != Vector3.zero ? sprintSpeed : 0; }
        if (dashing) { targetSpeed = moveInput != Vector3.zero ? dashSpeed : 0; }
        //else if (sprinting) { targetSpeed = moveInput != Vector3.zero ? sprintSpeed : 0; }
        else if (strafing) { targetSpeed = moveInput != Vector3.zero ? walkSpeed : 0; }
        //else if (dashing) { targetSpeed = moveInput != Vector3.zero ? dashSpeed : 0; }
        else { targetSpeed = moveInput != Vector3.zero ? runSpeed : 0; }
        newSpeed = Mathf.Lerp(newSpeed, targetSpeed, Time.deltaTime * moveSharpness);

        //Velocity
        newVelocity = moveInputOriented * newSpeed;
        transform.Translate(newVelocity * Time.deltaTime, Space.World);

        //Rotation
        if (strafing)
        {
            Vector3 toTarget = camControl.Target.TargetTransform.position - transform.position;
            Vector3 planarToTarget = Vector3.ProjectOnPlane(toTarget, Vector3.up);

            //targetRotation = Quaternion.LookRotation(camPlanarDirection);
            targetRotation = Quaternion.LookRotation(planarToTarget);
            newRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSharpness);
            transform.rotation = newRotation;
        }
        else if(targetSpeed != 0)
        {
            targetRotation = Quaternion.LookRotation(moveInputOriented);
            newRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSharpness);
            transform.rotation = newRotation;
        }


        //Animation
        if (strafing)
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


        //Request Lock-On
        if (inputs.LockOn.PressedDown())
            camControl.ToggleLockOn(!camControl.LockedOn);

        /*
        if (DashReady && UnityEngine.Input.GetKey(KeyCode.LeftShift) && status.getForce() >= forceReduction)
        {
            status.AddForce(-forceReduction);
            StartCoroutine(Dash());
            StartCoroutine(DashReadying());
        }
        */

        //Apply Dash
        /*
        if(inputs.Dash.PressedDown())
            camControl.ToggleLockOn(!camControl.LockedOn);
        */

        //Lightsaber
        if (AttackReady && inputs.Attack.PressedDown())
        {
            sounds.PlayOneShot(swish);
            StartCoroutine(Attacking());
            StartCoroutine(AttackReadying());
        }
        /*
        if (DeflectReady && Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(Deflect());
            StartCoroutine(DeflectReadying());
        }
        */
        if (DeflectReady && inputs.Block.PressedDown())
        {
            //DeflectReady = false;
            //BaseLightsaber.SetActive(false);
            blockZone.SetActive(true);
            DeflectZone.SetActive(true);

            AttackReady = false;
        }
        if (inputs.Block.PressedUp())
        {
            
            blockZone.SetActive(false);
            DeflectZone.SetActive(false);
            // BaseLightsaber.SetActive(true);
            AttackReady = true;
        }

    }

    IEnumerator Dash()
    {
        //DashReady = false;

        float startTime = Time.time;

        dashing = true;
        yield return new WaitForSeconds(DashTime/10f);
        dashing = false;
        /*
        while (Time.time < startTime + dashTime)
        {
            dashing = true;
            /*
            if (status.getForce() > 100)
                controller.Move(movement * 1.2f * DashSpeed * Time.deltaTime);
            else
                controller.Move(movement * DashSpeed * Time.deltaTime);
            

            yield return null;
        }
        */

    }

    IEnumerator DashReadying()
    {
        DashReady = false;
        yield return new WaitForSeconds(DashRecharge);
        DashReady = true;
    }

    public float GetDashRecharge()
    {
        return DashRecharge / 100;
    }


    IEnumerator Attacking()
    {
        DeflectReady = false;
        AttackReady = false;
        //BaseLightsaber.SetActive(false);
        StrikeZone.SetActive(true);
        //RepulseTimer = 0f;
        yield return new WaitForSeconds(.05f);
        StrikeZone.SetActive(false);
        //   BaseLightsaber.SetActive(true);
    }

    IEnumerator AttackReadying()
    {
        yield return new WaitForSeconds(DeflectWait);
        DeflectReady = true;
        AttackReady = true;

    }

    public float GetWait()
    {
        return DeflectWait / 100;
    }

}
