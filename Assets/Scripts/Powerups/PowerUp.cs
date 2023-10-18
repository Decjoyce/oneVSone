using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    PowerupEffect[] powerups;

    [SerializeField]
    RawImage powerupUI;

    [SerializeField]
    bool spawning;

    PowerupEffect powerUp;

    float countdownTime = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spawning)
        {
            if (collision.CompareTag("Player1"))
            {
                GiveSpawnedPowerup(collision.gameObject);
                Destroy(gameObject);
            }
            if (collision.CompareTag("Player2"))
            {
                GiveSpawnedPowerup(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        if (spawning)
        {
            if (GameManager.instance.roundOver)
                Destroy(gameObject);
        }
    }

    public void GiveSpawnedPowerup(GameObject player)
    {
        int powerUp = Random.Range(0, powerups.Length);
        player.GetComponent<PlayerPowerup>().AddPlayerPowerup(powerups[powerUp]);
    }

    IEnumerator RandomisePowerup()
    {
        powerupUI.enabled = true;
        int index = Random.Range(0, powerups.Length);
        while (countdownTime > 0)
        {
            powerUp = powerups[index++ % powerups.Length];
            powerupUI.texture = powerUp.powerupArt;
            Debug.Log(powerUp.name);
            yield return new WaitForSecondsRealtime(0.5f);
            countdownTime--;
        }
        countdownTime = 5f;
    }

    public void SelectPowerup()
    {
        StartCoroutine(RandomisePowerup());
    }

    public void GivePlayersPowerup(GameObject p1, GameObject p2)
    {
        powerupUI.enabled = false;
        p1.GetComponent<PlayerPowerup>().AddPlayerPowerup(powerUp);
        p2.GetComponent<PlayerPowerup>().AddPlayerPowerup(powerUp);
    }

}
