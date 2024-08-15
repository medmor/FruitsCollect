using UnityEngine;

[CreateAssetMenu(fileName = "PatrolDefinition", menuName = "SO/Enemies/PatrolDefinition")]
public class PatrolDefinition : ScriptableObject
{
    public float patrolSpeed = 5;
    public float followSpeed = 5;
}