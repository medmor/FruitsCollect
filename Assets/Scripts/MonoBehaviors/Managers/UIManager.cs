using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Manager<UIManager>
{
    [Header("Boot Menu")]
    public BootMenu bootMenu = default;

    [Header("Win Menu")] 
    public WinMenu winMenu = default;

    [Header("GameOver Menu")]
    public WinMenu gameOverMenu = default;

    [Header("Player Invetory UI")]
    public PlayerInventoryUI playerInventory = default;

    public void Start()
    {
        bootMenu.Show();
    }



}