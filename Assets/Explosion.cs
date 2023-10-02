using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    byte playerBullet;

    [SerializeField]
    float explosionTime = 4f, explosionGrowth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.instance.roundOver)
        {
            if (playerBullet == 0 && collision.gameObject.CompareTag("Player2"))
            {
                GameManager.instance.IncreaseScore_P1();
            }

            if (playerBullet == 1 && collision.gameObject.CompareTag("Player1"))
            {
                GameManager.instance.IncreaseScore_P2();
            }
        }
    }

    private void Start()
    {
        Destroy(gameObject, explosionTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale *= explosionGrowth;
    }

}
