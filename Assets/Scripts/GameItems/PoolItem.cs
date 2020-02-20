using UnityEngine;

[RequireComponent(typeof(NamedItem))]
public abstract class PoolItem: MonoBehaviour
{
    public abstract void ReturnToPool();
    public abstract void LoadFromPool();
    public abstract NamedItem NamedItem();
}
