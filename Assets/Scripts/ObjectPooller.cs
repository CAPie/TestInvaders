using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooller : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public PoolItem prefab;
        public int poolSize;
    }

    public static ObjectPooller Instance { get; private set; }

    [SerializeField]
    private List<Pool> pools;

    private Dictionary<string, PoolItem> _poolPrefabByName;
    private Dictionary<string, Queue<PoolItem>> _poolsDictionary;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        _poolPrefabByName = InitPrefabsByName();
        _poolsDictionary = InitPoolDictionary();
    }

    private Dictionary<string, PoolItem> InitPrefabsByName()
    {
        var dict = new Dictionary<string, PoolItem>();

        foreach (var pool in pools)
        {
            var key = pool.prefab.NamedItem().definedName;
            var item = pool.prefab;
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, item);
            }
        }

        return dict;
    }

    private Dictionary<string, Queue<PoolItem>> InitPoolDictionary()
    {
        var dict = new Dictionary<string, Queue<PoolItem>>();

        foreach (var pool in pools)
        {
            var key = pool.prefab.NamedItem().definedName;
            var queue = new Queue<PoolItem>();
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, queue);
            }

            for (var i = queue.Count; i < pool.poolSize; i++)
            {
                var item = Instantiate<PoolItem>(pool.prefab);
                // init spawn outside the world
                item.transform.position = new Vector3(-99, -99, -99);

                queue.Enqueue(item);
            }
        }

        return dict;
    }

    public PoolItem GetFromPool(PoolItem prefab, Vector3 position)
    {
        return GetFromPool(prefab.NamedItem().definedName, position);
    }

    public PoolItem GetFromPool(string prefabName, Vector3 position)
    {
        if (_poolsDictionary.ContainsKey(prefabName) && _poolsDictionary[prefabName].Count > 0)
        {
            var item = _poolsDictionary[prefabName].Dequeue();
            item.transform.position = position;
            item.LoadFromPool();

            _poolsDictionary[prefabName].Enqueue(item);

            return item;
        }
        else
        {
            Debug.LogWarning("No such item in pool: " + prefabName);
            return null;
        }
    }
}
