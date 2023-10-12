using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WInd : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D playerBody;
    public float influenceRange;
    public float intensity;
    public float distanceToPlayer;
    private Vector2 pullForce;
    void Start()
    {
        playerBody = player.GetComponent<Rigidbody2D>();
    }
    

   
    void Update()
    {
        wind();
    }

    void wind()
    {
        distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceToPlayer <= influenceRange)
        {
            pullForce = (transform.position - player.position).normalized / distanceToPlayer * intensity;
            playerBody.AddForce(pullForce, ForceMode2D.Force);
        }
    }
}
