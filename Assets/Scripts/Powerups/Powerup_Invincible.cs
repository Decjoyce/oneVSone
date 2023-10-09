using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Invincible : MonoBehaviour
{
    float timer = 10f;

    private void Start()
    {
        StartCoroutine(GetRidOfPower());
    }

    private void Update()
    {
        if (GameManager.instance.roundOver)
            Destroy(this);
    }

    IEnumerator GetRidOfPower()
    {
        yield return new WaitForSeconds(timer);
        Destroy(this);
    }
}
