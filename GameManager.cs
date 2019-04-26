using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;

    public static GameManager Instance;

    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject getReadyPage;
    public TextMeshProUGUI scoreText;

    enum PageState 
    { 
        None,
        Start,
        GameOver,
        GetReady
    }

    int score = 0;
    bool gameOver = false;

    public bool GameOver { get { return gameOver; } }

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        GetReady.OnGetReadyFinished += OnGetReadyFinished;
        Character.OnPlayerDied += OnPlayerDied;
        Character.OnPlayerScored += OnPlayerScored;
    }

    void OnDisable ()
    {
        GetReady.OnGetReadyFinished += OnGetReadyFinished;
        Character.OnPlayerDied -= OnPlayerDied;
        Character.OnPlayerScored -= OnPlayerScored;
    }

    void OnGetReadyFinished()
    {
        SetPageState(PageState.None);
        OnGameStarted();
        score = 0;
        gameOver = false;
    }

    void OnPlayerDied()
    {
        gameOver = true;
        int savedScore = PlayerPrefs.GetInt("Highscore");
        if ( score > savedScore )
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        SetPageState(PageState.GameOver);
    }

    void OnPlayerScored()
    {
        score++;
        scoreText.text = score.ToString();
    }

    void SetPageState(PageState state)
    {
        switch (state) 
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                getReadyPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                getReadyPage.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                getReadyPage.SetActive(false);
                break;
            case PageState.GetReady:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                getReadyPage.SetActive(true);
                break;
        }
    }

    public void ConfirmGameOver()
    {
        //activated when replay button is hit
        OnGameOverConfirmed();
        scoreText.text = "0";
        SetPageState(PageState.GetReady);
    }

    public void StartGame()
    {
        //activated when play button is hit
        SetPageState(PageState.GetReady);
    }
}
