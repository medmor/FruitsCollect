using UnityEngine;

public class UIManager : Manager<UIManager>
{
    [Header("Boot Menu")]
    public BootMenu BootMenu = default;

    [Header("Win Menu")]
    public WinGameOverMenu WinMenu = default;

    [Header("GameOver Menu")]
    public WinGameOverMenu GameOverMenu = default;

    [Header("Player Invetory UI")]
    public PlayerInventoryUI PlayerInventory = default;

    [Header("Controls")]
    public Controls Controls;

    public void Start()
    {
        BootMenu.Show();
    }



}