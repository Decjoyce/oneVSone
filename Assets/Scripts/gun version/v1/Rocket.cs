using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    byte playerBullet;

    [SerializeField]
    float kaboomTimer = 4f;

    [SerializeField]
    GameObject kaboomExplosion;

    int bounces = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bounces++;
        if (!GameManager.instance.roundOver)
        {
            if (playerBullet == 0 && collision.gameObject.CompareTag("Player2"))
            {
                collision.gameObject.GetComponent<PlayerHealth>().PlayerHit();
                Instantiate(kaboomExplosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            if (playerBullet == 1 && collision.gameObject.CompareTag("Player1"))
            {
                collision.gameObject.GetComponent<PlayerHealth>().PlayerHit();
                Instantiate(kaboomExplosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

            Instantiate(kaboomExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(Kaboom());
    }

    private void Update()
    {
        if (GameManager.instance.roundOver)
            Destroy(gameObject);
    }

    IEnumerator Kaboom()
    {
        yield return new WaitForSeconds(kaboomTimer);
        Instantiate(kaboomExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
