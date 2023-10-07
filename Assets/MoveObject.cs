using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MoveObject : MonoBehaviour
{
    public float Speed = 0f;
    
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(MoveObj), 3f);
    }

    private  void MoveObj()
    {
        obj1.transform.position = Vector3.MoveTowards(obj1.transform.position, obj2.transform.position, Speed);
        obj3.transform.position = Vector3.MoveTowards(obj3.transform.position, obj4.transform.position, Speed);
    }

    // Update is called once per frame
    void Update()
    {
        MoveObj();
    }

    private void FixedUpdate()
    {
    }
}
