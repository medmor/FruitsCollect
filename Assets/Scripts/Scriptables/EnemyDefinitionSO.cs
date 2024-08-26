using UnityEngine;

[CreateAssetMenu(fileName = "EnemySo", menuName = "SO/Enemies/EnemySo")]
public class EnemyDefinitionSO : ScriptableObject
{
    public float damagePower = 1;
    public float health = 1;
    public bool isDestoryable = false;
    public bool hasAttackAnimation = false;
}
