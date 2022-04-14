using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Framing")]
    [SerializeField] private Camera camera = null;
    [SerializeField] private Transform followTransform = null;
    [SerializeField] private Vector3 Framing = new Vector3(0, 0, 0);  //Used as offset from follow transform
    private Vector3 planarDirection;  //Camera forward on the x, z plane

    [Header("Sensitivity")]
    [SerializeField] [Range(0f, 10f)] private float LookSensitivity = 3.5f;
    [SerializeField] [Range(0f, 10f)] private float ZoomSensitivity = 5f;

    [Header("Rotation")]
    [SerializeField] private float rotationSharpness = 50;
    [SerializeField] private bool invertX = false;  //Boolean to let you know if x-axis should be inverted
    [SerializeField] private bool invertY = false;  //Boolean to let you know if y-axis should be inverted
    [SerializeField] private float defaultVerticalAngle = 20f;
    [SerializeField] [Range(-90, 90)] private float minVerticalAngle = -90;
    [SerializeField] [Range(-90, 90)] private float maxVerticalAngle = 90;
    private Quaternion targetRotation;
    private float targetVerticalAngle;
    private Quaternion newRotation;

    [Header("Distance")]
    //[SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float defaultDistance = 5f;  //How far away camera should start from player
    [SerializeField] private float minDistance = 0f;  //How close the camera can get to the player
    [SerializeField] private float maxDistance = 10f;  //How far the camera can get from the player
    private Vector3 targetPosition;  //Where the camera should be
    private float targetDistance;  //How far the camera should be from the player
    private Vector3 newPosition;


    [Header("Obstructions")]
    [SerializeField] private float checkRadius = 0.2f;  //How big of a radius for spherecast to check
    [SerializeField] private LayerMask obstructionLayers = -1;  //-1 sets it for all layers
    private List<Collider> ignoreColliders = new List<Collider>();  //List of colliders to be ignored

    public Vector3 CameraPlanarDirection { get => planarDirection; }

    /*
    private Vector3 planarDirection;  //Camera forward on the x, z plane
    private Vector3 targetPosition;  //Where the camera should be
    private float targetDistance;  //How far the camera should be from the player
    private Quaternion targetRotation;
    private float targetVerticalAngle;
    */

    private void OnValidate()
    {
        defaultDistance = Mathf.Clamp(defaultDistance, minDistance, maxDistance);
        defaultVerticalAngle = Mathf.Clamp(defaultVerticalAngle, minVerticalAngle, maxVerticalAngle);
    }

    // Start is called before the first frame update
    void Start()
    {
        ignoreColliders.AddRange(GetComponentsInChildren<Collider>());  //Adds all of Player's colliders to ignore list

        planarDirection = followTransform.forward;

        //Targets
        targetDistance = defaultDistance;
        targetRotation = Quaternion.LookRotation(planarDirection) * Quaternion.Euler(targetVerticalAngle, 0, 0);
        targetPosition = followTransform.position - (targetRotation * Vector3.forward) * targetDistance;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        //Handle Inputs
        float mouseX = PlayerInputs.MouseXInput * LookSensitivity;
        float mouseY = PlayerInputs.MouseYInput * LookSensitivity;
        float zoom = -PlayerInputs.MouseScrollInput * ZoomSensitivity;

        if (invertX) { mouseX *= -1f; }
        if (invertY) { mouseY *= -1f; }

        Vector3 focusPosition = followTransform.position + camera.transform.TransformDirection(Framing);

        planarDirection = Quaternion.Euler(0, mouseX, 0) * planarDirection;
        targetVerticalAngle = Mathf.Clamp(targetVerticalAngle + mouseY, minVerticalAngle, maxVerticalAngle);
        targetDistance = Mathf.Clamp(targetDistance + zoom, minDistance, maxDistance);

        Debug.DrawLine(camera.transform.position, camera.transform.position + planarDirection *1000, Color.red);

        //Handle Obstructions
        float smallestDistance = targetDistance;
        RaycastHit[] Hits = Physics.SphereCastAll(focusPosition, checkRadius, targetRotation * -Vector3.forward, targetDistance, obstructionLayers);
        if (Hits.Length != 0)
            foreach (RaycastHit hit in Hits)
                if (!ignoreColliders.Contains(hit.collider))
                    if (hit.distance < smallestDistance)
                        smallestDistance = hit.distance;

        //Final Targets
        targetRotation = Quaternion.LookRotation(planarDirection) * Quaternion.Euler(targetVerticalAngle, 0, 0);
        targetPosition = focusPosition - (targetRotation * Vector3.forward) * smallestDistance;

        //Handle Smoothing
        newRotation = Quaternion.Slerp(camera.transform.rotation, targetRotation, Time.deltaTime * rotationSharpness);
        newPosition = Vector3.Lerp(camera.transform.position, targetPosition, Time.deltaTime * rotationSharpness);

        //Apply
        camera.transform.rotation = newRotation;
        camera.transform.position = newPosition;

    }
}
