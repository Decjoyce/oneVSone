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

    [SerializeField]
    public GameObject bulletPrefab;

    public GameObject currentBulletPrefab;

    [SerializeField]
    private GameObject[] firePoints;

    public float fireForce = 20f;
    public float defaultFireForce = 20f;
    public float currentFireForce;

    public float fireDelay = 1.5f;
    public float defaultFireDelay = 1.5f;

    [SerializeField]
    private float switchDelay = 2f;
    private byte firePointNum = 0;
    [SerializeField]
    byte startingFirePointNum;

    bool canShoot = true;

    [SerializeField]
    bool alt_fire;

    private void Start()
    {
        AnimHandler();
        ResetWeapon();
    }

    public void Fire()
    {
        if (canShoot)
        {
            source.PlayOneShot(shot, 1f);
            
            switch (weaponType)
            {
                case "BURST":
                    StartCoroutine(BurstFire());
                    StartCoroutine(FireDelay());
                    break;
                case "DOUBLE":
                    DoubleFire();
                    StartCoroutine(FireDelay());
                    break;
                case "AOE":
                    NormalFire();
                    StartCoroutine(FireDelay());
                    break;
                case "TELEPORT":
                    NormalFire();
                    StartCoroutine(FireDelay());
                    break;
                case "ROCKET":
                    NormalFire();
                    StartCoroutine(FireDelay());
                    break;
                default:
                    fireForce = defaultFireForce;
                    NormalFire();
                    StartCoroutine(FireDelay());
                    break;
            }
            
        }
    }

    void NormalFire()
    {
        GameObject bullet = Instantiate(currentBulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
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
        GameObject bullet = Instantiate(currentBulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoints[firePointNum].transform.up * currentFireForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        bullet = Instantiate(currentBulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoints[firePointNum].transform.up * currentFireForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        bullet = Instantiate(currentBulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
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
        GameObject bullet1 = Instantiate(currentBulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
        bullet1.GetComponent<Rigidbody2D>().AddForce(firePoints[firePointNum].transform.up * currentFireForce, ForceMode2D.Impulse);
        int secondFirePoint = 0;
        if (firePointNum < 4)
            secondFirePoint = firePointNum + 4;
        else
            secondFirePoint = firePointNum - 4;

        GameObject bullet2 = Instantiate(currentBulletPrefab, firePoints[secondFirePoint].transform.position, firePoints[firePointNum].transform.rotation);
        bullet2.GetComponent<Rigidbody2D>().AddForce(firePoints[secondFirePoint].transform.up * currentFireForce, ForceMode2D.Impulse);
    }

    public void RoundHandler()
    {
        StopAllCoroutines();
        ResetWeapon();
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

    IEnumerator FireDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireDelay);
        canShoot = true;
    }

    public void ResetWeapon()
    {
        weaponType = null;
        firePointNum = startingFirePointNum;
        canShoot = true;
        currentFireForce = defaultFireForce;
        fireDelay = defaultFireDelay;
        currentBulletPrefab = bulletPrefab;
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
