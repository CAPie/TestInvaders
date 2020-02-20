using UnityEngine;

[RequireComponent(typeof(NamedItem))]
public class DefenceItem : MonoBehaviour, IDamagable
{
    private float hp;

    private void Awake()
    {
        var data = ConfigReader.DefenceItemData(GetComponent<NamedItem>());
        hp = data.hp;
    }

    public void Hit(float damage)
    {
        hp -= damage;
        if (hp <= 0) gameObject.SetActive(false);
    }
}
