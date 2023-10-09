using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Teleport : MonoBehaviour
{
    float timer = 5f;

    void AddWeap()
    {
        Weapon weap = GetComponent<Weapon>();
        weap.weaponType = "Teleport";
    }

    void RemoveWeap()
    {
        Weapon weap = GetComponent<Weapon>();
        weap.weaponType = null;
    }

    private void Start()
    {
        //StartCoroutine(GetRidOfPower());
        AddWeap();
    }

    private void Update()
    {
        if (GameManager.instance.roundOver)
            Destroy(this);
    }

    IEnumerator GetRidOfPower()
    {
        yield return new WaitForSeconds(timer);
        RemoveWeap();
        Destroy(this);
    }
}
