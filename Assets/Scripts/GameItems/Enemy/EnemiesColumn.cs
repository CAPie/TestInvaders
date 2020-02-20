using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemiesColumn : MonoBehaviour
{
    [SerializeField]
    private float enemiesHeighSpacing;

    private Dictionary<Enemy, int> _enemyIndexes;
    private BoxCollider2D _collider;
    private GameController _game;
    private EnemiesController _enemiesController;

    private void Awake()
    {
        _game = GetComponentInParent<GameController>();
        _enemyIndexes = new Dictionary<Enemy, int>();

        GameEvents.Instance.OnEnemyKilled += OnEnemyKill;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_game.IsColliderWorldRange(collision) && HasEmemiesInColumn())
        {
            _enemiesController.WorldBorderHit();
        }
    }

    public void Init(EnemiesController controller)
    {
        _enemiesController = controller;
    }

    public void StepDown(float step)
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y - step,
            transform.position.z);
    }

    public void SetEnemyInRow(Enemy enemy, int rowIndex)
    {
        enemy.transform.position = new Vector3(transform.position.x, transform.position.y - enemiesHeighSpacing * rowIndex, 0);
        enemy.transform.SetParent(transform);

        _enemyIndexes.Add(enemy, rowIndex);
    }

    public void CleanUp()
    {
        var enemies = _enemyIndexes.Keys.ToList();
        foreach (var enemy in enemies)
        {
            _enemyIndexes.Remove(enemy);
            enemy.ReturnToPool();
        }
        transform.DetachChildren();
        _enemyIndexes = null;
        GameEvents.Instance.OnEnemyKilled -= OnEnemyKill;

        Destroy(gameObject);
    }

    public bool HasEmemiesInColumn()
    {
        return _enemyIndexes.Count > 0;
    }

    public void Shoot()
    {
        GetLowestEnemyInColumn().Shoot();
    }

    private Enemy GetLowestEnemyInColumn()
    {
        return _enemyIndexes.OrderBy(enemy => enemy.Value).Last().Key;
    }

    private void OnEnemyKill(Enemy enemy)
    {
        if (enemy != null)
        {
            if (_enemyIndexes.ContainsKey(enemy)) _enemyIndexes.Remove(enemy);
        }
    }
}
