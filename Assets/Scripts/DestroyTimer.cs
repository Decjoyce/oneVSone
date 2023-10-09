using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField]
    float timer;

    [SerializeField]
    bool waitUntilStart = true;
    // Start is called before the first frame update
    void Start()
    {
        if(!waitUntilStart)
            Destroy(gameObject, timer);
    }

    private void Update()
    {
        if(waitUntilStart && GameManager.instance.gameStarted && !GameManager.instance.roundOver)
            Destroy(gameObject, timer);
    }
}
