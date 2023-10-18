using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnPlayerDie, OnPlayerHit;

    public string powerupType;

    [SerializeField]
    byte playerNum;

    [SerializeField]
    GameObject playerDeathEffect;

    [SerializeField]
    PlayerPowerup playerPowerup;

    public void PlayerHit()
    {
        switch (powerupType)
        {
            case "JUGG":
                if (playerPowerup.effect != null)
                    Destroy(playerPowerup.effect);
                powerupType = null;
                break;
            case "INVINCIBLE":
                break;
            default:
                PlayerDie();
                break;
        }
        OnPlayerHit.Invoke();
    }

    void PlayerDie()
    {
        OnPlayerDie.Invoke();
        StartCoroutine(PlayDeathEffect());
        if (playerNum == 0)
            GameManager.instance.IncreaseScore_P2();
        else if (playerNum == 1)
            GameManager.instance.IncreaseScore_P1();
    }
    
    public IEnumerator PlayDeathEffect()
    {
        playerDeathEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        playerDeathEffect.SetActive(false);
    }

    public void StartDisableInvinvincible()
    {
        StartCoroutine(InvincibleDisable());
    }

    IEnumerator InvincibleDisable()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<PlayerPowerup>().RemovePlayerPowerup();
    }

}
