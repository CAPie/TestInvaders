using UnityEngine;
using System.Collections;

public interface IShootingItem
{
    float Damage { get; }
    float ProjectileSpeed { get; }
    Transform  ShootingPoint { get; }
}
