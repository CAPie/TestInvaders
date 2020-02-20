using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalyerController : MonoBehaviour, IDamagable, IShootingItem
{
    // TODO JSON config
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float reloadingTime;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private float damage;
    [SerializeField]
    private Bullet projectile;
    [SerializeField]
    private GameObject shootingPoint;

    private GameController _game;
    private float _nextShootTimestamp;

    public float CurrentHealth { get; private set; }
    public float Damage { get { return damage; } }
    public float ProjectileSpeed { get { return projectileSpeed; } }
    public Transform ShootingPoint { get { return shootingPoint.transform; } }

    void Awake()
    {
        _game = GetComponentInParent<GameController>();
        GameEvents.Instance.OnAttackInput += Shoot;
        GameEvents.Instance.OnNewGame += () => GameEvents.Instance.PlayerStatsUpdate(this);
        GameEvents.Instance.OnGameOver += (win) => { if (!win) CurrentHealth = maxHealth; };
    }

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _nextShootTimestamp = Time.time;
        CurrentHealth = maxHealth;

        GameEvents.Instance.PlayerStatsUpdate(this);
    }


    void Update()
    {
        if (!_game.IsPause)
        {
            MoveHorizontal();
        }
    }

    private void MoveHorizontal()
    {
        var xMove = GameInputs.Instance.KeyboardDirection.x * speed * Time.deltaTime;
        var newXPosition = transform.position.x + xMove;

        transform.position = new Vector3(Mathf.Clamp(newXPosition, -_game.WorldRange, _game.WorldRange), 0, 0);
    }

    private void Shoot()
    {
        if (_nextShootTimestamp < Time.time & !_game.IsPause)
        {
            projectile.Spawn(this);
            _nextShootTimestamp = Time.time + reloadingTime;
        }
    }

    public void Hit(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, maxHealth);
        GameEvents.Instance.PlayerStatsUpdate(this);

        if(CurrentHealth == 0)
        {
            GameEvents.Instance.GameOver(false);
        }
    }
}
