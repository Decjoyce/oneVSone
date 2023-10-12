using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    PowerupEffect[] powerups;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            GivePowerUp(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player2"))
        {
            GivePowerUp(collision.gameObject);
            Destroy(gameObject);
        }         
    }

    private void Update()
    {
        if (GameManager.instance.roundOver)
            Destroy(gameObject);
    }

    public void GivePowerUp(GameObject player)
    {
        int powerUp = Random.Range(0, powerups.Length);
        player.GetComponent<PlayerPowerup>().AddPlayerPowerup(powerups[powerUp]);
    }

}
