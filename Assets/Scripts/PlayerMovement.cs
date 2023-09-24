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

    private CharacterController controller;

    [SerializeField] 
    private int playerIndex = 0;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
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
        MoveDirection *= moveSpeed;

        controller.Move(MoveDirection * Time.deltaTime);
    }
    
    public void SetInputVector(Vector2 Direction)
    {
        inputVector = Direction;
    }
    
   

    
}
