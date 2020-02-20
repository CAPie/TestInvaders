using System;
using UnityEngine;

public class EnemyMove : AbstractState
{
    public EnemyMove(GameController game, EnemiesController enemiesGroup)
    {
        _game = game;
        _enemiesController = enemiesGroup;
    }

    public override Type Tick()
    {
        if (_game.IsPause) return typeof(EnemyPause);
        if(_enemiesController.GetRandomNotEmptyColumn() == null) return typeof(EnemyDestroyed);
        // TODO Change speed on enemy kill
        var xMove = _enemiesController.MovementDirectionRight * _enemiesController.CurrentLevelSetup.minSpeed * Time.deltaTime;

        _enemiesController.transform.position = new Vector3(
            _enemiesController.transform.position.x + xMove,
            _enemiesController.transform.position.y,
            _enemiesController.transform.position.z);

        if (_enemiesController.NextShotTimestamp < Time.time) return typeof(EnemyShoot);

        return typeof(EnemyMove);
    }
}
