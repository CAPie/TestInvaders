using UnityEngine;

public class Enemy : PoolItem, IDamagable, IShootingItem
{
    [SerializeField]
    private Bullet projectile;
    [SerializeField]
    private GameObject shootingPoint;

    private float hp;

    public int Score { get; private set; }
    public float Damage { get; private set; }
    public float ProjectileSpeed { get; private set; }
    public Transform ShootingPoint { get { return shootingPoint.transform; } }


    public void Hit(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            ReturnToPool();
            GameEvents.Instance.EnemyKilled(this);
        }
    }

    public override void LoadFromPool()
    {
        var data = ConfigReader.EnemyData(NamedItem());
        hp = data.hp;
        ProjectileSpeed = -data.projectileSpeed;
        Score = data.score;
        Damage = data.damage;
        gameObject.SetActive(true);
    }

    public override void ReturnToPool()
    {
        gameObject.SetActive(false);
        transform.parent = null;
    }

    public override NamedItem NamedItem()
    {
        return GetComponent<NamedItem>();
    }

    public void Shoot()
    {
        if(Damage > 0) projectile.Spawn(this);
    }
}
