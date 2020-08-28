using UnityEngine;
public class PlayerInventory: MonoBehaviour
{
    public float Health = 10;
    public readonly float maxHealth = 10;

    public int Hearts = 3;

    public int[] items = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };



    public void SetHealth(float value)
    {
        Health = value;
    }

    public void SetHearts(int value)
    {
        Hearts = value;
    }

    public void CollectItem(int itemId)
    {
        items[itemId] = 1;
        UIManager.Instance.playerInventory.SetImageItem(itemId);
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