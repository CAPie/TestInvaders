using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static string HP_TEXT_FORMAT = "HP: {0}";
    private static string SCORE_TEXT_FORMAT = "SCORE:\n{0}";
    public Text HPIndicator;
    public Text ScoreIndicator;
    public Text PauseIndicator;
    public GameObject endGameScreen;
    public GameObject winGameScreen;

    private bool _isPause = false;

    void Awake()
    {
        GameEvents.Instance.OnPlayerStatsUpdate += OnPlayerStatsUpdate;
        GameEvents.Instance.OnGameStatsUpdate += OnGameStatsUpdate;
        GameEvents.Instance.OnPauseGameValue += OnPauseGame;
        GameEvents.Instance.OnGameOver += OnGameOver;
        GameEvents.Instance.OnNewGame += OnNewGame;
    }

    private void Start()
    {
        PauseIndicator.color = Color.green;
        endGameScreen.SetActive(false);
        winGameScreen.SetActive(false);
    }

    public void PessButtonPauseGame()
    {
        GameEvents.Instance.PauseGame();
    }

    private void OnPlayerStatsUpdate(PalyerController palyer)
    {
        HPIndicator.text = string.Format(HP_TEXT_FORMAT, palyer.CurrentHealth);
    }

    private void OnGameStatsUpdate(GameController game)
    {
        ScoreIndicator.text = string.Format(SCORE_TEXT_FORMAT, game.Score);
    }

    private void OnPauseGame(bool pause)
    {
        _isPause = pause;
        PauseIndicator.color = _isPause ? Color.red : Color.green;
    }

    private void OnGameOver(bool win)
    {
        _isPause = true;
        winGameScreen.SetActive(win);
        endGameScreen.SetActive(!win);
    }

    private void OnNewGame()
    {
        _isPause = false;
        endGameScreen.SetActive(false);
        winGameScreen.SetActive(false);
    }
}
