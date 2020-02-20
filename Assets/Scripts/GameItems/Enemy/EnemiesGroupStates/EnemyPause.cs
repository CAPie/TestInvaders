using System;
using UnityEngine;

public class EnemyPause : AbstractState
{
    public EnemyPause(GameController game, EnemiesController enemiesGroup)
    {
        _game = game;
        _enemiesController = enemiesGroup;
    }

    public override Type Tick()
    {
        if (_game.IsPause) return typeof(EnemyPause);

        return typeof(EnemyMove);
    }
}
