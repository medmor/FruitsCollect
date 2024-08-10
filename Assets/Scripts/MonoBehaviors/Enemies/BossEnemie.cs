using UnityEngine;
using UnityEngine.AI;

public class BossEnemie : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = 1.5f;
        target = GameObject.Find("Player").transform;

    }

    void Update()
    {
        agent.SetDestination(target.position);
    }


}
