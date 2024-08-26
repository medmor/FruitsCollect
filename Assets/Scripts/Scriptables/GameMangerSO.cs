using UnityEngine;

[CreateAssetMenu(fileName = "GameMangerSO", menuName = "SO/GameManger")]
public class GameMangerSO : ScriptableObject
{
    public int NumberOfLevels = 9;
    public int currentLevel = 1;
    public Enums.GameState CurrentGameState = Enums.GameState.PREGAME;
}
