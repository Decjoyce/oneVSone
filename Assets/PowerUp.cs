using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    string[] powerups = new string[8];

    private void Start()
    {
        InitialisePowerups();
    }

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
        GameManager.instance.poweredUp = true;
        string powerUp = powerups[Random.Range(0, powerups.Length)];
        switch (powerUp)
        {
            case "FASTER":
                player.AddComponent<Powerup_Faster>();
                break;
            case "BURST":
                player.AddComponent<Powerup_Burst>();
                break;
            case "DOUBLE":
                player.AddComponent<Powerup_Double>();
                break;
            case "JUGGERNAUT":
                player.AddComponent<Powerup_Jugg>();
                break;
            case "AOE":
                player.AddComponent<Powerup_AOE>();
                break;
            case "INVINCIBLE":
                player.AddComponent<Powerup_Invincible>();
                break;
            case "ROCKET":
                player.AddComponent<Powerup_Rocket>();
                break;
            case "TELEPORT":
                player.AddComponent<Powerup_Teleport>();
                break;
            default:
                Debug.Log("Powerup not found!");
                break;
        }
    }

    void InitialisePowerups()
    {
        powerups[0] = "BURST";
        powerups[1] = "DOUBLE";
        powerups[2] = "AOE";
        powerups[3] = "TELEPORT";
        powerups[4] = "ROCKET";
        powerups[5] = "JUGG";
        powerups[6] = "INVINCIBLE";
        powerups[7] = "FASTER";
    }

}
