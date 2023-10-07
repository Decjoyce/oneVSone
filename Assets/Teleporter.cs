using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public AudioSource source;
    public AudioClip Teleport;
    
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
                source.PlayOneShot(Teleport, 0.6f);
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
                source.PlayOneShot(Teleport, 0.6f);
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
