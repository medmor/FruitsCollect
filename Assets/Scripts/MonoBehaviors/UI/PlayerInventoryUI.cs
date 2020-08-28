using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour
{
    [Header("Player Inventory Panel")]
    [SerializeField] private GameObject playerInventoryMenu = default;

    [Header("Health Bar")]
    [SerializeField] private Image forgroundHealthBar = default;

    [Header("Hearts")]
    [SerializeField] private TextMeshProUGUI HeartsText = default;

    [Header("Items")]
    [SerializeField] private Image[] itemImages = default;

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

    public void SetHeartsText(int value)
    {
        HeartsText.SetText(value.ToString());
    }

    public void Reset()
    {
        foreach(var image in itemImages)
        {
            var temp = image.color;
            temp.a = .3f;
            image.color = temp;
        }
        forgroundHealthBar.fillAmount = 1;
        HeartsText.text = "3";
    }
}
