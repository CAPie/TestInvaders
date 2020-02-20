using System;
using UnityEngine;

public class EnemyDestroyed : AbstractState
{
    public EnemyDestroyed(GameController game, EnemiesController enemiesGroup)
    {
        _game = game;
        _enemiesController = enemiesGroup;
    }

    public override Type Tick()
    {
        Debug.Log("DESTROYED STATE");
        if (_game.IsPause) return typeof(EnemyPause);

        if(_enemiesController.GetRandomNotEmptyColumn() == null)
        {
            GameEvents.Instance.GameOver(true);
            return typeof(EnemyPause);
        }

        return typeof(EnemyMove);
    }
}
