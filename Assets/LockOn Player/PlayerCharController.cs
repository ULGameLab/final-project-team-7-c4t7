using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float sprintSpeed = 10f;

    [Header("Sharpness")]
    [SerializeField] private float rotationSharpness = 25f;
    [SerializeField] private float moveSharpness = 25f;


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
    private float strafeParameter;
    private Vector3 strafeParametersXZ;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inputs = GetComponent<PlayerInputs>();
        camControl = GetComponent<CameraController>();

        animator.applyRootMotion = false;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(inputs.MoveAxisRight, 0, inputs.MoveAxisForward);
        Vector3 camPlanarDirection = camControl.CameraPlanarDirection;
        Quaternion camPlanarRotation = Quaternion.LookRotation(camPlanarDirection);

        // Debug.DrawLine(transform.position, transform.position + moveInput, Color.green);  //Consider re-enable
        Vector3 moveInputOriented = camPlanarRotation * moveInput.normalized;
        // Debug.DrawLine(transform.position, transform.position + moveInput, Color.magenta);  //Consider re-enable

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
        if (sprinting ) { targetSpeed = moveInput != Vector3.zero ? sprintSpeed : 0; }
        else if (strafing) { targetSpeed = moveInput != Vector3.zero ? walkSpeed : 0; }
        else { targetSpeed = moveInput != Vector3.zero ? runSpeed : 0; }
        newSpeed = Mathf.Lerp(newSpeed, targetSpeed, Time.deltaTime * moveSharpness);

        //Velocity
        newVelocity = moveInputOriented * newSpeed;
        transform.Translate(newVelocity * Time.deltaTime, Space.World);

        //Rotation
        if (strafing)
        {
            targetRotation = Quaternion.LookRotation(camPlanarDirection);
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
        animator.SetFloat("Strafing", strafeParameter);
        animator.SetFloat("StrafingX", Mathf.Round(strafeParametersXZ.x * 100f) / 100f);
        animator.SetFloat("StrafingZ", Mathf.Round(strafeParametersXZ.z * 100f) / 100f);

    }
}
