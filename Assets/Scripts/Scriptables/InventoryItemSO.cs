using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "SO/Player/Inventory/InventoryItem")]
public class InventoryItemSO : ScriptableObject
{
    [SerializeField] private int amount;
    public int GetAmount() => amount;
    public void UpdateAmount(int a)
    {
        amount += a;
        EventsManager.Instance.OnBulletsAmountChanged?.Invoke();
    }
}