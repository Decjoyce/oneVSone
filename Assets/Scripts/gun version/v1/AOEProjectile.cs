using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEProjectile : MonoBehaviour
{
    [SerializeField]
    byte playerBullet;

    public AudioSource source;
    public AudioClip riochet;

    [SerializeField]
    GameObject trailPrefab, trailColliderPrefab;

    LineRenderer aoeRenderer;
    EdgeCollider2D col;

    List<Vector2> points = new List<Vector2>();
    Vector2 startPos;
    public Transform helperPoint;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        source.PlayOneShot(riochet, 0.1f);
        if (!GameManager.instance.roundOver)
        {
            if (playerBullet == 0 && collision.gameObject.CompareTag("Player2"))
            {
                collision.gameObject.GetComponent<PlayerHealth>().PlayerHit();
                Destroy(gameObject);
            }

            if (playerBullet == 1 && collision.gameObject.CompareTag("Player1"))
            {
                collision.gameObject.GetComponent<PlayerHealth>().PlayerHit();
                Destroy(gameObject);
            }
        }
        NewLine();
    }

    void Start()
    {
        points.Add(transform.position);
        points.Add(transform.position);
        NewLine();
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        if (GameManager.instance.roundOver)
            Destroy(gameObject);
        if(aoeRenderer != null)
        {
            aoeRenderer.SetPosition(1, transform.position);
        }
        helperPoint.position = startPos;
        points[1] =  helperPoint.InverseTransformPoint(transform.position);
        if(col != null)
            col.SetPoints(points);
    }

    void NewLine()
    {
        GameObject aoeTrail = Instantiate(trailPrefab, transform);
        //GameObject trailCollider = Instantiate(trailColliderPrefab, aoeTrail.transform);
        //Destroy(trailCollider, 4f);
        //Destroy(aoeTrail, 4f);
        aoeRenderer = aoeTrail.GetComponent<LineRenderer>();
        col = aoeTrail.GetComponent<EdgeCollider2D>();
        aoeRenderer.positionCount = 2;
        aoeRenderer.SetPosition(0, transform.position);
        startPos = transform.position;
        points[0] = Vector2.zero;
    }

    //Helps with Telleporter functionality
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            NewLine();
            Destroy(aoeRenderer.gameObject);
        }

    }
    //Helps with Telleporter functionality
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
            NewLine();
    }
}
