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
    private Weapon weap;
    private PlayerInput playerInput;
    

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var movers = FindObjectsOfType<playermovement>();
        var index = playerInput.playerIndex;
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        print(index);
        if(!playerInput.enabled)
            playerInput.enabled = true;
        weap = mover.weap;
    }

    public void OnMove(CallbackContext context)
    {

        if (!GameManager.instance.gamePaused)
            mover.SetInputVector(context.ReadValue<Vector2>());
        else
            mover.SetInputVector(Vector2.zero);
    }

    //dec
    public void Shoot(CallbackContext ctx)
    {
        if (!GameManager.instance.gamePaused && ctx.performed)
        {
            weap.Fire();
        }
    //if (!GameManager.instance.gameStarted && ctx.performed)
           // GameManager.instance.ChangeGameMode();
    }

    public void ReadyUp(CallbackContext ctx)
    {
        if (mover.GetPlayerIndex() == 0 && !GameManager.instance.gameStarted)
            GameManager.instance.ready_p1 = !GameManager.instance.ready_p1;
        if (mover.GetPlayerIndex() == 1 && !GameManager.instance.gameStarted)
            GameManager.instance.ready_p2 = !GameManager.instance.ready_p2;
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
        if (GameManager.instance.ready_p1 || GameManager.instance.ready_p2)
        {            
            GameManager.instance.PauseUnPause();
        }
        else
        {
            if(ctx.performed)
                GameManager.instance.LightSwitch();
        }
    }

    public void MenuUp(CallbackContext ctx)
    {
        if (!GameManager.instance.gameStarted && ctx.performed)
            GameManager.instance.ChangeGameMode(true);
    }
    public void MenuDown(CallbackContext ctx)
    {
        if (!GameManager.instance.gameStarted && ctx.performed)
            GameManager.instance.ChangeGameMode(false);
    }
    public void MenuRight(CallbackContext ctx)
    {
        if (!GameManager.instance.gameStarted && ctx.performed)
            GameManager.instance.ChangeMap(true);
    }
    public void MenuLeft(CallbackContext ctx)
    {
        if (!GameManager.instance.gameStarted && ctx.performed)
            GameManager.instance.ChangeMap(false);
    }
}
