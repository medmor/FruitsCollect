using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour
{
    [Header("Player Inventory Panel")]
    [SerializeField] private GameObject playerInventoryMenu = default;

    [Header("Home Button")]
    [SerializeField] private Button homeButton = default;

    [Header("Health Bar")]
    [SerializeField] private Image forgroundHealthBar = default;

    [Header("Hearts")]
    [SerializeField] private TextMeshProUGUI HeartsText = default;

    [Header("Bullets Text")]
    [SerializeField] private TextMeshProUGUI BulletsText = default;
    [SerializeField] private InventoryItemSO BulletsItem = default;

    [Header("Items")]
    [SerializeField] private Image[] itemImages = default;

    private void Start()
    {
        homeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.LoadIntro();
        });
        SetBulletsText();
        EventsManager.Instance.OnBulletsAmountChanged.AddListener(SetBulletsText);
    }

    private void SetBulletsText()
    {
        BulletsText.text = BulletsItem.GetAmount().ToString();
    }

    public void Hide()
    {
        playerInventoryMenu.SetActive(false);
    }

    public void Show()
    {
        playerInventoryMenu.SetActive(true);
    }

    public void SetImageItem(int index)
    {
        var temp = itemImages[index].color;
        temp.a = 1f;
        itemImages[index].color = temp;
    }

    public void SetHealthBar(float value)
    {
        forgroundHealthBar.fillAmount = value;
    }

    public float GetHealth()
    {
        return forgroundHealthBar.fillAmount * 10;
    }

    public void SetHeartsText(int value)
    {
        HeartsText.SetText(value.ToString());
    }

    public int GetHeartsInt()
    {
        return int.Parse(HeartsText.text);
    }

    public void ResetInventory(bool all)
    {
        foreach (var image in itemImages)
        {
            var temp = image.color;
            temp.a = .3f;
            image.color = temp;
        }
        if (all)
        {
            forgroundHealthBar.fillAmount = 1;
            HeartsText.text = "3";
        }
    }
}
