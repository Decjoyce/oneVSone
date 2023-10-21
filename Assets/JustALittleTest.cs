using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustALittleTest : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Rotate(0, 0, 45);
        }
    }
}
