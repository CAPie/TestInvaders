using System;
using UnityEngine;

public class GameEvents
{
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
    public void PlayerStatsUpdate(PalyerController palyer)
    {
        if(OnPlayerStatsUpdate != null)
        {
            OnPlayerStatsUpdate(palyer);
        }
    }

    // == GAME ========================
    public event Action<GameController> OnGameStatsUpdate;
    public void GameStatsUpdate(GameController game)
    {
        if(OnGameStatsUpdate != null)
        {
            OnGameStatsUpdate(game);
        }
    }

    public event Action<bool> OnGameOver;
    public void GameOver(bool win)
    {
        if(OnGameOver != null)
        {
            OnGameOver(win);
        }
    }

    public event Action OnNewGame;
    public void NewGame()
    {
        if(OnNewGame != null)
        {
            OnNewGame();
        }
    }

    public event Action<Enemy> OnEnemyKilled;
    public void EnemyKilled(Enemy enemy)
    {
        if(OnEnemyKilled != null)
        {
            OnEnemyKilled(enemy);
        }
    }

    // == INPUTS ======================
    public event Action OnPauseGame;
    public void PauseGame()
    {
        if (OnPauseGame != null)
        {
            OnPauseGame();
        }
    }

    public event Action<bool> OnPauseGameValue;
    public void PauseGameValue(bool value)
    {
        if (OnPauseGameValue != null)
        {
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
