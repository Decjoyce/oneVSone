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
    bool haveACountdown_A = true, haveACountdown_B = false, countdownDebuff = false;

    [SerializeField]
    Animator animator;
    //bool isAfk = true;

    Vector3 lastPos;
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

        if (!GameManager.instance.gamePaused && haveACountdown_A)
        {
            if(timerAFK <= 0f)
            {
                if (Vector3.Distance(transform.position, lastPos) <= 2)
                {
                    if (!timerOn)
                        StartCoroutine(AFKCountdown_A());
                }
                lastPos = transform.position;
                timerAFK = 1f;
            }
            if (Vector3.Distance(transform.position, lastPos) > 2)
            {
                StopCountdown();
                weap.fireForce = 20f;
            }
            timerAFK -= Time.deltaTime;
        }

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
        if (haveACountdown_B)
        {
            if (inputVector.x == 0 && inputVector.y == 0 && !GameManager.instance.roundOver)
            {
                if (!timerOn)
                    StartCoroutine(AFKCountdown_B());
            }
            else
            {
                StopCountdown();
                weap.fireForce = 20f;
            }
        }


        //UnityEngine.Debug.Log(MoveDirection);
        transform.Translate(MoveDirection * moveSpeed * Time.deltaTime);
    }
    
    public void SetInputVector(Vector2 Direction)
    {
        inputVector = Direction;
    }

    IEnumerator AFKCountdown_A()
    {
        timerOn = true;
        yield return new WaitForSeconds(2f);
        animator.SetBool("pulse", true);
        weap.fireForce = 5f;

        yield return new WaitForSeconds(timerCountdown);
        if (!countdownDebuff)
        {
            if (playerIndex == 0)
                GameManager.instance.IncreaseScore_P2();
            else if (playerIndex == 1)
                GameManager.instance.IncreaseScore_P1();
            StopCountdown();
        }
        else
        {
            weap.fireForce = 2f;
        }       
    }

    IEnumerator AFKCountdown_B()
    {
        timerOn = true;

        yield return new WaitForSeconds(timerAFK);

        animator.SetBool("pulse", true);
        weap.fireForce = 5f;

        yield return new WaitForSeconds(timerCountdown);
        if (!countdownDebuff)
        {
            if (playerIndex == 0)
                GameManager.instance.IncreaseScore_P2();
            else if (playerIndex == 1)
                GameManager.instance.IncreaseScore_P1();
            StopCountdown();
        }
        else
        {
            weap.fireForce = 2f;
        }
        
    }

    void StopCountdown()
    {
        StopAllCoroutines();
        animator.SetBool("pulse", false);
        timerOn = false;
    }
    
}
