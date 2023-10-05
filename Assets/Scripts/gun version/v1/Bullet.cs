using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public AudioClip death;
    [SerializeField]
    byte playerBullet;    
    
    [SerializeField]
    bool hasTrail;

    [SerializeField]
    GameObject trailPrefab;

    LineRenderer trailRenderer;

    int bounce = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bounce++;
        if (!GameManager.instance.roundOver)
        {
            source.PlayOneShot(clip);
            if (playerBullet == 0 && collision.gameObject.CompareTag("Player2"))
            {
                GameManager.instance.IncreaseScore_P1();
                source.PlayOneShot(death);
                Destroy(gameObject);
            }

            if (playerBullet == 1 && collision.gameObject.CompareTag("Player1"))
            {
                GameManager.instance.IncreaseScore_P2();
                source.PlayOneShot(death);
                Destroy(gameObject);    
            }
        }
        if (hasTrail)
        {
            //NewLineTest();
            NewLine();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter") && hasTrail)
        {
            NewLine();
            Destroy(trailRenderer.gameObject);           
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter") && hasTrail)
            NewLine();
    }

    void Start()
    {
        if(hasTrail)
            NewLine();
        //StartNewLine();
        Destroy(gameObject, 4f);
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
    void NewLine()
    {
        if (trailRenderer != null)
            Destroy(trailRenderer.gameObject);
        GameObject aoeTrail = Instantiate(trailPrefab, transform);
        trailRenderer = aoeTrail.GetComponent<LineRenderer>();
        trailRenderer.positionCount = 2;
        trailRenderer.SetPosition(0, transform.position);
    }
    void NewLineTest()
    {
        trailRenderer.positionCount = bounce + 2;
        trailRenderer.SetPosition(bounce, transform.position);
    }
    void StartNewLine()
    {
        GameObject aoeTrail = Instantiate(trailPrefab, transform);
        trailRenderer = aoeTrail.GetComponent<LineRenderer>();
        trailRenderer.positionCount = 2;
        trailRenderer.SetPosition(0, transform.position);
    }
}
