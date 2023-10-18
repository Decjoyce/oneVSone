using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerup : MonoBehaviour
{
    PowerupEffect powerup;
    public GameObject effect;

    [SerializeField]
    byte playerNum; 

    public void AddPlayerPowerup(PowerupEffect powerupEffect)
    {
        if(powerup != null)
        {
            powerup.Remove(gameObject);
            Destroy(effect);
        }
            
        powerup = powerupEffect;
        powerup.Apply(gameObject);
        if (playerNum == 0 && powerup.visualEffect_P1 != null)
            effect = Instantiate(powerup.visualEffect_P1, transform);
        else if(playerNum == 1 && powerup.visualEffect_P2 != null)
            effect = Instantiate(powerup.visualEffect_P2, transform);
    }

    public void RemovePlayerPowerup()
    {
        if (powerup != null)
        {
            powerup.Remove(gameObject);
            powerup = null;
            if(effect != null)
                Destroy(effect);
            effect = null;
        }
    }

}
