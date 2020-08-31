using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Manager<UIManager>
{
    [Header("Boot Menu")]
    public BootMenu bootMenu = default;

    [Header("Win Menu")] 
    public WinGameOverMenu winMenu = default;

    [Header("GameOver Menu")]
    public WinGameOverMenu gameOverMenu = default;

    [Header("Player Invetory UI")]
    public PlayerInventoryUI playerInventory = default;

    public void Start()
    {
        bootMenu.Show();
    }



}