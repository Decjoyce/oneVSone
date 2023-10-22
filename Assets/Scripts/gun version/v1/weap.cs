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
    public AudioClip reloadSound;

    //Weapon Types
    public string weaponType;

    [SerializeField]
    private GameObject bulletPrefab;

    public GameObject currentBulletPrefab;

    [SerializeField]
    private GameObject[] firePoints;

    [SerializeField]
    private Transform firePoint, doubleFirePoint;
    [SerializeField]
    private SpriteRenderer weaponArt, doubleWeaponArt;

    [SerializeField]
    private Sprite[] defaultWeaponArts;

    private Sprite[] weaponArts;

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
            AnimHandler();
            StartCoroutine(FireDelay());
            if (currentlyReloading != null)
                StopCoroutine(currentlyReloading);
            currentlyReloading = StartCoroutine(Reload());
        }
    }

    void NormalFire()
    {
        source.PlayOneShot(shot, 1f);
        GameObject bullet = Instantiate(currentBulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * currentFireForce, ForceMode2D.Impulse);
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
        GameObject bullet = Instantiate(currentBulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * currentFireForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.25f);
        source.PlayOneShot(shot, 1f);
        bullet = Instantiate(currentBulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * currentFireForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.25f);
        source.PlayOneShot(shot, 1f);
        bullet = Instantiate(currentBulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * currentFireForce, ForceMode2D.Impulse);
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
        GameObject bullet1 = Instantiate(currentBulletPrefab, firePoint.position, firePoint.rotation);
        bullet1.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * currentFireForce, ForceMode2D.Impulse);
        currentCapacity--;
        GameObject bullet2 = Instantiate(currentBulletPrefab, doubleFirePoint.position, doubleFirePoint.rotation);
        bullet2.GetComponent<Rigidbody2D>().AddForce(doubleFirePoint.up * currentFireForce, ForceMode2D.Impulse);
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
        if (firePointNum == 8)
            firePointNum = 0;
        firePoint.parent.rotation = Quaternion.Euler(new Vector3(0, 0, -45 * firePointNum));
        if (weaponType == "DOUBLE")
        {
            doubleFirePoint.parent.rotation = Quaternion.Euler(new Vector3(0, 0, -45 * firePointNum));
        }
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
        yield return new WaitForSeconds(reloadDelay);
        source.PlayOneShot(reloadSound, 1f);
        currentCapacity = capacity;
        AnimHandler();
    }

    public void ResetWeapon()
    {
        weaponType = null;
        if (GameManager.instance.roundOver)
        {
            firePoint.parent.rotation = Quaternion.Euler(new Vector3(0, 0, -45 * startingFirePointNum));
            doubleFirePoint.parent.rotation = Quaternion.Euler(new Vector3(0, 0, -45 * startingFirePointNum));
            firePointNum = startingFirePointNum;
        }
        canShoot = true;
        currentFireForce = defaultFireForce;
        fireDelay = defaultFireDelay;
        capacity = defaultCapacity;
        currentCapacity = capacity;
        reloadDelay = defaultReloadDelay;
        currentBulletPrefab = bulletPrefab;
        shot = normalShot;
        weaponArts = defaultWeaponArts;
        doubleFirePoint.transform.parent.gameObject.SetActive(false);
    }

    public void changeWeapon(float newFireDelay, float newFireForce, int newCapacity, float newReloadDelay, string newWeaponType, AudioClip newShotSound, Sprite[] newWeaponaArt)
    {
        fireDelay = newFireDelay;
        fireForce = newFireForce;
        capacity = newCapacity;
        currentCapacity = newCapacity;
        reloadDelay = newReloadDelay;
        weaponType = newWeaponType;
        shot = newShotSound;
        weaponArts = newWeaponaArt;
        if (newWeaponType == "DOUBLE")
            doubleFirePoint.transform.parent.gameObject.SetActive(true);
        AnimHandler();
    }

    public void AnimHandler()
    {
        weaponArt.sprite = weaponArts[currentCapacity];
        doubleWeaponArt.sprite = weaponArts[currentCapacity];
    }

    #region TheDump
    /*        int secondFirePoint = 0;
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
        }*/
    #endregion

}
