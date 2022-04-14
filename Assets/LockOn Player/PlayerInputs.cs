using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Input
{
    public KeyCode primary;
    public KeyCode alternate;

    public bool Pressed()
    {
        return UnityEngine.Input.GetKey(primary) || UnityEngine.Input.GetKey(alternate);
    }

    public bool PressedDown()
    {
        return UnityEngine.Input.GetKeyDown(primary) || UnityEngine.Input.GetKeyDown(alternate);
    }
    public bool PressedUp()
    {
        return UnityEngine.Input.GetKeyUp(primary) || UnityEngine.Input.GetKeyUp(alternate);
    }

}

public class PlayerInputs : MonoBehaviour
{
    //Inputs
    public Input Forward, Back, Right, Left, Sprint, Jump, Dash, Attack, Block, LockOn, Grab, Push, Pull;

    public int MoveAxisForward
    {
        get
        {
            if(Forward.Pressed() && Back.Pressed() ) { return 0; }
            else if(Forward.Pressed() ) {
                Debug.Log("Forward");
                return 1; }
            else if(Back.Pressed() ) { return -1; }
            else { return 0; }
        }
    }
    public int MoveAxisRight
    {
        get
        {
            if (Right.Pressed() && Left.Pressed()) { return 0; }
            else if (Right.Pressed()) { return 1; }
            else if (Left.Pressed()) { return -1; }
            else { return 0; }
        }
    }

    public const string MouseXString = "Mouse X";
    public const string MouseYString = "Mouse Y";
    public const string MouseScrollString = "Mouse ScrollWheel";

    public static float MouseXInput
    {
        get => UnityEngine.Input.GetAxis(MouseXString);
    }

    public static float MouseYInput
    {
        get => UnityEngine.Input.GetAxis(MouseYString);
    }

    public static float MouseScrollInput
    {
        get => UnityEngine.Input.GetAxis(MouseScrollString);
    }


    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
