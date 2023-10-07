using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioSource source;
    public AudioClip riochet;
    
    [SerializeField]
    byte playerBullet;    
    
    [SerializeField]
    bool hasTrail;

    [SerializeField]
    GameObject trailPrefab;

    LineRenderer trailRenderer;

    int bounce = 0;

    //When ever this object collides with something bounce is incremented.
    //It also checks if the round is over and if it has a trail or not.
    //If the round isn't over then it checks which player it is for and if the object it's colliding with has the tag Player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bounce++;
        source.PlayOneShot(riochet, 0.1f);
        if (!GameManager.instance.roundOver)
        {
            switch (playerBullet)
            {
                case 0:
                    if (collision.gameObject.CompareTag("Player2"))
                    {
                        GameManager.instance.IncreaseScore_P1();
                        Destroy(gameObject);
                    }
                    break;
                case 1:
                    if (collision.gameObject.CompareTag("Player1"))
                    {
                        GameManager.instance.IncreaseScore_P2();
                        Destroy(gameObject);
                    }
                    break;
            }
        }
        if (hasTrail)
        {
            //NewLineTest();
            NewLine();
        }
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

        //Sets the trails furthest point to the transform of the bullet
        if (trailRenderer != null && hasTrail)
        {
            trailRenderer.SetPosition(1, transform.position);
            //trailRenderer.SetPosition(bounce + 1, transform.position);
        }
    }

    #region Trail_v1
    void NewLine()
    {
        //if (trailRenderer != null)
            //estroy(trailRenderer.gameObject);
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
    #endregion

    #region Trail_v2
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
    #endregion
}
