using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Jugg : MonoBehaviour
{
    public bool hasBeenShot = false;

    private void Update()
    {
        if (GameManager.instance.roundOver)
            Destroy(this);
    }


}
