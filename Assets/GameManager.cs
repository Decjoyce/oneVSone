using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    static int score_P1;
    static int score_P2;

    public GameObject p1;
    public GameObject p2;

    public Transform spawn_p1;
    public Transform spawn_p2;

    private GameObject currentLayout;
    public GameObject[] layouts;

    public GameObject winnerUI;

    [SerializeField]
    TextMeshProUGUI winnerText;

    // Start is called before the first frame update
    void Start()
    {
        currentLayout = layouts[Random.Range(0, layouts.Length - 1)];
    }

    public void IncreaseScore_P1()
    {
        score_P1++;
        ResetRound();
        if (score_P1 == 5)
        {
            GameOver(1);
        }
    }

    public void IncreaseScore_P2()
    {
        score_P2++;
        ResetRound();
        if (score_P2 == 5)
        {
            GameOver(2);
        }
    }

    public void ResetRound()
    {
        p1.transform.position = spawn_p1.transform.position;
        p2.transform.position = spawn_p2.transform.position;
        currentLayout.SetActive(false);
        currentLayout = layouts[Random.Range(0, layouts.Length - 1)];
    }

    public void GameOver(byte winner)
    {
        winnerText.text = "Player " + winner + " Wins";
        winnerUI.SetActive(true);
    }

}
