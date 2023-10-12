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

    public void PlayerHit()
    {
        switch (powerupType)
        {
            case "JUGG":
                powerupType = null;
                break;
            case "Invincible":
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

}
