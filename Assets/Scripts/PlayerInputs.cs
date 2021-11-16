using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public bool LeftClickInput { get; private set; }
    public bool RightClickInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool InteractionInput { get; private set; }
    public Vector3 MovementInput { get; private set; }
    private void Update()
    {
        GetInputs();
    }
    void GetInputs()
    {
        GetLeftClickInput();
        GetRightClickInput();
        GetMovementInput();
    }
    void GetLeftClickInput()
    {
        if (Input.GetButtonDown("Fire1") == true) LeftClickInput = true;
        else LeftClickInput = false;
    }
    void GetRightClickInput()
    {
        if (Input.GetButtonDown("Fire2") == true) RightClickInput = true;
        else RightClickInput = false;
    }
    void GetMovementInput()
    {
        float frontInput = Input.GetAxisRaw("Horizontal");
        float sideInput = Input.GetAxisRaw("Vertical");
        MovementInput = new Vector3(sideInput, 0, frontInput).normalized;
    }
}
