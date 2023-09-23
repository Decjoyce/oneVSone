using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class playermovement : MonoBehaviour
{

    public float moveSpeed;

    public Rigidbody2D rb;

    private Vector2 MoveDirection;

    private Vector2 inputVector = Vector2.zero;
   
    
    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        MoveDirection = new Vector2(moveX, moveY);
    }
    void Move()
    {
        rb.velocity = new Vector2(MoveDirection.x  * moveSpeed, MoveDirection.y * moveSpeed);
    }

    public void SetInputVector(Vector2 Direction)
    {
        inputVector = Direction;
    }
}
