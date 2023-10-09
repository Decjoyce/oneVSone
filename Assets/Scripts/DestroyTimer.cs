using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField]
    float timer;

    [SerializeField]
    bool waitUntilStart = true;

    [SerializeField]
    bool disable = true;

    [SerializeField]
     private GameObject objectToDisable;

    // Start is called before the first frame update
    void Start()
    {
        if(!waitUntilStart)
            Destroy(gameObject, timer);
    }

    private void Update()
    {
        if(waitUntilStart && GameManager.instance.gameStarted && !GameManager.instance.roundOver)
        {
            if (!disable)
                Destroy(gameObject, timer);
            else if(objectToDisable.activeSelf)
                StartCoroutine(DisableObject());
        }
            

        if(GameManager.instance.gameStarted && GameManager.instance.roundOver && !objectToDisable.activeSelf)
        {
            objectToDisable.SetActive(true);
        }
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(timer);
        objectToDisable.gameObject.SetActive(false);
    }
}
