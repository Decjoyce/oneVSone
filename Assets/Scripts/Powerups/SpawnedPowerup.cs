using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnedPowerup : MonoBehaviour
{
    [SerializeField]
    PowerupEffect[] powerups;

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
            {
                GiveSpawnedPowerup(collision.gameObject);
                Destroy(gameObject);
            }
    }

    private void Update()
    {
        if (GameManager.instance.roundOver)
                Destroy(gameObject);
    }

    public void GiveSpawnedPowerup(GameObject player)
    {
        int powerupIndex = Random.Range(0, powerups.Length);
        player.GetComponent<PlayerPowerup>().AddPlayerPowerup(powerups[powerupIndex]);
    }
}
