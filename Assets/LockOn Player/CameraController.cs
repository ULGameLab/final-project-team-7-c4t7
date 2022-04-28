using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Framing & FOV")]
    [SerializeField] private Camera camera = null;
    [SerializeField] private Transform followTransform = null;
    //[SerializeField] private Vector3 Framing = new Vector3(0, 0, 0);  //Used as offset from follow transform
    //[SerializeField] private Vector3 Framing = Vector3.zero;  //Used as offset from follow transform
    [SerializeField] private Vector3 FramingNormal = Vector3.zero;  //Used as offset from follow transform
    [SerializeField] private Vector3 LockOnFraming = Vector3.zero;
    [SerializeField, Range(1, 179)] private float LockOnFOV = 40;
    private Vector3 planarDirection;  //Camera forward on the x, z plane
    private float FOVnormal;
    private float framingLerp;

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

    [Header("Lock-On")]
    [SerializeField] private float LockOnLossTime = 15;
    [SerializeField] private float LockOnDistance = 15;  //Max distance a potential target can be
    [SerializeField] private LayerMask lockOnLayers = -1;
    private float LockOnLossTimeCurrent;

    [SerializeField] private bool lockedOn;
    //[SerializeField] Transform target;
    private Targetable target;
    public bool LockedOn { get => lockedOn; }
    //public Transform Target { get => target; }
    public Targetable Target { get => target; }

    public GameObject ForcePoint;

    /*
    [SerializeField] private Vector3 LockOnFraming = Vector3.zero;
    [SerializeField, Range(1, 179)] private float LockOnFOV = 40;
    private float FOVnormal;
    private float framingLerp;
    */

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

        FOVnormal = camera.fieldOfView;
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

        //Framing & FOV
        Vector3 framing = Vector3.Lerp(FramingNormal, LockOnFraming, framingLerp);
        //Vector3 focusPosition = followTransform.position + camera.transform.TransformDirection(Framing);
        //Vector3 focusPosition = followTransform.position + followTransform.TransformDirection(FramingNormal);  //Changed for lockon framing stuff
        Vector3 focusPosition = followTransform.position + followTransform.TransformDirection(framing);
        float FOV = Mathf.Lerp(FOVnormal,LockOnFOV, framingLerp);
        camera.fieldOfView = FOV;

        //Handle Lock-On
        if (lockedOn && target != null)
        {
            Vector3 CamToTarget = target.TargetTransform.position - camera.transform.position;
            Vector3 planarCamToTarget = Vector3.ProjectOnPlane(CamToTarget, Vector3.up);
            Quaternion LookRotation = Quaternion.LookRotation(CamToTarget, Vector3.up);

            framingLerp = Mathf.Clamp01(framingLerp + Time.deltaTime * 4);  //Takes 1/4 second to fully transition
            planarDirection = planarCamToTarget != Vector3.zero ? planarCamToTarget.normalized : planarDirection;
            targetVerticalAngle = Mathf.Clamp(LookRotation.eulerAngles.x, minVerticalAngle, maxVerticalAngle);
            targetDistance = Mathf.Clamp(targetDistance + zoom, minDistance, maxDistance);
        }
        else
        {
            framingLerp = Mathf.Clamp01(framingLerp - Time.deltaTime * 4);  //Takes 1/4 second to fully transition
            planarDirection = Quaternion.Euler(0, mouseX, 0) * planarDirection;
            targetVerticalAngle = Mathf.Clamp(targetVerticalAngle + mouseY, minVerticalAngle, maxVerticalAngle);
            targetDistance = Mathf.Clamp(targetDistance + zoom, minDistance, maxDistance);

            Debug.DrawLine(camera.transform.position, camera.transform.position + planarDirection * 1000, Color.red);
        }
        /*
        planarDirection = Quaternion.Euler(0, mouseX, 0) * planarDirection;
        targetVerticalAngle = Mathf.Clamp(targetVerticalAngle + mouseY, minVerticalAngle, maxVerticalAngle);
        targetDistance = Mathf.Clamp(targetDistance + zoom, minDistance, maxDistance);

        Debug.DrawLine(camera.transform.position, camera.transform.position + planarDirection *1000, Color.red);
        */

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
        ForcePoint.transform.position = focusPosition;

        //Handle Smoothing
        newRotation = Quaternion.Slerp(camera.transform.rotation, targetRotation, Time.deltaTime * rotationSharpness);
        newPosition = Vector3.Lerp(camera.transform.position, targetPosition, Time.deltaTime * rotationSharpness);

        //Apply
        camera.transform.rotation = newRotation;
        //camera.transform.rotation = Quaternion.LookRotation(target.position - camera.transform.position, Vector3.up);
        camera.transform.position = newPosition;

        //Lock-On loss time
        if (lockedOn && target != null)
        {
            bool valid = target.Targetable && InDistance(target) && InScreen(target) && NotBlocked(target);

            if(valid) { LockOnLossTimeCurrent = 0; }
            else { LockOnLossTimeCurrent = Mathf.Clamp(LockOnLossTimeCurrent + Time.deltaTime, 0, LockOnLossTime); }

            if (LockOnLossTimeCurrent == LockOnLossTime)
                lockedOn = false;
        }

    }
    public void ToggleLockOn(bool Toggle)
    {
        //Early Out
        if (Toggle == lockedOn)
            return;

        //Toggle
        lockedOn = !lockedOn;

        //Find target for lock on
        if(lockedOn)
        {
            //Filter targetables
            List<Targetable> targetables = new List<Targetable>();
            Collider[] colliders = Physics.OverlapSphere(transform.position, LockOnDistance, lockOnLayers);
            foreach (Collider collider in colliders)
            {
                Targetable targetable = collider.GetComponent<Targetable>();
                if (targetable != null)
                    if (targetable.Targetable)
                        if (InScreen(targetable))
                            if (NotBlocked(targetable))
                                targetables.Add(targetable);
            }
            //Find closest targetable to center of screen
            float hypotenuse;
            float smallestHypotenuse = Mathf.Infinity;
            Targetable closestTargetable = null;

            foreach(Targetable targetable in targetables)
            {
                hypotenuse = CalculateHypotenuse(targetable.TargetTransform.position);
                if (smallestHypotenuse > hypotenuse)
                {
                    closestTargetable = targetable;
                    smallestHypotenuse = hypotenuse;
                }
            }
            //Final
            target = closestTargetable;
            lockedOn = closestTargetable != null;
        }

    }

    private bool InDistance(Targetable targetable)
    {
        float distance = Vector3.Distance(transform.position, targetable.TargetTransform.position);
        return distance <= LockOnDistance;
    }

    private bool InScreen(Targetable targetable)
    {
        // Vector3 viewPortPosition = camera.WorldToViewportPoint(target.TargetTransform.position);  //Changed to fix null reference exception problem when trying to toggle lock-on
        Vector3 viewPortPosition = camera.WorldToViewportPoint(targetable.TargetTransform.position);

        if ( !(viewPortPosition.x > 0) || !(viewPortPosition.x < 1) ) { return false; }
        if ( !(viewPortPosition.y > 0) || !(viewPortPosition.y < 1) ) { return false; }
        if ( !(viewPortPosition.z > 0) ) { return false; }

        return true;
    }

    private bool NotBlocked(Targetable targetable)
    {
        Vector3 origin = camera.transform.position;
        Vector3 direction = targetable.TargetTransform.position - origin;

        float radius = 0.15f;
        float distance = direction.magnitude;

        bool notBlocked = !Physics.SphereCast(origin, radius, direction, out RaycastHit hit, distance, obstructionLayers);

        return notBlocked;
    }

    private float CalculateHypotenuse(Vector3 position)
    {
        float screenCenterX = camera.pixelWidth / 2;
        float screenCenterY = camera.pixelHeight / 2;

        Vector3 screenPosition = camera.WorldToScreenPoint(position);
        float xDelta = screenCenterX - screenPosition.x;
        float yDelta = screenCenterY - screenPosition.y;
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(xDelta, 2) + Mathf.Pow(yDelta, 2));

        return hypotenuse;
    }

}
