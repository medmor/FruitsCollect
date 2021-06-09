using UnityEngine;

public class SaveManager : Manager<SaveManager>
{

    public readonly string Level = "level";
    public readonly string BestScore = "bestScore";

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        if (GetLevel() == 0)
            SetLevel(1);
    }

    public bool IsBestScoreSaved()
        => PlayerPrefs.HasKey(BestScore);

    public void SetLevel(int value)
    {
        PlayerPrefs.SetInt(Level, value);
        PlayerPrefs.Save();
    }

    public int GetLevel()
        => PlayerPrefs.GetInt(Level);

    public void SetBestScore(int value)
    {
        PlayerPrefs.SetInt(BestScore, value);
        PlayerPrefs.Save();
    }

    public int GetBestScore()
        => PlayerPrefs.GetInt(BestScore);

    public void ResetSaving()
    {
        SetBestScore(0);
        SetLevel(1);
    }
}
