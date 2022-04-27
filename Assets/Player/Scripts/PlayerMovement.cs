using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 8f; //12f;
    private float Ospeed;
    public float gravity = -9.81f;
    public Vector3 velocity;
    public float jumpHeight = 3f;

    public Transform GroundCheck;
    public float dropDist = 0.4f;
    public LayerMask groundMask;

    public float DashSpeed;
    public float DashTime;
    public float DashRecharge = 1f;
    float dashTime;

    public Transform body;
    bool onGround;
    bool isCrouched = false;
    bool DashReady;


    Vector3 movement;



    // Start is called before the first frame update
    void Start()
    {
        Ospeed = speed;
        dashTime = DashTime / 10f; /// 10f;
        DashReady = true;
        DashSpeed = 2 * Ospeed;
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics.CheckSphere(GroundCheck.position, dropDist, groundMask);
        if (onGround && velocity.y < 0)
        {
            velocity.y = -1; //To ensure velocity is not set to 0 a few seconds before ground contact is made with the actually player model.
        }

        float x = UnityEngine.Input.GetAxis("Horizontal");
        float z = UnityEngine.Input.GetAxis("Vertical");

        movement = transform.right * x + transform.forward * z;

        if (DashReady && UnityEngine.Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
            StartCoroutine(DashReadying());
        }

        controller.Move(movement /*movement.normalized*/ * speed * Time.deltaTime);
        if (UnityEngine.Input.GetButtonDown("Jump") && onGround) { velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        IEnumerator Dash()
        {
            DashReady = false;

            float startTime = Time.time;

            while (Time.time < startTime + dashTime)
            {
                controller.Move(movement * DashSpeed * Time.deltaTime);

                yield return null;
            }

        }

        IEnumerator DashReadying()
        {
            //DashReady = false;
            yield return new WaitForSeconds(DashRecharge);
            DashReady = true;
        }

        

    }


}
