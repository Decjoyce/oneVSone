using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private playermovement mover;

    private void Awake()
    {
        mover = GetComponent<playermovement>();
    }

    public void OnMove(CallbackContext context)
    {
        mover.SetInputVector(context.ReadValue<Vector2>());
    }

}
