using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, AbstractState> _states;
    private AbstractState _state;

    private void Awake()
    {
        var enemiesController = GetComponent<EnemiesController>();
        var game = GetComponentInParent<GameController>();


        //TODO Need More states
        _states = new Dictionary<Type, AbstractState>()
        {
            { typeof (EnemyPause), new EnemyPause(game, enemiesController)},
            { typeof (EnemyDestroyed), new EnemyDestroyed(game, enemiesController)},
            { typeof (EnemyMove), new EnemyMove(game, enemiesController)},
            { typeof (EnemyShoot), new EnemyShoot(game, enemiesController)}
        };
    }

    void Start()
    {
        _state = _states[typeof(EnemyPause)];
    }

    void Update()
    {
        _state = _states[_state.Tick()];
    }
}
