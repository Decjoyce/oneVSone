using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    //Audio
    public AudioSource source;
 
    public AudioClip normalShot;
    public AudioClip shot;
    public AudioClip shotTick;

    //Weapon Types
    public string weaponType;

    [SerializeField]
    private GameObject bulletPrefab;

    public GameObject currentBulletPrefab;

    [SerializeField]
    private GameObject[] firePoints;

    public float fireForce = 20f;
    public float defaultFireForce = 20f;
    public float currentFireForce;

    public float fireDelay = 1.5f;
    public float defaultFireDelay = 1.5f;

    [SerializeField]
    private int defaultCapacity;

    int currentCapacity;
    public int capacity;

    [SerializeField]
    private float defaultReloadDelay;
    public float reloadDelay;

    Coroutine currentlyReloading;

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
        ResetWeapon();
        AnimHandler();
    }

    public void Fire()
    {
        if (canShoot && currentCapacity > 0)
        {          
            switch (weaponType)
            {
                case "BURST":
                    StartCoroutine(BurstFire());
                    break;
                case "DOUBLE":
                    DoubleFire();
                    AnimHandler();
                    break;
                case "AOE":
                    NormalFire();
                    break;
                case "TELEPORT":
                    NormalFire();
                    break;
                case "ROCKET":
                    NormalFire();
                    break;
                default:
                    fireForce = defaultFireForce;
                    NormalFire();
                    break;
            }
            StartCoroutine(FireDelay());
            if (currentlyReloading != null)
                StopCoroutine(currentlyReloading);
            currentlyReloading = StartCoroutine(Reload());
        }
    }

    void NormalFire()
    {
        source.PlayOneShot(shot, 1f);
        GameObject bullet = Instantiate(currentBulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoints[firePointNum].transform.up * currentFireForce, ForceMode2D.Impulse);
        currentCapacity--;
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
        source.PlayOneShot(shot, 1f);
        GameObject bullet = Instantiate(currentBulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoints[firePointNum].transform.up * currentFireForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.25f);
        source.PlayOneShot(shot, 1f);
        bullet = Instantiate(currentBulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoints[firePointNum].transform.up * currentFireForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.25f);
        source.PlayOneShot(shot, 1f);
        bullet = Instantiate(currentBulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoints[firePointNum].transform.up * currentFireForce, ForceMode2D.Impulse);
        currentCapacity--;
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
        source.PlayOneShot(shot, 1f);
        GameObject bullet1 = Instantiate(currentBulletPrefab, firePoints[firePointNum].transform.position, firePoints[firePointNum].transform.rotation);
        bullet1.GetComponent<Rigidbody2D>().AddForce(firePoints[firePointNum].transform.up * currentFireForce, ForceMode2D.Impulse);
        currentCapacity--;
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

    IEnumerator Reload()
    {
        while(currentCapacity < capacity)
        {
            yield return new WaitForSeconds(reloadDelay);
            currentCapacity++;
        }
    }

    public void ResetWeapon()
    {
        weaponType = null;
        if(GameManager.instance.roundOver)
            firePointNum = startingFirePointNum;
        canShoot = true;
        currentFireForce = defaultFireForce;
        fireDelay = defaultFireDelay;
        capacity = defaultCapacity;
        currentCapacity = capacity;
        reloadDelay = defaultReloadDelay;
        currentBulletPrefab = bulletPrefab;
        shot = normalShot;
    }

    public void changeWeapon(float newFireDelay, float newFireForce, int newCapacity, float newReloadDelay, string newWeaponType, AudioClip newShotSound)
    {
        fireDelay = newFireDelay;
        fireForce = newFireForce;
        capacity = newCapacity;
        currentCapacity = capacity;
        reloadDelay = newReloadDelay;
        weaponType = newWeaponType;
        shot = newShotSound;
        AnimHandler();
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
            else if(i == secondFirePoint && weaponType == "DOUBLE")
                firePoints[i].GetComponentInChildren<SpriteRenderer>().enabled = true;
            else
                firePoints[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

}
