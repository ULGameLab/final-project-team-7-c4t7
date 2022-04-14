using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharController : MonoBehaviour
{
    private Animator animator;
    private PlayerInputs inputs;
    private CameraController camControl;

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

        Debug.DrawLine(transform.position, transform.position + moveInput * 1000, Color.green);
    }
}
