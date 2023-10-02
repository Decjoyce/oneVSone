using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using System.Linq;

public class playermovement : MonoBehaviour
{
    public float moveSpeed;
    
    private Vector2 MoveDirection;
    private Vector2 inputVector = Vector2.zero;

    public Weapon weap;

    [SerializeField] 
    private int playerIndex = 0;

    [SerializeField]
    float timerAFK = 3f;
    [SerializeField]
    float timerCountdown = 3f;

    bool timerOn = false;

    [SerializeField]
    bool haveACountdown = true;

    [SerializeField]
    Animator animator;
    //bool isAfk = true;

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.gamePaused)
            ProcessInputs();
        if (GameManager.instance.roundOver)
            StopCountdown();
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    void ProcessInputs()
    {
        //float moveX = Input.GetAxisRaw("Horizontal");
        //float moveY = Input.GetAxisRaw("Vertical");

        //Move System
        MoveDirection = new Vector2(inputVector.x, inputVector.y);
        MoveDirection = transform.TransformDirection(MoveDirection);
        //MoveDirection *= moveSpeed;
        if (haveACountdown)
        {
            if (inputVector.x == 0 && inputVector.y == 0 && !GameManager.instance.roundOver)
            {
                if (!timerOn)
                    StartCoroutine(AFKCountdown());
            }
            else
            {
                StopCountdown();
            }
        }


        //UnityEngine.Debug.Log(MoveDirection);
        transform.Translate(MoveDirection * moveSpeed * Time.deltaTime);
    }
    
    public void SetInputVector(Vector2 Direction)
    {
        inputVector = Direction;
    }

    IEnumerator AFKCountdown()
    {
        timerOn = true;
        yield return new WaitForSeconds(timerAFK);
        animator.SetBool("pulse", true);       

        yield return new WaitForSeconds(timerCountdown);
        if (playerIndex == 0)
            GameManager.instance.IncreaseScore_P1();
        else if (playerIndex == 1)
            GameManager.instance.IncreaseScore_P2();
        StopCountdown();        
    }

    void StopCountdown()
    {
        StopAllCoroutines();
        animator.SetBool("pulse", false);
        timerOn = false;
    }
    
}
