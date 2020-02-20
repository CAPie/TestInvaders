using System;
using UnityEngine;

public class GameEvents
{
    private readonly string LOG_FORMAT = "[GameEvents] {0}({1})";
    private static GameEvents _instance;
    public static GameEvents Instance { get {
            if (_instance == null)
            {
                _instance = new GameEvents();
            }
            return _instance;
        }
    }

    // == PLAYER ======================
    public event Action<PalyerController> OnPlayerStatsUpdate;
    public void PlayerStatsUpdate(PalyerController player)
    {
        if(OnPlayerStatsUpdate != null)
        {
            Debug.Log(string.Format(LOG_FORMAT, "PlayerStatsUpdate", player));
            OnPlayerStatsUpdate(player);
        }
    }

    // == GAME ========================
    public event Action<GameController> OnGameStatsUpdate;
    public void GameStatsUpdate(GameController game)
    {
        if(OnGameStatsUpdate != null)
        {
            Debug.Log(string.Format(LOG_FORMAT, "GameStatsUpdate", game));
            OnGameStatsUpdate(game);
        }
    }

    public event Action<bool> OnGameOver;
    public void GameOver(bool win)
    {
        if(OnGameOver != null)
        {
            Debug.Log(string.Format(LOG_FORMAT, "OnGameOver", win));
            OnGameOver(win);
        }
    }

    public event Action OnNewGame;
    public void NewGame()
    {
        if(OnNewGame != null)
        {
            Debug.Log(string.Format(LOG_FORMAT, "OnGameOver", ""));
            OnNewGame();
        }
    }

    public event Action<Enemy> OnEnemyKilled;
    public void EnemyKilled(Enemy enemy)
    {
        if(OnEnemyKilled != null)
        {
            Debug.Log(string.Format(LOG_FORMAT, "EnemyKilled", enemy.NamedItem().definedName));
            OnEnemyKilled(enemy);
        }
    }

    // == INPUTS ======================
    public event Action OnPauseGame;
    public void PauseGame()
    {
        if (OnPauseGame != null)
        {
            Debug.Log(string.Format(LOG_FORMAT, "OnPauseGame", ""));
            OnPauseGame();
        }
    }

    public event Action<bool> OnPauseGameValue;
    public void PauseGameValue(bool value)
    {
        if (OnPauseGameValue != null)
        {
            Debug.Log(string.Format(LOG_FORMAT, "PauseGameValue", value));
            OnPauseGameValue(value);
        }
    }

    public event Action OnAttackInput;
    public void PlayerAttackInput()
    {
        if (OnAttackInput != null)
        {
            OnAttackInput();
        }
    }
}
