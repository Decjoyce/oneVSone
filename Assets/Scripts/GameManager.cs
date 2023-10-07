using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor;
//using UnityEngine.iOS;
using UnityEngine.UI;
using System.Linq;

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

    public UnityEvent HitPlayer1, HitPlayer2, GameEnded;

    public bool gamePaused = false;
    public bool gameOver = false;
    public bool roundOver = true;
    public bool gameStarted = false;
    bool waitForReady;
    public bool ready_p1 = false;
    public bool ready_p2 = false;

    int score_P1;
    int score_P2;
    static int wins_P1;
    static int wins_P2;

    public GameObject p1;
    public GameObject p2;
    public Rigidbody2D p1RB;

    public Transform spawn_p1;
    public Transform spawn_p2;

    private GameObject currentLayout;
    int layoutNum;

    public PlayerInputManager inputManager;

    #endregion

    #region UI
    [SerializeField]
    private GameObject winnerUI;
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private GameObject gameplayUI, countdownUI, readyUI, toggle_p1, toggle_p2;

    [SerializeField]
    TextMeshProUGUI winnerText, scoreWinningText, scoreText, countdownText, win1Text, win2Text;

    [SerializeField]
    private GameObject pauseButton, gameOverButton;

    private int countdownTime = 3;

    [SerializeField]
    PopUpHandler popUpHandler;

    bool comeback = false;
    #endregion
    public GameObject[] layouts;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        //StartCoroutine(StartGame());
        LayoutSetter();
        Time.timeScale = 1f;
        win1Text.text = "Wins: " + wins_P1;
        win2Text.text = "Wins: " + wins_P2;
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
                EventSystem.current.SetSelectedGameObject(pauseButton);
            }
        }
    }


    #region Score Code
    public void IncreaseScore_P1()
    {
        HitPlayer1.Invoke();
        if (!gameOver)
            score_P1++;

        if (score_P1 == 5)
        {
            StartCoroutine(GameOver(1));
        }
        else
            StartCoroutine(ResetRound());
    }

    public void IncreaseScore_P2()
    {
        HitPlayer2.Invoke();
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
        if (inputManager.playerCount == 1 && !gameStarted)
        {
            if (ready_p1 || ready_p2)
            {
                StartCoroutine(StartGame());
                readyUI.SetActive(false);
                gameStarted = true;
                //EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (ready_p1)
            toggle_p1.SetActive(true);
        else
            toggle_p1.SetActive(false);

        if (ready_p2)
            toggle_p2.SetActive(true);
        else
            toggle_p2.SetActive(false);
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
        p1.GetComponent<Weapon>().RandomFireInitiartor();
        p2.GetComponent<Weapon>().RandomFireInitiartor();
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

        LayoutSetter();
        CheckScore();
        p1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        p1.GetComponent<Weapon>().RoundHandler(0);
        p2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        p2.GetComponent<Weapon>().RoundHandler(4);

        countdownUI.SetActive(true);
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        countdownUI.SetActive(false);
        countdownTime = 3;
        p1.GetComponent<Weapon>().RandomFireInitiartor();
        p2.GetComponent<Weapon>().RandomFireInitiartor();
        roundOver = false;
        gamePaused = false;
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
            wins_P1++;
        }
        if (winner == 2)
        {
            winnerText.color = new Color(255, 0, 0);
            scoreWinningText.color = new Color(255, 0, 0);
            wins_P2++;
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
        layoutNum = Random.Range(0, layouts.Length);
        currentLayout = layouts[layoutNum];
        currentLayout.SetActive(true);
    }

    public void ChangeMap()
    {
        if (currentLayout != null)
            currentLayout.SetActive(false);
        layoutNum++;
        if (layoutNum >= layouts.Length)
            layoutNum = 0;
        currentLayout = layouts[layoutNum];
        currentLayout.SetActive(true);
    }

    void CheckScore()
    {
        if ((score_P1 == 4 && score_P2 == 0) || (score_P1 == 0 && score_P2 == 4))
        {
            popUpHandler.PopUpDom();
            comeback = true;
        }
            
        if (score_P1 == 4 && score_P2 == 4)
        {
            if(!comeback)
                popUpHandler.PopUpClose();
            else
                popUpHandler.PopUpComeback();
        }

    }

    #endregion

    #region Button Events
    public void Restart()
    {
        SceneManager.LoadScene(0);
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
