using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Audio
    public AudioSource source;
    public AudioClip shot;
    public AudioClip shotTick;

    //Weapon Types
    public string weaponType;

    public GameObject bulletPrefab;
    public GameObject rocketPrefab;
    public GameObject aoePrefab;
    public GameObject teleportPrefab;

    public GameObject[] firePoints;

    public float fireForce = 20f;
    public float defaultFireForce = 20f;
    public float rocketFireForce = 10f;
    public float aoeFireForce = 10f;
    public float teleportFireForce = 25f;
    public float currentFireForce;

    public float fireDelay = 1.5f;
    public float rocketFireDelay = 1.5f;
    public float aoeFireDelay = 1.5f;
    public float teleportFireDelay = 1.5f;
    public float doubleFireDelay = 1.5f;
    public float burstFireDelay = 1.5f;

    public float switchDelay = 2f;
    public byte firePointNum = 0;
    bool canShoot = true;

    [SerializeField]
    bool alt_fire;

    private void Start()
    {
        AnimHandler();
        currentFireForce = fireForce;
    }

    public void Fire()
    {
        if (canShoot)
        {
            source.PlayOneShot(shot, 1f);
            
            switch (weaponType)
            {
                case "Burst":
                    StartCoroutine(BurstFire());
                    StartCoroutine(FireDelay(burstFireDelay));
                    break;
                case "Double":
                    DoubleFire();
                    StartCoroutine(FireDelay(doubleFireDelay));
                    break;
                case "AOE":
                    fireForce = aoeFireForce;
                    NormalFire(aoePrefab);
                    StartCoroutine(FireDelay(aoeFireDelay));
                    break;
                case "Teleport":
                    fireForce = teleportFireForce;
                    NormalFire(teleportPrefab);
                    StartCoroutine(FireDelay(teleportFireDelay));
                    break;
                case "Rocket":
                    fireForce = rocketFireForce;
                    NormalFire(rocketPrefab);
                    StartCoroutine(FireDelay(rocketFireDelay));
                    break;
                default:
                    fireForce = defaultFireForce;
                    NormalFire(bulletPrefab);
                    StartCoroutine(FireDelay(fireDelay));
                    break;
            }
            
        }
    }

    void NormalFire(GameObject prefab)
    {
        GameObject bullet = Instantiate(prefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoints[firePointNum].transform.up * currentFireForce, ForceMode2D.Impulse);
        if (alt_fire)
        {
            StopAllCoroutines();
            firePointNum++;
            if (firePointNum == firePoints.Length)
                firePointNum = 0;
            AnimHandler();
            StartCoroutine(RandomFire());
        }
    }

    IEnumerator BurstFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoints[firePointNum].transform.up * currentFireForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        bullet = Instantiate(bulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoints[firePointNum].transform.up * currentFireForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        bullet = Instantiate(bulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoints[firePointNum].transform.up * currentFireForce, ForceMode2D.Impulse);
        if (alt_fire)
        {
            StopAllCoroutines();
            firePointNum++;
            if (firePointNum == firePoints.Length)
                firePointNum = 0;
            AnimHandler();
            StartCoroutine(RandomFire());
        }
    }

    void DoubleFire()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
        bullet1.GetComponent<Rigidbody2D>().AddForce(firePoints[firePointNum].transform.up * currentFireForce, ForceMode2D.Impulse);
        int secondFirePoint = 0;
        if (firePointNum < 4)
            secondFirePoint = firePointNum + 4;
        else
            secondFirePoint = firePointNum - 4;

        GameObject bullet2 = Instantiate(bulletPrefab, firePoints[secondFirePoint].transform.position, firePoints[firePointNum].transform.rotation);
        bullet2.GetComponent<Rigidbody2D>().AddForce(firePoints[secondFirePoint].transform.up * currentFireForce, ForceMode2D.Impulse);
    }

    public void RoundHandler(byte num)
    {
        StopAllCoroutines();
        weaponType = null;
        firePointNum = num;
        canShoot = true;
        AnimHandler();
    }

    public void RandomFireInitiartor()
    {
        StartCoroutine(RandomFire());
    }

    IEnumerator RandomFire()
    {
        yield return new WaitForSeconds(switchDelay);
        source.PlayOneShot(shotTick, 0.2f);
        firePointNum++;
        if (firePointNum == firePoints.Length)
            firePointNum = 0;
        AnimHandler();
        StartCoroutine(RandomFire());
    }

    IEnumerator FireDelay(float delay)
    {
        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }

    public void AnimHandler()
    {
        int secondFirePoint = 0;
        if (firePointNum < 4)
            secondFirePoint = firePointNum + 4;
        else
            secondFirePoint = firePointNum - 4;
        for (int i = 0; i < firePoints.Length; i++)
        {
            if (i == firePointNum)
                firePoints[i].GetComponentInChildren<SpriteRenderer>().enabled = true;
            else if(i == secondFirePoint && weaponType == "Double")
                firePoints[i].GetComponentInChildren<SpriteRenderer>().enabled = true;
            else
                firePoints[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

}
