using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerup : MonoBehaviour
{
    PowerupEffect powerup;
    GameObject effect;

    [SerializeField]
    byte playerNum; 

    public void AddPlayerPowerup(PowerupEffect powerupEffect)
    {
        powerup = powerupEffect;
        powerup.Apply(gameObject);
        if (playerNum == 0)
            effect = Instantiate(powerup.visualEffect_P1, transform);
        else if(playerNum == 1)
            effect = Instantiate(powerup.visualEffect_P2, transform);
    }

    public void RemovePlayerPowerup()
    {
        if (powerup != null)
        {
            powerup.Remove(gameObject);
            powerup = null;
            Destroy(effect);
            effect = null;
        }
    }

}
