using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public bool LeftClickInput { get; private set; }
    public bool RightClickInput { get; private set; }
    public bool MouseWheelUp { get; private set; }
    public bool MouseWheelDown { get; private set; }
    public bool RKey { get; private set; }
    public Vector3 MovementInput { get; private set; }
    public bool DashInput { get; private set; }
    private void Update()
    {
        GetInputs();
    }
    void GetInputs()
    {
        GetLeftClickInput();
        GetRightClickInput();
        GetRKey();
        GetMovementInput();
        GetMouseWheelUp();
        GetMouseWheelDown();
        GetDashInput();
    }
    void GetLeftClickInput()
    {
        if (Input.GetButton("Fire1") == true) LeftClickInput = true;
        else LeftClickInput = false;
    }
    void GetRightClickInput()
    {
        if (Input.GetButton("Fire2") == true) RightClickInput = true;
        else RightClickInput = false;
    }
    void GetRKey()
    {
        if (Input.GetKey(KeyCode.R) == true) RKey = true;
        else RKey = false;
    }
    void GetMovementInput()
    {
        float frontInput = Input.GetAxisRaw("Horizontal");
        float sideInput = Input.GetAxisRaw("Vertical");
        MovementInput = new Vector3(sideInput, 0, frontInput).normalized;
    }
    void GetMouseWheelUp()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) MouseWheelUp = true;
        else MouseWheelUp = false;
    }
    void GetMouseWheelDown()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) MouseWheelDown = true;
        else MouseWheelDown = false;
    }

    void GetDashInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) == true) DashInput = true;
        else DashInput = false;
    }

    public void StopAllInputs()
    {
        LeftClickInput = false;
        RightClickInput = false;
        MouseWheelUp = false;
        MouseWheelDown = false;
        RKey = false;
        MovementInput = Vector3.zero.normalized;
        DashInput = false;
    }
}
