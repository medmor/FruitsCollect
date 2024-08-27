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

    [Header("Timer")]
    public Timer Timer = default;

    public void Start()
    {
        BootMenu.Show();

        EventsManager.Instance.OnLevelChoosen.AddListener(OnLevelChoosen);
    }

    void OnLevelChoosen(int levelName)
    {
        PlayerInventory.Show();
        Controls.Show();
        Timer.ResetTimer();
        Timer.Begin();
    }

    public void OnLevelReset()
    {
        Timer.ResetTimer();
        Timer.Begin();
    }



}