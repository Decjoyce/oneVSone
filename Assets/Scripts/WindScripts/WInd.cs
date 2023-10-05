using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WInd : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    private Rigidbody2D playerBody1;
    private Rigidbody2D playerBody2;
    public float influenceRange;
    public float intensity;
    public float distanceToPlayer;
    private Vector2 pullForce;
    void Start()
    {
        playerBody1 = player1.GetComponent<Rigidbody2D>();
        playerBody2 = player2.GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        distanceToPlayer = Vector2.Distance(player1.position, transform.position);
        distanceToPlayer = Vector2.Distance(player2.position, transform.position);
        if (distanceToPlayer <= influenceRange)
        {
            pullForce = (transform.position - player1.position).normalized / distanceToPlayer * intensity;
            playerBody1.AddForce(pullForce, ForceMode2D.Force);
            
            pullForce = (transform.position - player2.position).normalized / distanceToPlayer * intensity;
            playerBody2.AddForce(pullForce, ForceMode2D.Force);
        }
    }
}
