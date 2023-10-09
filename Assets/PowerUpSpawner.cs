using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject powerupPrefab;

    [SerializeField]
    float spawnTime;

    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public GameObject fakes;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPowerup());
    }

    IEnumerator SpawnPowerup()
    {
        yield return new WaitForSeconds(spawnTime);
        if (fakes != null)
            Destroy(fakes);

        Instantiate(powerupPrefab, spawnPoint1.position, spawnPoint1.rotation);
        if(spawnPoint2 != null)
            Instantiate(powerupPrefab, spawnPoint2.position, spawnPoint2.rotation);
    }
}
