using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_AOE : MonoBehaviour
{
    float timer = 5f;

    void AddWeap()
    {
        Weapon weap = GetComponent<Weapon>();
        weap.weaponType = "AOE";
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

    IEnumerator GetRidOfPower()
    {
        yield return new WaitForSeconds(timer);
        RemoveWeap();
        Destroy(this);
    }
}
