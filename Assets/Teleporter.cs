using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    Transform sendTo;

    [SerializeField]
    bool invert;

    [SerializeField]
    bool xAxis;

    [SerializeField]
    bool bulletOnly;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!bulletOnly)
        {
            if (collision.CompareTag("Bullet") || collision.CompareTag("Player1") || collision.CompareTag("Player2"))
            {
                if (xAxis)
                {
                    collision.transform.position = new Vector2(sendTo.position.x, collision.transform.position.y);
                }
                else
                {
                    collision.transform.position = new Vector2(collision.transform.position.x, sendTo.position.y);
                }

            }
        }
        else
        {
            if (collision.CompareTag("Bullet"))
            {
                if (xAxis)
                {
                    collision.transform.position = new Vector2(sendTo.position.x, collision.transform.position.y);
                }
                else
                {
                    collision.transform.position = new Vector2(collision.transform.position.x, sendTo.position.y);
                }

            }
        }

    }

}