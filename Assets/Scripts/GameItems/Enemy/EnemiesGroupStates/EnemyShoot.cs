using System;
using UnityEngine;

public class EnemyShoot : AbstractState
{

    public EnemyShoot(GameController game, EnemiesController enemiesGroup)
    {
        _game = game;
        _enemiesController = enemiesGroup;
    }

    public override Type Tick()
    {
        if (_game.IsPause) return typeof(EnemyPause);
        if (_enemiesController.GetRandomNotEmptyColumn() == null) return typeof(EnemyDestroyed);

        if (_enemiesController.NextShotTimestamp < Time.time)
        {
            var randomColumn = _enemiesController.GetRandomNotEmptyColumn();
            if (randomColumn != null)
            {
                randomColumn.Shoot();
                _enemiesController.NextShotTimestamp =
                    Time.time + UnityEngine.Random.Range(1 / _enemiesController.CurrentLevelSetup.minReloadTime, 1 / _enemiesController.CurrentLevelSetup.maxReloadTime);
            }
        }

        return typeof(EnemyMove);
    }
}
