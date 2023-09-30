using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField]
    float degreesPerSecond;
    [SerializeField]
    bool anitClockwise;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.instance.roundOver)
            Rotating();
        else
            transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    void Rotating()
    {
        if(anitClockwise)
            transform.Rotate(new Vector3(0, 0, degreesPerSecond * Time.fixedDeltaTime));
        else
            transform.Rotate(new Vector3(0, 0, -degreesPerSecond * Time.fixedDeltaTime));
    }

}
