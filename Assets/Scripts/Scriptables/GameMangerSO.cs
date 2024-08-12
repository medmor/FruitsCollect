using UnityEngine;

[CreateAssetMenu(fileName = "GameMangerSO", menuName = "GameManger")]
public class GameMangerSO : ScriptableObject
{
    public int NumberOfLevels = 9;
    public string currentLevelName = "Intro";
    public Enums.GameState CurrentGameState = Enums.GameState.PREGAME;
}
