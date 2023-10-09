using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportShot : MonoBehaviour
{
    [SerializeField]
    byte playerBullet;

    [SerializeField]
    float tpTime = 2f;

    [SerializeField]
    GameObject trailPrefab;
    bool canTeleport = true;
    LineRenderer trailRenderer;
    private void OnCollisionEnter2D(Collision2D collision)
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
        //NewLine();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Teleporter"))
            //NewLine();
    }

    void Start()
    {
        //NewLine();
        StartCoroutine(TeleportPlayer());
    }

    IEnumerator TeleportPlayer()
    {
        yield return new WaitForSeconds(tpTime);
        if (playerBullet == 0 && !GameManager.instance.roundOver && canTeleport)
            GameManager.instance.p1.transform.position = transform.position;
        if (playerBullet == 2 && !GameManager.instance.roundOver && canTeleport)
            GameManager.instance.p2.transform.position = transform.position;
        Destroy(gameObject);
    }

    private void Update()
    {
        if (GameManager.instance.roundOver)
            Destroy(gameObject);
        if (trailRenderer != null)
        {
            trailRenderer.SetPosition(1, transform.position);
        }
    }
    void NewLine()
    {
        GameObject aoeTrail = Instantiate(trailPrefab, transform);
        trailRenderer = aoeTrail.GetComponent<LineRenderer>();
        trailRenderer.positionCount = 2;
        trailRenderer.SetPosition(0, transform.position);
    }
}
