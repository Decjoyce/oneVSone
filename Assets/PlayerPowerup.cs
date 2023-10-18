using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerup : MonoBehaviour
{
    PowerupEffect powerup;
    public SpriteRenderer effect;

    public void AddPlayerPowerup(PowerupEffect powerupEffect)
    {
        if(powerup != null)
        {
            powerup.Remove(gameObject);
        }
            
        powerup = powerupEffect;
        powerup.Apply(gameObject);
        effect.enabled = true;
        effect.sprite = powerup.visualEffect;
    }

    public void RemovePlayerPowerup()
    {
        if (powerup != null)
        {
            powerup.Remove(gameObject);
            powerup = null;

            effect.enabled = false;
        }
    }

}
