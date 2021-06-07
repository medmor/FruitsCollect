using UnityEngine;
public class PlayerInventory: MonoBehaviour
{
    public float Health { get; private set; } = 10;
    public readonly float maxHealth = 10;

    public int Hearts { get; private set; } = 3;

    public int[] items = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };



    public void SetHealth(float value)
    {
        Health = value;
        UIManager.Instance.PlayerInventory.SetHealthBar(Health/maxHealth);
    }

    public void SetHearts(int value)
    {
        Hearts = value;
        UIManager.Instance.PlayerInventory.SetHeartsText(Hearts);
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

}