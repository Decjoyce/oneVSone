using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind1 : MonoBehaviour
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
        Wind();
    }

    void Wind()
    {
        if (!GameManager.instance.roundOver && GameManager.instance.gameStarted)
        {
            distanceToPlayer = Vector2.Distance(player.position, transform.position);
            if (distanceToPlayer <= influenceRange)
            {
                pullForce = (transform.position - player.position).normalized / distanceToPlayer * intensity;
                playerBody.AddForce(pullForce, ForceMode2D.Force);
            }
            else
            {
                pullForce = Vector2.zero;
                playerBody.AddForce(pullForce, ForceMode2D.Force);

            }
        }

    }
}
