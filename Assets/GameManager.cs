using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Game Manager found");
            return;
        }
        instance = this;
    }
    #endregion

    #region Functionality
    [Header("Game Functionality")]
    public bool gamePaused = false;
    public bool gameOver = false;

    int score_P1;
    int score_P2;

    public GameObject p1;
    public GameObject p2;

    public Transform spawn_p1;
    public Transform spawn_p2;

    private GameObject currentLayout;
    public GameObject[] layouts;

    #endregion

    #region UI
    [Header("UI")]
    [SerializeField]
    private GameObject winnerUI;
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private GameObject gameplayUI;

    [SerializeField]
    TextMeshProUGUI winnerText, scoreWinningText, scoreText;

    [SerializeField]
    private GameObject pauseButton, gameOverButton;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentLayout = layouts[Random.Range(0, layouts.Length)];
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseUnPause();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            IncreaseScore_P1();
        }
        scoreText.text = score_P1 + " - " + score_P2;
    }

    public void PauseUnPause()
    {
        if (!pauseUI.activeSelf)
        {
            gamePaused = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0.0f;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseButton);
        }
        else
        {
            gamePaused = false;
            pauseUI.SetActive(false);
            Time.timeScale = 1f;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void IncreaseScore_P1()
    {
        if(!gameOver)
            score_P1++;

        if (score_P1 == 5)
        {
            StartCoroutine(GameOver(2));
        }
        else
            ResetRound();
    }

    public void IncreaseScore_P2()
    {
        if (!gameOver)
            score_P2++;

        if (score_P2 == 5)
        {
            StartCoroutine(GameOver(2));
        }
        else
            ResetRound();
    }

    public void ResetRound()
    {
        p1.transform.position = spawn_p1.transform.position;
        p2.transform.position = spawn_p2.transform.position;
        currentLayout.SetActive(false);
        currentLayout = layouts[Random.Range(0, layouts.Length)];
    }

    public IEnumerator GameOver(byte winner)
    {        
        gameOver = true;
        Time.timeScale = 0.5f;

        yield return new WaitForSecondsRealtime(2);

        Time.timeScale = 0f;
        gamePaused = true;

        if(winner == 1)
        {
            winnerText.color = new Color(0, 0, 255);
            scoreWinningText.color = new Color(0, 0, 255);
        }
        if(winner == 2)
        {
            winnerText.color = new Color(255, 0, 0);
            scoreWinningText.color = new Color(255, 0, 0);
        }
        winnerText.text = "Player " + winner + " Wins";
        scoreWinningText.text = score_P1 + " - " + score_P2;

        gameplayUI.SetActive(false);
        pauseUI.SetActive(false);
        winnerUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameOverButton);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
