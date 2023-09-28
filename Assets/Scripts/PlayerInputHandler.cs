using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private playermovement mover;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var movers = FindObjectsOfType<playermovement>();
        var index = playerInput.playerIndex;
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        print(index);
    }

    public void OnMove(CallbackContext context)
    {
        if(!GameManager.instance.gamePaused)
            mover.SetInputVector(context.ReadValue<Vector2>());
    }

    //dec
    public void Shoot(CallbackContext ctx)
    {
        if (!GameManager.instance.gamePaused)
        {

        }
    }

    public void ReadyUp(CallbackContext ctx)
    {
        if (mover.GetPlayerIndex() == 0)
            GameManager.instance.ready_p1 = true;
        if (mover.GetPlayerIndex() == 1)
            GameManager.instance.ready_p2 = true;
    }

    public void OnDisconnected()
    {
        if (mover.GetPlayerIndex() == 0)
            GameManager.instance.ready_p1 = false;
        if (mover.GetPlayerIndex() == 1)
            GameManager.instance.ready_p2 = true;
    }

    public void PauseGame(CallbackContext ctx)
    {
        if (GameManager.instance.ready_p1 && GameManager.instance.ready_p2)
        {            
            GameManager.instance.PauseUnPause();
        }
    }
}
