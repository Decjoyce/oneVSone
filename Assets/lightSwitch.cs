using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class lightSwitch : MonoBehaviour
{
    public Light2D test;

    private void Awake()
    {
        test.enabled = true;
    }
}
