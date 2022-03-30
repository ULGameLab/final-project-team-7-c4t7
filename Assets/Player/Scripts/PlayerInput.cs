using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public float mSens = 600f;
    float xRotation = 0f;
    public Transform playerBody;
    [SerializeField] private Transform UserCamera;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mSens * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void FixedUpdate()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(UserCamera.position, UserCamera.forward, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, UserCamera.forward * hit.distance, Color.red);

        }
        else
        {
            Debug.DrawRay(transform.position, UserCamera.forward * 1000, Color.blue);

        }

    }
}
