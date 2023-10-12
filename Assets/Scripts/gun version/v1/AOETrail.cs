using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETrail : MonoBehaviour
{
    [SerializeField]
    byte playerBullet;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.instance.roundOver)
        {
            if (playerBullet == 0 && collision.gameObject.CompareTag("Player2"))
            {
                GameManager.instance.IncreaseScore_P1();
                Destroy(gameObject);
            }

            if (playerBullet == 1 && collision.gameObject.CompareTag("Player1"))
            {
                GameManager.instance.IncreaseScore_P2();
                Destroy(gameObject);
            }
        }
    }
}
