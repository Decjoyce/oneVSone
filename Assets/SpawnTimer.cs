using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    [SerializeField]
    float timer;

    [SerializeField]
    bool waitUntilStart = true;

    [SerializeField]
    private GameObject objectToEnable;

    // Start is called before the first frame update
    void Start()
    {
        if (!waitUntilStart)
            Destroy(gameObject, timer);
    }

    private void Update()
    {
        if (waitUntilStart && GameManager.instance.gameStarted && !GameManager.instance.roundOver)
        {
            if (!objectToEnable.activeSelf)
                StartCoroutine(EnableObject());
        }


        if (GameManager.instance.gameStarted && GameManager.instance.roundOver && objectToEnable.activeSelf)
        {
            objectToEnable.SetActive(false);
        }
    }

    IEnumerator EnableObject()
    {
        yield return new WaitForSeconds(timer);
        objectToEnable.gameObject.SetActive(true);
    }
}
