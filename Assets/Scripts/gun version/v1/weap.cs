using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject[] firePoints;
    public float fireForce = 20f;
    public byte firePointNum = 0;
    bool canShoot = true;

    private void Start()
    {
        AnimHandler();
    }

    public void Fire()
    {
        if (canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoints[firePointNum].transform.up * fireForce, ForceMode2D.Impulse);
            firePointNum++;
            if (firePointNum == firePoints.Length)
                firePointNum = 0;
            StartCoroutine(FireDelay());
            AnimHandler();
        }
    }
    IEnumerator FireDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }

    public void AnimHandler()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            if (i == firePointNum)
                firePoints[i].GetComponentInChildren<SpriteRenderer>().enabled = true;
            else
                firePoints[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

}
