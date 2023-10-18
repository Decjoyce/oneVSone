using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    PowerupEffect[] powerups;

    [SerializeField]
    RawImage powerupUI1, powerupUI2;

    [SerializeField]
    bool spawning;

    private PowerupEffect powerup1, powerup2;

    float countdownTime = 5f;

    IEnumerator RandomisePowerup(bool both)
    {
        powerupUI1.enabled = true;
        powerupUI2.enabled = true;
        int index = Random.Range(0, powerups.Length);
        int index1 = Random.Range(0, powerups.Length);
        int index2 = Random.Range(0, powerups.Length);
        Debug.Log(both);
        if (both)
        {
            while (countdownTime > 0)
            {
                powerup1 = powerups[index++ % powerups.Length];
                powerup2 = powerup1;
                powerupUI1.texture = powerup1.powerupArt;
                powerupUI2.texture = powerup1.powerupArt;
                Debug.Log(powerup1.name);
                yield return new WaitForSecondsRealtime(0.5f);
                countdownTime--;
            }
        }
        else
        {
            while (countdownTime > 0)
            {
                powerup1 = powerups[index1++ % powerups.Length];
                powerup2 = powerups[index2++ % powerups.Length];
                powerupUI1.texture = powerup1.powerupArt;
                powerupUI2.texture = powerup2.powerupArt;
                //Debug.Log(powerup1.name);
                yield return new WaitForSecondsRealtime(0.5f);
                countdownTime--;
            }
        }
        countdownTime = 5f;
    }

    public void SelectPowerup(string type)
    {
        switch (type)
        {
            case "Old School":
                break;            
            case "Powerless":
                break;
            case "Chaos":
                StartCoroutine(RandomisePowerup(false));
                break;
            case "Default":
                StartCoroutine(RandomisePowerup(true));
                break;
            default:
                StartCoroutine(RandomisePowerup(true));
                break;
        }
        
    }

    public void GivePlayersPowerup(GameObject p1, GameObject p2)
    {
        powerupUI1.enabled = false;
        powerupUI2.enabled = false;
        p1.GetComponent<PlayerPowerup>().AddPlayerPowerup(powerup1);
        p2.GetComponent<PlayerPowerup>().AddPlayerPowerup(powerup2);
    }

}
