using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.iOS;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
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
    public bool roundOver = true;
    public bool gameStarted = false;
    bool waitForReady;
    public bool ready_p1 = false;
    public bool ready_p2 = false;

    int score_P1;
    int score_P2;

    public GameObject p1;
    public GameObject p2;
    public Rigidbody2D p1RB;

    public Transform spawn_p1;
    public Transform spawn_p2;

    private GameObject currentLayout;
    public GameObject[] layouts;

    public PlayerInputManager inputManager;
    #endregion

    #region UI
    [Header("UI")]
    [SerializeField]
    private GameObject winnerUI;
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private GameObject gameplayUI, countdownUI, popUpUI;

    [SerializeField]
    TextMeshProUGUI winnerText, scoreWinningText, scoreText, countdownText, popUpText;

    [SerializeField]
    private GameObject pauseButton, gameOverButton;

    private int countdownTime = 3;
    private string[] popUpsDom = new string[12];
    //private string[] popUpsClose = new string[12];

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        //StartCoroutine(StartGame());
        PopUpInitialiser();
        LayoutSetter();
    }

    private void Update()
    {
        ReadyPlayers();
        scoreText.text = score_P1 + " - " + score_P2;
    }

    public void PauseUnPause()
    {
        if (!roundOver && !gameOver && gameStarted)
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
    }


    #region Score Code
    public void IncreaseScore_P1()
    {
        if (!gameOver)
            score_P1++;

        if (score_P1 == 5)
        {
            StartCoroutine(GameOver(2));
        }
        else
            StartCoroutine(ResetRound());
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
            StartCoroutine(ResetRound());
    }
    #endregion

    #region Functionality
    void ReadyPlayers()
    {
        if (inputManager.playerCount == 2 && !gameStarted)
        {
            if (ready_p1 && ready_p2)
            {
                StartCoroutine(StartGame());
                gameStarted = true;
            }
        }
    }

    IEnumerator StartGame()
    {
        //LayoutSetter();
        countdownUI.SetActive(true);
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        countdownUI.SetActive(false);
        countdownTime = 3;
        roundOver = false;
        gamePaused = false;
    }

    public IEnumerator ResetRound()
    {
        roundOver = true;
        gamePaused = true;

        yield return new WaitForSecondsRealtime(1);

        StartCoroutine(PopUpHandler());
        LayoutSetter();

        countdownUI.SetActive(true);
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        countdownUI.SetActive(false);
        countdownTime = 3;
        roundOver = false;

    }

    public IEnumerator GameOver(byte winner)
    {
        gameOver = true;
        Time.timeScale = 0.5f;

        yield return new WaitForSecondsRealtime(1);

        Time.timeScale = 0f;
        gamePaused = true;

        if (winner == 1)
        {
            winnerText.color = new Color(0, 0, 255);
            scoreWinningText.color = new Color(0, 0, 255);
        }
        if (winner == 2)
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
    #endregion

    #region Setting Up
    void LayoutSetter()
    {
        p1.transform.position = spawn_p1.transform.position;
        p2.transform.position = spawn_p2.transform.position;
        if (currentLayout != null)
            currentLayout.SetActive(false);
        currentLayout = layouts[Random.Range(0, layouts.Length)];
        currentLayout.SetActive(true);
    }

    IEnumerator PopUpHandler()
    {

        if (score_P1 == 4 && score_P2 == 0)
        {
            popUpUI.SetActive(true);
            popUpText.text = popUpsDom[Random.Range(0, popUpsDom.Length)];
        }
        else if (score_P2 == 4 && score_P1 == 0)
        {
            popUpUI.SetActive(true);
            popUpText.text = popUpsDom[Random.Range(0, popUpsDom.Length)];
        }

        yield return new WaitForSecondsRealtime(3f);
        popUpUI.SetActive(false);
    }

    void PopUpInitialiser()
    {
        popUpsDom[0] = "DEMOLISHMENT";
        popUpsDom[1] = "OBLITERATION";
        popUpsDom[2] = "DOMINATION";
        popUpsDom[3] = "ANNIHILATION";
        popUpsDom[4] = "EXTERMINATION";
        popUpsDom[5] = "ERADICATION";
        popUpsDom[6] = "DECIMATION";
        popUpsDom[7] = "MASSACRE";
        popUpsDom[8] = "SLAUGHTER";
        popUpsDom[8] = "LOPSIDED";
        popUpsDom[9] = "Are you even trying?";
    }
    #endregion

    #region Button Events
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
        Application.Quit();
    }
    #endregion

    #region Debugging/Testing
    /*void testingSommin()
    {
        if (!roundOver && gameStarted)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            p1RB.MovePosition(new Vector3(mousePosition.x, mousePosition.y, 0));
        }

    }*/
    #endregion

}
