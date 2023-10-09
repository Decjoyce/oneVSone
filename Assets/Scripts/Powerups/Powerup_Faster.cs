using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Faster : MonoBehaviour
{
    float timer = 5f;

    playermovement pMove;

    private void Start()
    {
        //StartCoroutine(GetRidOfPower());
        IncreaseMovementSpeed();
    }

    void IncreaseMovementSpeed()
    {
        pMove = GetComponent<playermovement>();
        pMove.moveSpeed = 20f;
    }

    private void Update()
    {
        if (GameManager.instance.roundOver)
            Destroy(this);
    }

    IEnumerator GetRidOfPower()
    {
        yield return new WaitForSeconds(timer);
        pMove.moveSpeed = pMove.ogSpeed;
        Destroy(this);
    }
}
