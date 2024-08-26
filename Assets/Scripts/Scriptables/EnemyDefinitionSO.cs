using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefinition", menuName = "SO/Enemies/EnemyDefinitionSo")]
public class EnemyDefinitionSO : ScriptableObject
{
    public float damagePower = 1;
    public float health = 1;
    public bool isDestoryable = false;
    public bool hasAttackAnimation = false;
}
