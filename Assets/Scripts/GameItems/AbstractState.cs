using UnityEngine;
using System.Collections;
using System;

public abstract class AbstractState
{
    protected GameController _game;
    protected EnemiesController _enemiesController;
    public abstract Type Tick();
}
