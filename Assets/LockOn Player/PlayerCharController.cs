using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharController : MonoBehaviour
{
    [Header("Movement")]
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
        Vector3 moveInput = new Vector3(inputs.MoveAxisRight, 0, inputs.MoveAxisForward).normalized;
        Vector3 camPlanarDirection = camControl.CameraPlanarDirection;
        Quaternion camPlanarRotation = Quaternion.LookRotation(camPlanarDirection);

        Debug.DrawLine(transform.position, transform.position + moveInput, Color.green);
        moveInput = camPlanarRotation * moveInput;
        Debug.DrawLine(transform.position, transform.position + moveInput, Color.magenta);
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
        if (inputs.Sprint.Pressed() ) { targetSpeed = moveInput != Vector3.zero ? sprintSpeed : 0; }
        else { targetSpeed = moveInput != Vector3.zero ? runSpeed : 0; }
        newSpeed = Mathf.Lerp(newSpeed, targetSpeed, Time.deltaTime * moveSharpness);

        //Velocity
        newVelocity = moveInput * newSpeed;
        transform.Translate(newVelocity * Time.deltaTime, Space.World);

        //Rotation
        if(targetSpeed != 0)
        {
            targetRotation = Quaternion.LookRotation(moveInput);
            newRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSharpness);
            transform.rotation = newRotation;
        }


        //Animation
        animator.SetFloat("Forward", newSpeed);

    }
}
