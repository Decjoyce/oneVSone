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

    [SerializeField]
    bool hasTrail;

    [SerializeField]
    GameObject trailPrefab;

    LineRenderer trailRenderer;

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
        if(bounces >= 2)
        {
            Instantiate(kaboomExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (hasTrail)
        {
            //NewLineTest();
            NewLine();
        }

    }

    void Start()
    {
        StartCoroutine(Kaboom());
        if (hasTrail)
            NewLine();
    }

    private void Update()
    {
        if (GameManager.instance.roundOver)
            Destroy(gameObject);

        if (trailRenderer != null && hasTrail)
        {
            trailRenderer.SetPosition(1, transform.position);
            //trailRenderer.SetPosition(bounce + 1, transform.position);
        }
    }
    
    IEnumerator Kaboom()
    {
        yield return new WaitForSeconds(kaboomTimer);
        Instantiate(kaboomExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }


    void NewLine()
    {
        if (trailRenderer != null)
            Destroy(trailRenderer.gameObject);
        GameObject aoeTrail = Instantiate(trailPrefab, transform);
        trailRenderer = aoeTrail.GetComponent<LineRenderer>();
        trailRenderer.positionCount = 2;
        trailRenderer.SetPosition(0, transform.position);
    }

    //Helps with Telleporter functionality
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter") && hasTrail)
        {
            NewLine();
            Destroy(trailRenderer.gameObject);
        }

    }
    //Helps with Telleporter functionality
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter") && hasTrail)
            NewLine();
    }

}
