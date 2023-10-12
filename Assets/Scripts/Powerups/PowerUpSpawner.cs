using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    bool hasPowerup;

    [SerializeField]
    GameObject powerupPrefab;

    [SerializeField]
    float spawnTime;

    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public GameObject fakes;

    GameObject spawnedPowerup1, spawnedPowerup2;

    public void SpawnPowerupInitiator()
    {
        if(hasPowerup)
            StartCoroutine(SpawnPowerup());
    }

    public void PowerupGone()
    {
        StopAllCoroutines();
        if (fakes != null)
            fakes.SetActive(true);
        if (spawnedPowerup1 != null)
            Destroy(spawnedPowerup1);
        if (spawnedPowerup2 != null)
            Destroy(spawnedPowerup2);
    }

    IEnumerator SpawnPowerup()
    {
        yield return new WaitForSeconds(spawnTime);
        if (fakes != null)
            fakes.SetActive(false);

        spawnedPowerup1 = Instantiate(powerupPrefab, spawnPoint1.position, spawnPoint1.rotation);
        if(spawnPoint2 != null)
            spawnedPowerup2 = Instantiate(powerupPrefab, spawnPoint2.position, spawnPoint2.rotation);
    }
}
