using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int score_P1;
    int score_P2

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore_P1()
    {
        score_P1++;
        if (score_P1 == 5)
        {

        }
    }

    public void IncreaseScore_P2()
    {
        score_P2++;
    }

}
