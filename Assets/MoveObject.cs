using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MoveObject : MonoBehaviour
{
    public float Speed = 0f;
    
    public GameObject Barrier;
    public GameObject P1;
    public GameObject Barrier_;
    public GameObject P2;

    private bool Isinvoked = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Isinvoked = true;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void InvokedFun()
    {
        if (Isinvoked == true)
        {
            Invoke(nameof(MoveObj), 5f);
        }
    }

    private void MoveObj()
    {
        if (GameManager.instance.gameStarted && !GameManager.instance.roundOver)
        {
            if (Vector3.Distance(Barrier.transform.position, P1.transform.position) >= 3f) 
            {
                Barrier.transform.position = Vector3.MoveTowards(Barrier.transform.position, P1.transform.position, Speed);
                Barrier_.transform.position = Vector3.MoveTowards(Barrier_.transform.position, P2.transform.position, Speed);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        InvokedFun();
    }
}
