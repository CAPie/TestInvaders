using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : PoolItem
{
    private float _speed = 1;
    private float _damage = 1;
    private NamedItem _namedItem;
    private bool _isPause = false;

    public float Speed { set { _speed = value; } }
    public float Damage { set { _damage = value; } }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damagable = other.GetComponent<IDamagable>();
        if(damagable != null)
        {
            damagable.Hit(_damage);
            ReturnToPool();
        }

        var otherBullet = other.GetComponent<Bullet>();
        if (otherBullet != null)
        {
            otherBullet.ReturnToPool();
            ReturnToPool();
        }
    }

    public void Spawn(IShootingItem shootingItem)
    {
        var bullet = ObjectPooller.Instance.GetFromPool(this, shootingItem.ShootingPoint.position) as Bullet;
        bullet.transform.rotation = shootingItem.ShootingPoint.rotation;
        bullet.Speed = shootingItem.ProjectileSpeed;
        bullet.Damage = shootingItem.Damage;
    }

    private void Move()
    {
        if (!_isPause)
        {
            var move = _speed * Time.deltaTime;
            transform.position += new Vector3(0, move, 0);
        }
    }

    public override void LoadFromPool()
    {
        GameEvents.Instance.OnPauseGameValue += OnPauseGame;
        GameEvents.Instance.OnGameOver += OnGameOver;
        gameObject.SetActive(true);
    }

    public override void ReturnToPool()
    {
        GameEvents.Instance.OnPauseGameValue += OnPauseGame;
        GameEvents.Instance.OnGameOver -= OnGameOver;
        gameObject.SetActive(false);
    }

    public override NamedItem NamedItem()
    {
        return GetComponent<NamedItem>();
    }

    private void OnPauseGame(bool value)
    {
        _isPause = value;
    }

    private void OnGameOver(bool value)
    {
        ReturnToPool();
    }
}
