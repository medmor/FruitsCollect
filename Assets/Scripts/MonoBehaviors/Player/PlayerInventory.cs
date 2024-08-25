using System;
using UnityEngine;
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] float health = 10;
    [NonSerialized] public readonly float maxHealth = 10;

    [SerializeField] int hearts = 3;

    [SerializeField] int[] items = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
    [SerializeField] InventoryItemSO bullets;



    public void SetHealth(float value)
    {
        health = value;
        UIManager.Instance.PlayerInventory.SetHealthBar(health / maxHealth);
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHearts(int value)
    {
        hearts = value;
        UIManager.Instance.PlayerInventory.SetHeartsText(hearts);
    }

    public int GetHearts()
    {
        return hearts;
    }

    public void CollectItem(int itemId)
    {
        items[itemId] = 1;
        UIManager.Instance.PlayerInventory.SetImageItem(itemId);
    }

    public bool CheckAllCollected()
    {
        foreach (var item in items)
        {
            if (item == 0) return false;
        }
        return true;
    }

    public void UpdateBullets(int amount)
    {
        bullets.UpdateAmount(amount);
    }

}